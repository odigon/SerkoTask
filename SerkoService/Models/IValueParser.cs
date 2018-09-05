using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 

namespace SerkoService.Models
{
    //general interface for a parser that looks for 
    public interface IValueParser
    {
        string rawText { get; set; }

        string ValueFinder(string rootTag, bool mandatory);
        string ValueFinder(string rootTag, bool mandatory, string rawText);
    }
}