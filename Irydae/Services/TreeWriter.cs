using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using Irydae.Model;

namespace Irydae.Services
{
    public class TreeWriter
    {
        private const int PersoX = 295;
        private const int PersoY = 265;
        private const int FrameWidth = 110;
        private const int FrameHeight = 110;

        public string GenerateHtml(ICollection<Partenaire> partenaires, Options options)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(Path.Combine("Web", "tree.html"));
            HtmlNode graphContainer = doc.GetElementbyId("graph");
            HtmlNode svg = doc.CreateElement("svg");
            svg.SetAttributeValue("width", "650");
            svg.SetAttributeValue("height", "650");
            graphContainer.AppendChild(svg);
            foreach (var partenaire in partenaires)
            {
                graphContainer.AppendChild(GenerateRelationNode(doc, partenaire));
                svg.AppendChild(GenerateLine(doc, partenaire, options));
            }
            graphContainer.AppendChild(GenerateRelationNode(doc, new Partenaire
            {
                AvatarLink = options.AvatarUrl,
                Position = new Position
                {
                    X = PersoX,
                    Y = PersoY
                }
            }));
            graphContainer.AppendChild(CreateLegende(doc, options));
            doc.Save(Path.Combine(JournalService.DataPath, "Web", "result.html"));
            var cssLink = doc.CreateElement("link");
            cssLink.SetAttributeValue("rel", "stylesheet");
            cssLink.SetAttributeValue("href", @"https://cdn.jsdelivr.net/gh/Irydae/Web@master/tree.css");
            //cssLink.SetAttributeValue("href", @"style.css");
            return string.Concat(cssLink.OuterHtml, graphContainer.ParentNode.ParentNode.OuterHtml);
        }

        private HtmlNode GenerateLine(HtmlDocument doc, Partenaire partenaire, Options options)
        {
            var res = doc.CreateElement("line");
            byte r, g, b;
            if (partenaire.Type != null)
            {
                var type = options.TypesRelation.FirstOrDefault(tr => tr.Nom == partenaire.Type.Nom);
                if (type != null && type.LinkColor.HasValue)
                {
                    r = type.LinkColor.Value.R;
                    g = type.LinkColor.Value.G;
                    b = type.LinkColor.Value.B;
                }
                else
                {
                    r = 0;
                    g = 0;
                    b = 0;
                }
                res.SetAttributeValue("style", string.Format("stroke:rgb({0}, {1}, {2}); stroke-width:2", r, g, b));
            }

            int startX = partenaire.Position.X + FrameWidth/2;
            int startY = partenaire.Position.Y + 20;
            int endX = PersoX + FrameWidth/2;
            int endY = PersoY + 20;

            if (partenaire.Position.Y > PersoY + FrameHeight)
            {
                endY = PersoY + FrameHeight;
            }
            if (partenaire.Position.Y < PersoY - FrameHeight)
            {
                startY = partenaire.Position.Y + FrameHeight;
            }

            res.SetAttributeValue("x1", startX.ToString());
            res.SetAttributeValue("y1", startY.ToString());
            res.SetAttributeValue("x2", endX.ToString());
            res.SetAttributeValue("y2", endY.ToString());
            return res;
        }

        private HtmlNode GenerateRelationNode(HtmlDocument doc, Partenaire partenaire)
        {
            var res = HtmlWriterService.CreateDiv(doc, "rel", string.Format("top:{0}px;left:{1}px", partenaire.Position.Y, partenaire.Position.X));
            //var portrait = HtmlWriterService.CreateDiv(doc, "r-p");
            var frame = HtmlWriterService.CreateDiv(doc, "r-f");
            var checkBox = doc.CreateElement("input");
            checkBox.SetAttributeValue("type", "checkbox");
            checkBox.SetAttributeValue("id", partenaire.Nom ?? string.Empty);
            frame.AppendChild(checkBox);
            if (partenaire.RpsCommuns.Any())
            {
                frame.AppendChild(GenerateListeRpCommuns(doc, partenaire));
            }
            var portrait = doc.CreateElement("label");
            portrait.AddClass("r-p");
            portrait.SetAttributeValue("for", partenaire.Nom ?? string.Empty);

            var portraitImage = doc.CreateElement("img");
            portraitImage.SetAttributeValue("src", partenaire.AvatarLink);
            portrait.AppendChild(portraitImage);

            var userName = doc.CreateElement("div");
            userName.AddClass("r-n");
            userName.AppendChild(doc.CreateTextNode(partenaire.Nom ?? string.Empty));

            frame.AppendChild(portrait);

            if (partenaire.Position.Y > PersoY)
            {
                res.AppendChild(frame); 
                res.AppendChild(userName);
            }
            else
            {
                res.AppendChild(userName);
                res.AppendChild(frame);
            }
            if (!string.IsNullOrWhiteSpace(partenaire.Description))
            {
                var userDesc = HtmlWriterService.CreateDiv(doc, "r-d");
                userDesc.AppendChild(doc.CreateTextNode(partenaire.Description));
                frame.AppendChild(userDesc);
            }
            return res;
        }

        private HtmlNode GenerateListeRpCommuns(HtmlDocument doc, Partenaire partenaire)
        {
            var res = HtmlWriterService.CreateDiv(doc, "r-d r-r");
            foreach (var rp in partenaire.RpsCommuns)
            {
                var link = doc.CreateElement("a");
                link.SetAttributeValue("href", rp.Url);
                link.AppendChild(doc.CreateTextNode(rp.Titre));
                res.AppendChild(link);
            }

            return res;
        }

        private HtmlNode CreateLegende(HtmlDocument doc, Options options)
        {
            var res = HtmlWriterService.CreateDiv(doc, "legend");

            HtmlNode content = doc.CreateElement("table");
            foreach (var typeRelation in options.TypesRelation)
            {
                var row = doc.CreateElement("tr");

                HtmlNode colorCell = doc.CreateElement("td");
                HtmlNode color = doc.CreateElement("div");
                color.SetAttributeValue("style", string.Format("background-color:#{0:X2}{1:X2}{2:X2};", typeRelation.LinkColor.Value.R, typeRelation.LinkColor.Value.G, typeRelation.LinkColor.Value.B));
                colorCell.AppendChild(color);
                row.AppendChild(colorCell);

                HtmlNode label = doc.CreateElement("td");
                label.AppendChild(doc.CreateTextNode(typeRelation.Nom));
                row.AppendChild(label);
                content.AppendChild(row);
            }

            res.AppendChild(content);
            return res;
        }
    }
}