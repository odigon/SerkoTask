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
        public string RawText { get; set; }

        public string ValueFinder(string rootTag, bool mandatory)
        {
            var s = ValueFinder(rootTag, mandatory, RawText);
            return s;
        }

        public string ValueFinder(string rootTag, bool mandatory, string _rawText)
        {

            //Am not implementing attributes for the opening tag.
            //The format is simply <tag>, not <tag attr=data> 
            var startTag = $"<{rootTag}>";
            var startPoint = _rawText.IndexOf(startTag);
            if (startPoint == -1)
                if (mandatory) throw new NoOpeningTagException(rootTag);
                else return String.Empty;

            //just look for the first corresponding closing tag after the opening tag. 
            //XML standards are that opening tag case must = closing tag case.
            var endTag = $"</{rootTag}>";
            var endPoint = _rawText.IndexOf(endTag, startPoint + startTag.Length);
            if (endPoint == -1)  throw new NoClosingTagException(rootTag);

            endPoint += endTag.Length;

            //use xml library to extract data. XML formatting errors are raised here, its the client responsibility to ensure good XML data 
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