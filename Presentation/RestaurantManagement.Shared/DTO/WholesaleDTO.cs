namespace RestaurantManagement.Shared.DTO
{
    public class WholesaleDTO : BaseDTO
    {//Supplier-Ingredient ara tablosu
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool Paid { get; set; }

        public Guid SupplierId { get; set; }
        public SupplierDTO? Supplier { get; set; }
        public Guid IngredientId { get; set; }
        public IngredientDTO? Ingredient { get; set; }
    }
}
