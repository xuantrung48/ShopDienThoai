using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheGioiDienThoai.Models
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
