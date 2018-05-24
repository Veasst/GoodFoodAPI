using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoodFoodAPI.Models
{
    public class UserLocals
    {
        [ForeignKey("userId")]
        public int userId { get; set; }
        public virtual User user { get; set; }
        [ForeignKey("dishId")]
        public int localId { get; set; }
        public virtual Local local { get; set; }
    }
}
