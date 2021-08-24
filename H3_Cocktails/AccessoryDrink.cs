using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3_Cocktails
{
    class AccessoryDrink : Drink
    {
        public Dictionary<string,string> AccessoryDic { get; set; }
        public AccessoryDrink()
        {
            AccessoryDic = new Dictionary<string, string>();
        }

        public AccessoryDrink(string name) : base(name)
        {
            AccessoryDic = new Dictionary<string, string>();
        }
    }
}
