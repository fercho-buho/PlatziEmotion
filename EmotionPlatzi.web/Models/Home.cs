using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionPlatzi.web.Models
{
    public class Home
    {
        public int Id{ get; set; }
        public string WelcomeMessage { get; set; }
        public string FooterMessage { get; set; }= "Footer by Fercho-Buho";
    }
}
