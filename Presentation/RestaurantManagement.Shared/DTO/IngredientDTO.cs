namespace RestaurantManagement.Shared.DTO
{
    public class IngredientDTO : BaseDTO
    {
        public string Name { get; set; }
        public bool Finished { get; set; }

        public ICollection<RecipeDTO>? Recipes { get; set; }
        public ICollection<WholesaleDTO>? Wholesales { get; set; }
    }
}
