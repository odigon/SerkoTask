using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerkoService.Models
{
    //simple class that holds information about the expense.   
    //field names are as per the incoming doument tags
    
    public class Expense
    {
        public Expense()
        {

        }

        public decimal total { get; set; }

        //custom get method means we cant use auto properties
        private string _cost_centre;
        public string cost_centre
        {
            get
            {
                return String.IsNullOrEmpty(_cost_centre) ? "UNKNOWN" : _cost_centre;
            }
            set {
                _cost_centre = value;
            }
        }

        public string payment_method { get; set; }
        public string vendor { get; set; }
        public string description { get; set; }

        //incoming data data is unlikely to be correct enough to reliably convert to datetime, 
        //so this might have to become string in the real world
        public DateTime date { get; set; }

        public decimal gst
        {
            get
            {
                return decimal.Round((total / 11),2);
            }
        }

        //subtract from gst to avoid rounding errors
        public decimal ex_gst
        {
            get
            {
                return total - gst;
            }
        }
    }
}