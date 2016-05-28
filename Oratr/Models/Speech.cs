using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oratr.Models
{
    public class Speech
    {
        public int SpeechId { get; set; }
        public string SpeechTitle { get; set; }
        public string SpeechBody { get; set; }
        public TimeSpan TargetDeliveryTime { get; set; }

        public ApplicationUser CreatedBy { get; set; }
    }
}