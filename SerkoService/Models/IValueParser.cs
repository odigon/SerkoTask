using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 

namespace SerkoService.Models
{
    //general interface for a parser that extracts data from formatted text.
    //the fact that its XML that we are looking for is implemented in the concrete class.
    //so this can be used for json, csv, or any custom format
    public interface IValueParser
    {
        string RawText { get; set; }

        string ValueFinder(string rootTag, bool mandatory);
        string ValueFinder(string rootTag, bool mandatory, string rawText);
    }
}