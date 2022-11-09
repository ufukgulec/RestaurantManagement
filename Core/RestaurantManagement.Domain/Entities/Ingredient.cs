using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }
        public bool Finished { get; set; }

        public ICollection<Recipe>? Recipes { get; set; }
        public ICollection<Wholesale>? Wholesales { get; set; }
    }
}
