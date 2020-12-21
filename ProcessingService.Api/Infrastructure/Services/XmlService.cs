using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ProcessingService.Api.Infrastructure.Services
{
    public class XmlService : IXmlService
    {
        private const string TAG_PATTERN = @"<[^>]+>|\+";
        private const string NODES_PATTERN = @"(<[^>/]+>)(.+?)(</[^>]+>)";

        public List<string> ExtractXmlTags(string text)
        {
            Regex regex = new Regex(TAG_PATTERN);
            MatchCollection matches = regex.Matches(text);
            return matches.Select(x => x.Value).ToList();
        }

        public string ExtractXmlNodes(string text)
        {
            text = text.Replace(Environment.NewLine, string.Empty);
            Regex regex = new Regex(NODES_PATTERN);
            MatchCollection matches = regex.Matches(text);
            return string.Join(String.Empty, matches.Select(x => x.Value).ToArray());
        }

        public bool XmlTagExist(string text, string xmlTag)
        {
            text = text.Replace(Environment.NewLine, String.Empty);
            Regex regex = new Regex($"(<{xmlTag}>)(.+?)(</{xmlTag}>)");
            Match match = regex.Match(text);

            return match.Success;
        }

        public bool IsXmlWellFormed(string text)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(text);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetXmlNodeValue(string xmlText, string xmlTag)
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlText);

                var xmlNode = xmlDocument.SelectSingleNode($"//{xmlTag}");
                return xmlNode != null ? xmlNode.InnerText : String.Empty;
            }
            catch
            {
                return String.Empty;
            }
        }
    }
}
