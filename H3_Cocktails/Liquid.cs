using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3_Cocktails
{
    public enum LiquidType
    {
        Dark_Rum,
        Bourbon,
        Vodka,
        Water,
        Orange_Juice,
        Lime_Juice,
        Tequila,
        Fresh_Cream,
        Kahlua,
        White_Rum,
        Tomato_Juice
    }

    [Table("Liquid")]
    public class Liquid
    {
        [Key]
        public LiquidType LiquidName { get; set; }
        public float AmountInml { get; set; }
        public Liquid(LiquidType liquidName, float amountInml)
        {
            LiquidName = liquidName;
            AmountInml = amountInml;
        }
    }
}
