using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3_Cocktails
{
    public class ViewDrink
    {
        [Key]
        public string Name { get; set; }
        public List<Liquid> Liquids { get; set; }
    }
}
