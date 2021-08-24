using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3_Cocktails
{
    [Table("Drink")]
    public class Drink
    {
        private List<Liquid> liquids;


        public List<Liquid> Liquids
        {
            get
            {
                return liquids;
            }
            set
            {
                liquids = value;
            }
        }

        [Key]
        public string Name { get; set; }

        public Drink()
        {
            liquids = new List<Liquid>();
        }

        public Drink(string name)
        {
            Name = name;
            liquids = new List<Liquid>();
        }

        public Drink(string name, List<Liquid> liquids) : this(name)
        {
            Liquids = liquids;
        }
    }
}
