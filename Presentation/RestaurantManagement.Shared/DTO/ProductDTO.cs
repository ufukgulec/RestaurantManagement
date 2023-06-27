namespace RestaurantManagement.Shared.DTO
{
    public class ProductDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImgUrl { get; set; }

        public Guid CategoryId { get; set; }
        public CategoryDTO? Category { get; set; }

        public ICollection<OrderDetailDTO>? OrderDetails { get; set; }
        public ICollection<RecipeDTO>? Recipes { get; set; }
    }
}
