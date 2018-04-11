using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
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
            HtmlNode svg = doc.CreateElement("svg");
            svg.SetAttributeValue("width", "650");
            svg.SetAttributeValue("height", "650");
            graphContainer.AppendChild(svg);
            var familyTree = GenerateTree(partenaires);
            foreach (var row in familyTree.Niveaux)
            {
                graphContainer.AppendChild(GenerateLine(doc, row));
            }
            doc.Save(Path.Combine(JournalService.DataPath, "Web", "result.html"));
            var cssLink = doc.CreateElement("link");
            cssLink.SetAttributeValue("rel", "stylesheet");
            cssLink.SetAttributeValue("href", @"https://cdn.rawgit.com/Irydae/Web/3641e2b4/style.css");
            //cssLink.SetAttributeValue("href", @"style.css");
            return string.Concat(cssLink.OuterHtml, graphContainer.ParentNode.ParentNode.ParentNode.ParentNode.OuterHtml);
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
            var portrait = HtmlWriterService.CreateDiv(doc, "r-p");

            var portraitImage = doc.CreateElement("img");
            portraitImage.SetAttributeValue("src", partenaire.AvatarLink);
            portrait.AppendChild(portraitImage);
            
            var userName = doc.CreateElement("span");
            userName.AddClass("r-n");
            userName.AppendChild(doc.CreateTextNode(partenaire.Nom));
            portrait.AppendChild(userName);

            var userDesc = HtmlWriterService.CreateDiv(doc, "r-d");
            userDesc.AppendChild(doc.CreateTextNode(partenaire.Description ?? string.Empty));

            res.AppendChild(portrait);
            res.AppendChild(userDesc);
            return res;
        }

        public FamilyTreeViewModel GenerateTree(ICollection<Partenaire> partenaires)
        {
            FamilyTreeViewModel res = new FamilyTreeViewModel();

            res.Niveaux = new ObservableCollection<TreeLevelViewModel>(partenaires.GroupBy(p => p.NiveauRelation).Select(g =>  new TreeLevelViewModel
            {
                Level = g.Key,
                Membres = new ObservableCollection<Partenaire>(g)
            }));
            res.ColumnCount = res.Niveaux.Max(n => n.Membres.Count);
            return res;
        }
    }
}