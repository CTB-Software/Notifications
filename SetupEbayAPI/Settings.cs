using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetupEbayAPI
{
    class Settings
    {
        public string EbayAPI { get; set; } = "https://api.ebay.com/ws/api.dll";
        public string AppID  { get; set; } = "";
        public string DevID  { get; set; } = "";
        public string CartID { get; set; } = "";
        public string RuName { get; set; } = "";
    }
}
