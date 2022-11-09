using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities
{
    public class Wholesale : BaseEntity
    {//Supplier-Ingredient ara tablosu
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool Paid { get; set; }

        public Guid SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public Guid IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }
    }
}
