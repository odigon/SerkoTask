using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//putting small classes such as custom exceptions in a single file
namespace SerkoService.Models.Exceptions
{
    //superclass so I can test for both of them at once
    public class CustomExpenseException : Exception
    {
        public CustomExpenseException(string message) : base(message)
        {

        }
    }
    //custom exception to support the stated requirements.   No closing tag is always fatal.
    public class NoClosingTagException : CustomExpenseException
    {
        public NoClosingTagException(string tag) : base($"No Closing Tag for: {tag}")
        {

        }
    }

    //this is for when the tag cant be found at all.   Fatal if the field is mandatory.
    public class NoOpeningTagException : CustomExpenseException
    {
        public NoOpeningTagException(string tag) : base($"No Opening Tag for: {tag}")
        {

        }
    }
}
