using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oratr.Models
{
    public class Results
    {
        public int ResultsId { get; set; }
        public int ActualWPM { get; set; }
        public object[] DynamicsChart { get; set; }
        public int UhUm { get; set; }
    }
}