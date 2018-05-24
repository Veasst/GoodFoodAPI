using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoodFoodAPI.Models
{
    public class Dish
    {
        public int dishId { get; set; }
        public string dishName { get; set; }
        public DishType dishType { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public string ingredients { get; set; }
        public string logoPath { get; set; }

        public virtual ICollection<LocalDishes> localDishes { get; set; } = new HashSet<LocalDishes>();
        public virtual ICollection<UserDishes> userDishes { get; set; } = new HashSet<UserDishes>();
    }
}
