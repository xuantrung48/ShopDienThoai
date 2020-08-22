using System.Collections.Generic;

namespace ShopDienThoai.Models
{
    public interface ICarouselImageRepository
    {
        IEnumerable<CarouselImage> Get();

        CarouselImage Get(int id);

        CarouselImage Create(CarouselImage carouselImage);

        CarouselImage Edit(CarouselImage carouselImage);

        bool Remove(int id);
    }
}