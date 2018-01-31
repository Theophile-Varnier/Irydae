using HtmlAgilityPack;
using Irydae.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Irydae.Services
{
    public class HtmlWriterService
    {
        private static int circleWidth = 12;
        public string GenerateHtml(IList<Periode> periodes)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(Path.Combine("Web", "index.html"));
            HtmlNode graphContainer = doc.GetElementbyId("graph");
            HtmlNode svg = doc.CreateElement("svg");
            svg.SetAttributeValue("width", "650");
            svg.SetAttributeValue("height", "650");
            graphContainer.AppendChild(svg);
            Periode lastPeriode = null;
            foreach (var periode in periodes)
            {
                graphContainer.AppendChild(GenerateEntry(doc, periode));
                if(lastPeriode != null)
                {
                    svg.AppendChild(GenerateLine(doc, lastPeriode, periode));
                }
                lastPeriode = periode;
            }
            doc.Save(Path.Combine("Web", "result.html"));
            return graphContainer.ParentNode.ParentNode.ParentNode.OuterHtml;
        }

        private HtmlNode GenerateLine(HtmlDocument doc, Periode previous, Periode current)
        {
            var res = doc.CreateElement("line");
            res.SetAttributeValue("x1", (previous.Position.X + circleWidth/2).ToString());
            res.SetAttributeValue("x2", (current.Position.X + circleWidth / 2).ToString());
            res.SetAttributeValue("y1", (previous.Position.Y + circleWidth / 2).ToString());
            res.SetAttributeValue("y2", (current.Position.Y + circleWidth / 2).ToString());
            res.SetAttributeValue("style", "stroke:rgb(0, 0, 0); stroke-width:2");
            return res;
        }

        private HtmlNode GenerateEntry(HtmlDocument doc, Periode periode)
        {
            var res = CreateDiv(doc, "rp progress", string.Format("left:{0}px;top:{1}px;", periode.Position.X, periode.Position.Y));

            var tooltipLeft = 0;
            var tooltipTop = 20;

            if(periode.Position.X > 400)
            {
                tooltipLeft = -200;
            }

            if(periode.Position.Y > 300)
            {
                tooltipTop = -(190 + circleWidth / 2);
            }

            var tooltip = CreateDiv(doc, "tooltip", string.Format("left:{0}px;top:{1}px;", tooltipLeft, tooltipTop));
            res.AppendChild(tooltip);

            var panelTitle = CreateDiv(doc, "panel-title bottom-border", string.Empty);

            var periodeTitle = doc.CreateElement("span");
            periodeTitle.AddClass("lieu bottom-border");
            periodeTitle.AppendChild(doc.CreateTextNode(periode.Lieu));
            periodeTitle.AppendChild(doc.CreateElement("br"));

            var periodePeriode = doc.CreateElement("i");
            periodePeriode.AppendChild(doc.CreateTextNode(string.Format("{0}{1}", periode.DateDebut.ToString("MMM yyyy"), periode.DateFin.HasValue ? "-" + periode.DateFin.Value.ToString("MMM yyyy") : string.Empty)));

            var panelBody = CreateDiv(doc, "panel-body-wrapper", string.Empty);
            foreach(var rp in periode.Rps)
            {
                GenerateRpNode(doc, panelBody, rp);
            }

            panelTitle.AppendChild(periodeTitle);
            panelTitle.AppendChild(periodePeriode);

            tooltip.AppendChild(panelTitle);
            tooltip.AppendChild(panelBody);
            return res;
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
            var participants = CreateDiv(doc, "infosgen", string.Empty)
                .AppendChild(doc.CreateElement("i"));
            participants.AppendChildren(GenerateParticipantsNodes(doc, participants, rp));
            parentDiv.AppendChild(res);
            parentDiv.AppendChild(participants.ParentNode);
        }

        private HtmlNodeCollection GenerateParticipantsNodes(HtmlDocument doc, HtmlNode parent, Rp rp)
        {
            HtmlNodeCollection res = new HtmlNodeCollection(parent);
            res.Add(doc.CreateTextNode("Feat. "));
            foreach (var participant in rp.Partenaires)
            {
                var partenaireNode = doc.CreateElement("span");
                partenaireNode.AddClass(participant.Groupe);
                partenaireNode.AppendChild(doc.CreateTextNode(participant.Nom));
                res.Add(partenaireNode);
                res.Add(doc.CreateTextNode(" - "));
            }
            return res;
        }
    }
}
