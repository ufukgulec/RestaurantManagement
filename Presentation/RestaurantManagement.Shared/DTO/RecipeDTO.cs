namespace RestaurantManagement.Shared.DTO
{
    public class RecipeDTO : BaseDTO
    {//Product-Ingredient ara tablosu
        public string? Description { get; set; }
        public Guid ProductId { get; set; }
        public ProductDTO? Product { get; set; }
        public Guid IngredientId { get; set; }
        public IngredientDTO? Ingredient { get; set; }
    }
}
