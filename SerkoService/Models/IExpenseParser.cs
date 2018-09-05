using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace SerkoService.Models
{
    //only one method for this interface
    public interface IExpenseParser
    {
        Expense ExtractExpenseData(string rawExpenseText);

        
    }
}

