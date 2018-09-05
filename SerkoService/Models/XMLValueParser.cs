using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using SerkoService.Models.Exceptions;

namespace SerkoService.Models
{
    public class XMLValueParser : IValueParser
    {
        public string rawText { get; set; }

        public string ValueFinder(string rootTag, bool mandatory)
        {
            var s = ValueFinder(rootTag, mandatory, rawText);
            return s;
        }

        public string ValueFinder(string rootTag, bool mandatory, string _rawText)
        {

            var startTag = $"<{rootTag}>";
            var startPoint = _rawText.IndexOf(startTag);
            if (startPoint == -1)
                if (mandatory) throw new NoOpeningTagException(rootTag);
                else return String.Empty;

            var endTag = $"</{rootTag}>";
            var endPoint = _rawText.IndexOf(endTag, startPoint + startTag.Length);
            if (endPoint == -1)  throw new NoClosingTagException(rootTag);

            endPoint += endTag.Length;

            var XmlString = _rawText.Substring(startPoint,endPoint-startPoint);
            var XmlDoc = new XmlDocument();
            try
            {
                XmlDoc.Load(new StringReader(XmlString));
                return (XmlDoc.FirstChild.FirstChild?.Value) ?? String.Empty;
            }
            catch
            {
                throw;
            }
        }

    }
}