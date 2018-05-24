using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoodFoodAPI.Models
{
    public class LocalDishes
    {
        [ForeignKey("Local")]
        public int localId { get; set; }
        public virtual Local local { get; set; }
        [ForeignKey("Dish")]
        public int dishId { get; set; }
        public virtual Dish dish { get; set; }
    }
}
