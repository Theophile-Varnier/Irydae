using HtmlAgilityPack;
using Irydae.Helpers;
using Irydae.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Irydae.Services
{
    public class HtmlWriterService
    {
        private const int borderWidth = 4;

        public Dictionary<string, Position> ReadMapDatas()
        {
            var res = new Dictionary<string, Position>();
            HtmlDocument doc = new HtmlDocument();
            doc.Load(Path.Combine("Web", "mapData.html"), Encoding.UTF8);
            var datas = doc.DocumentNode.ChildNodes[0].ChildNodes;
            foreach(var data in datas)
            {
                string tmp;
                if (!string.IsNullOrWhiteSpace(tmp = data.GetAttributeValue("data-coords", string.Empty)))
                {
                    res.Add(data.ChildNodes[0].ChildNodes[0].NextSibling.InnerText.Trim(), new Position
                    {
                        X = ((int.Parse(tmp.Split(',')[0]) - Options.Crop) * Options.NewMapWidthHeight) / Options.OldMapWidth,
                        Y = (int.Parse(tmp.Split(',')[1]) * Options.NewMapWidthHeight) / Options.OldMapHeight
                    });
                }
            }
            var filePath = Path.Combine("web", "dataMap.json");
            File.WriteAllText(filePath, JsonConvert.SerializeObject(res, Formatting.Indented));
            return res;
        }

        public string GenerateHtml(IList<Periode> periodes, Options options)
        {
            IList<Periode> innerPeriode;
            if (options.DisplayByYear)
            {
                innerPeriode = new List<Periode>();
                var grouped = periodes.GroupBy(p => p.Lieu);
                foreach (var group in grouped)
                {
                    innerPeriode.Add(new Periode
                    {
                        Lieu = group.Key,
                        DateDebut = group.Min(p => p.DateDebut),
                        DateFin = group.Min(p => p.DateFin),
                        SubPeriodes = new List<Periode>(group),
                        Position = group.First().Position
                    });
                }
            }
            else
            {
                innerPeriode = periodes;
            }
            HtmlDocument doc = new HtmlDocument();
            doc.Load(Path.Combine("Web", "index.html"));
            HtmlNode graphContainer = doc.GetElementbyId("graph");
            HtmlNode svg = doc.CreateElement("svg");
            svg.SetAttributeValue("width", "650");
            svg.SetAttributeValue("height", "650");
            graphContainer.AppendChild(svg);
            Periode lastPeriode = null;
            foreach (var periode in innerPeriode.OrderBy(p => p.DateDebut))
            {
                graphContainer.AppendChild(GenerateEntry(doc, periode, options));
            }
            foreach (var periode in periodes.OrderBy(p => p.DateDebut))
            {
                if (lastPeriode != null && periode.Lieu != lastPeriode.Lieu)
                {
                    svg.AppendChild(GenerateLine(doc, lastPeriode, periode, options));
                }
                lastPeriode = periode;
            }
            doc.Save(Path.Combine(JournalService.DataPath, "Web", "result.html"));
            var cssLink = doc.CreateElement("link");
            cssLink.SetAttributeValue("rel", "stylesheet");
            cssLink.SetAttributeValue("href", @"https://cdn.rawgit.com/Theophile-Varnier/Rp/1f25c820/Irydae/carnet/style.css");
            return string.Concat(cssLink.OuterHtml, graphContainer.ParentNode.ParentNode.ParentNode.ParentNode.OuterHtml);
        }

        private HtmlNode GenerateLine(HtmlDocument doc, Periode previous, Periode current, Options options)
        {
            var res = doc.CreateElement("line");
            res.SetAttributeValue("x1", (previous.Position.X + (options.CircleWidth + borderWidth) / 2).ToString());
            res.SetAttributeValue("x2", (current.Position.X + (options.CircleWidth + borderWidth) / 2).ToString());
            res.SetAttributeValue("y1", (previous.Position.Y + (options.CircleWidth + borderWidth) / 2).ToString());
            res.SetAttributeValue("y2", (current.Position.Y + (options.CircleWidth + borderWidth) / 2).ToString());
            byte r, g, b;
            if (options.LinkColor.HasValue)
            {
                r = options.LinkColor.Value.R;
                g = options.LinkColor.Value.G;
                b = options.LinkColor.Value.B;
            }
            else
            {
                r = 0;
                g = 0;
                b = 0;
            }
            res.SetAttributeValue("style", string.Format("stroke:rgb({0}, {1}, {2}); stroke-width:2", r, g, b));
            return res;
        }

        private HtmlNode GenerateEntry(HtmlDocument doc, Periode periode, Options options)
        {
            string inlineStyle = string.Format("left:{0}px;top:{1}px;", periode.Position.X, periode.Position.Y);
            if (options.BorderColor.HasValue)
            {
                inlineStyle = string.Format("{0}border-color:#{1:X2}{2:X2}{3:X2};", inlineStyle, options.BorderColor.Value.R, options.BorderColor.Value.G, options.BorderColor.Value.B);
            }
            if (options.CircleColor.HasValue)
            {
                inlineStyle = string.Format("{0}background-color:#{1:X2}{2:X2}{3:X2};", inlineStyle, options.CircleColor.Value.R, options.CircleColor.Value.G, options.CircleColor.Value.B);
            }
            if (options.CircleWidth != 10)
            {
                inlineStyle = string.Format("{0}width:{1}px;height:{2}px", inlineStyle, options.CircleWidth, options.CircleWidth);
            }
            if(options.BorderRadius != 50)
            {
                inlineStyle = string.Format("{0}border-radius:{1}%;", inlineStyle, options.BorderRadius);
            }
            /*if(options.BorderRotation != 0)
            {
                inlineStyle = string.Format("{0}transform:rotate({1}deg);", inlineStyle, options.BorderRotation);
            }*/
            var res = CreateDiv(doc, "rp progress", inlineStyle);

            var tooltipLeft = 0;
            var tooltipTop = 20;

            if (periode.Position.X > 400)
            {
                tooltipLeft = -200;
            }

            if (periode.Position.Y > 300)
            {
                tooltipTop = -(190 + (options.CircleWidth + borderWidth) / 2);
            }

            var tooltip = CreateDiv(doc, "tooltip", string.Format("left:{0}px;top:{1}px;", tooltipLeft, tooltipTop));
            res.AppendChild(tooltip);

            var panelTitle = CreateDiv(doc, "panel-title bottom-border", string.Empty);

            var periodeTitle = doc.CreateElement("span");
            periodeTitle.AddClass("lieu bottom-border");
            periodeTitle.AppendChild(doc.CreateTextNode(periode.Lieu));

            var panelBody = CreateDiv(doc, "panel-body-wrapper", string.Empty);
            if (options.DisplayByYear)
            {
                foreach (var subPeriode in periode.SubPeriodes)
                {
                    if (periode.SubPeriodes.Count > 1)
                    {
                        panelBody.AppendChild(CreatePeriodePeriode(doc, subPeriode));
                        panelBody.AppendChild(doc.CreateElement("br"));
                    }
                    foreach (var rp in subPeriode.Rps)
                    {
                        GenerateRpNode(doc, panelBody, rp);
                    }
                }
            }
            else
            {
                foreach (var rp in periode.Rps)
                {
                    GenerateRpNode(doc, panelBody, rp);
                }
            }

            panelTitle.AppendChild(periodeTitle);
            if (!options.DisplayByYear || periode.SubPeriodes.Count == 1)
            {
                periodeTitle.AppendChild(doc.CreateElement("br"));
                panelTitle.AppendChild(CreatePeriodePeriode(doc, periode));
            }

            tooltip.AppendChild(panelTitle);
            tooltip.AppendChild(panelBody);
            return res;
        }

        private HtmlNode CreatePeriodePeriode(HtmlDocument doc, Periode periode)
        {
            var periodePeriode = doc.CreateElement("i");
            periodePeriode.AppendChild(doc.CreateTextNode(string.Format("{0}{1}", periode.DateDebut.ToString("MMM yyyy"), periode.DateFin.HasValue ? "-" + periode.DateFin.Value.ToString("MMM yyyy") : string.Empty)));
            return periodePeriode;
        }

        private HtmlNode CreateDiv(HtmlDocument doc, string className, string style)
        {
            var res = doc.CreateElement("div");
            if (!string.IsNullOrWhiteSpace(className))
            {
                res.AddClass(className);
            }
            if (!string.IsNullOrWhiteSpace(style))
            {
                res.SetAttributeValue("style", style);
            }
            return res;
        }

        private void GenerateRpNode(HtmlDocument doc, HtmlNode parentDiv, Rp rp)
        {
            var res = doc.CreateElement("a");
            res.SetAttributeValue("href", rp.Url);
            res.AddClass("lieu bottom-border");
            var titre = doc.CreateElement("span");
            titre.AddClass("titreun");
            titre.AppendChild(doc.CreateTextNode(rp.Titre));
            res.AppendChild(titre);
            parentDiv.AppendChild(res);
            var participants = CreateDiv(doc, "infosgen", string.Empty)
            .AppendChild(doc.CreateElement("i"));
            if (rp.Partenaires.Any())
            {
                participants.AppendChildren(GenerateParticipantsNodes(doc, participants, rp));
            }
            parentDiv.AppendChild(participants.ParentNode);
        }

        private HtmlNodeCollection GenerateParticipantsNodes(HtmlDocument doc, HtmlNode parent, Rp rp)
        {
            HtmlNodeCollection res = new HtmlNodeCollection(parent);
            res.Add(doc.CreateTextNode("Feat. "));
            foreach (var participant in rp.Partenaires)
            {
                var partenaireNode = doc.CreateElement("span");
                partenaireNode.AddClass(participant.Groupe.GetDescription());
                partenaireNode.AppendChild(doc.CreateTextNode(participant.Nom));
                res.Add(partenaireNode);
                res.Add(doc.CreateTextNode(" - "));
            }
            return res;
        }
    }
}
