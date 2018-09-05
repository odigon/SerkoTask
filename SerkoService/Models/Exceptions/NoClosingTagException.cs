﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerkoService.Models.Exceptions
{
    //custom exception to support the stated requirements
    public class NoClosingTagException : Exception
    {
        public NoClosingTagException(string tag) : base($"No Closing Tag for: {tag}")
        {

        }
    }

    //this is for when the tag cant be found at all
    public class NoOpeningTagException : Exception
    {
        public NoOpeningTagException(string tag) : base($"No Opening Tag for: {tag}")
        {

        }
    }
}
