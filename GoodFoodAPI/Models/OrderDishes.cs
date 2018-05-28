using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoodFoodAPI.Models
{
    public class OrderDishes
    {
        [ForeignKey("Order")]
        public int orderId { get; set; }
        public virtual Order order { get; set; }
        [ForeignKey("Dish")]
        public int dishId { get; set; }
        public virtual Dish dish { get; set; }
        public float price { get; set; }
        public int amount { get; set; }
    }
}
