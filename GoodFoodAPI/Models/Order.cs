using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodFoodAPI.Models
{
    public class Order
    {
        public int orderId { get; set; }
        public float total { get; set; }

        public virtual ICollection<OrderDishes> orderDishes { get; set; } = new HashSet<OrderDishes>();

    }
}
