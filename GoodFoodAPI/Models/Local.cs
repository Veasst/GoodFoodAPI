using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodFoodAPI.Models
{
    public class Local
    {
        public int localId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string logoPath { get; set; }
        public string address { get; set; }

        public virtual ICollection<LocalDishes> localDishes { get; set; } = new HashSet<LocalDishes>();
        public virtual ICollection<UserLocals> userLocals { get; set; } = new HashSet<UserLocals>();
    }
}
