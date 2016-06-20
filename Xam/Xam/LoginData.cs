using System;
using System.Collections.Generic;
using System.Text;

namespace Xam
{
    class LoginData
    {
        public Id _id { get; set; }
        public string name { get; set; }
        public string pass { get; set; }
        //public string url { get; set; }

    }
    public class Id
    {
        public string oid { get; set; }
    }
}
