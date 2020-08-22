namespace ShopDienThoai.Models.ProductModel
{
    public interface IImageRepository
    {
        Image Get(string id);

        Image Create(Image image);

        bool Remove(string id);
    }
}