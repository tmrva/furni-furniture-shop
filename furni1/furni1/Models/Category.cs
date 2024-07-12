namespace furni1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeactive { get; set; }
        public List<ProductCategory>? ProductCategories { get; set; }
    }
}
