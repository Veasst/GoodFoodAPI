using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodFoodAPI.Models
{
    public class User
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int stamps { get; set; }
        public int freeDishes { get; set; }

        public virtual ICollection<UserLocals> userLocals { get; set; } = new HashSet<UserLocals>();
        public virtual ICollection<UserDishes> userDishes { get; set; } = new HashSet<UserDishes>();
    }
}
