using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities
{
    public class Recipe : BaseEntity
    {//Product-Ingredient ara tablosu
        public string? Description { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }
    }
}
