using System.ComponentModel.DataAnnotations;

namespace ShopDienThoai.Models.ProductModel
{
    public class Image
    {
        [Required] public string ImageId { get; set; }

        public string ImageName { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}