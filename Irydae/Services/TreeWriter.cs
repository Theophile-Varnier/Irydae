using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using Irydae.Helpers;
using Irydae.Model;
using Irydae.ViewModels;

namespace Irydae.Services
{
    public class TreeWriter
    {
        public string GenerateHtml(ICollection<Partenaire> partenaires, Options options)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(Path.Combine("Web", "tree.html"));
            HtmlNode graphContainer = doc.GetElementbyId("graph");
            //HtmlNode svg = doc.CreateElement("svg");
            //svg.SetAttributeValue("width", "650");
            //svg.SetAttributeValue("height", "650");
            //graphContainer.AppendChild(svg);
            var familyTree = GenerateTree(partenaires, options);
            foreach (var row in familyTree.Niveaux.OrderByDescending(n => n.Level))
            {
                graphContainer.AppendChild(GenerateLine(doc, row));
            }
            graphContainer.AppendChild(CreateLegende(doc, options));
            doc.Save(Path.Combine(JournalService.DataPath, "Web", "result.html"));
            var cssLink = doc.CreateElement("link");
            cssLink.SetAttributeValue("rel", "stylesheet");
            cssLink.SetAttributeValue("href", @"https://rawgit.com/Irydae/Web/master/tree.css");
            //cssLink.SetAttributeValue("href", @"style.css");
            return string.Concat(cssLink.OuterHtml, graphContainer.ParentNode.ParentNode.OuterHtml);
        }

        private HtmlNode GenerateLine(HtmlDocument doc, TreeLevelViewModel level)
        {
            var line = HtmlWriterService.CreateDiv(doc, "row");
            foreach (var relation in level.Membres)
            {
                line.AppendChild(GenerateRelationNode(doc, relation));
            }
            return line;
        }

        private HtmlNode GenerateRelationNode(HtmlDocument doc, Partenaire partenaire)
        {
            var res = HtmlWriterService.CreateDiv(doc, "rel");
            //var portrait = HtmlWriterService.CreateDiv(doc, "r-p");
            var portrait = doc.CreateElement("label");
            portrait.AddClass("r-p");
            portrait.SetAttributeValue("for", partenaire.Nom);

            var portraitImage = doc.CreateElement("img");
            portraitImage.SetAttributeValue("src", partenaire.AvatarLink);
            portrait.AppendChild(portraitImage);

            var userName = doc.CreateElement("div");
            userName.AddClass("r-n");
            userName.AppendChild(doc.CreateTextNode(partenaire.Nom));
            portrait.AppendChild(userName);

            var checkBox = doc.CreateElement("input");
            checkBox.SetAttributeValue("type", "checkbox");
            checkBox.SetAttributeValue("id", partenaire.Nom);

            res.AppendChild(portrait);
            res.AppendChild(checkBox);
            if (!string.IsNullOrWhiteSpace(partenaire.Description))
            {
                var userDesc = HtmlWriterService.CreateDiv(doc, "r-d");
                userDesc.AppendChild(doc.CreateTextNode(partenaire.Description));
                res.AppendChild(userDesc);
            }
            return res;
        }

        public FamilyTreeViewModel GenerateTree(ICollection<Partenaire> partenaires, Options options)
        {
            FamilyTreeViewModel res = new FamilyTreeViewModel();

            res.Niveaux = new ObservableCollection<TreeLevelViewModel>(partenaires.GroupBy(p => p.NiveauRelation).Select(g => new TreeLevelViewModel
            {
                Level = g.Key,
                Membres = new ObservableCollection<Partenaire>(g)
            }));
            res.ColumnCount = res.Niveaux.Max(n => n.Membres.Count);
            TreeLevelViewModel groundZero = res.Niveaux.GetOrAddNew(n => n.Level == 0, () => new TreeLevelViewModel());
            groundZero.Membres.Insert(groundZero.Membres.Count / 2, new Partenaire
            {
                Nom = options.CharacterName,
                AvatarLink = options.AvatarUrl
            });
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