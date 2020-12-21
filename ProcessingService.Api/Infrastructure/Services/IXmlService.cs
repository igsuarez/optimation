using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessingService.Api.Infrastructure.Services
{
    public interface IXmlService
    {
        /// <summary>
        /// Extract all xml tags present in the text
        /// </summary>
        /// <param name="text">Raw text with xml embedded</param>
        /// <returns>List of xml tag found</returns>
        List<string> ExtractXmlTags(string text);

        /// <summary>
        /// Extract all xml nodes present in the text
        /// </summary>
        /// <param name="text">Raw text with xml embedded</param>
        /// <returns>xml nodes as text</returns>
        string ExtractXmlNodes(string text);

        /// <summary>
        /// Check existence of a xml tag in the text
        /// </summary>
        /// <param name="text">Raw text with xml embedded</param>
        /// <param name="xmlTag">Xml tag to be checked</param>
        /// <returns>true, tag exists otherwise false</returns>
        bool XmlTagExist(string text, string xmlTag);

        /// <summary>
        /// Check if text is a xml well formed
        /// </summary>
        /// <param name="text">Supposed xml text</param>
        /// <returns>true, text is a xml well formed otherwise false</returns>
        bool IsXmlWellFormed(string text);

        string GetXmlNodeValue(string xmlText, string xmlTag);
    }
}
