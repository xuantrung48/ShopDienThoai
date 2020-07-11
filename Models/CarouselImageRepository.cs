using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TheGioiDienThoai.Models
{
    public class CarouselImageRepository : ICarouselImageRepository
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CarouselImageRepository(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public CarouselImage Create(CarouselImage image)
        {
            context.CarouselImages.Add(image);
            context.SaveChanges();
            return image;
        }

        public CarouselImage Edit(CarouselImage image)
        {
            var editImage = context.CarouselImages.Attach(image);
            editImage.State = EntityState.Modified;
            context.SaveChanges();
            return image;
        }

        public IEnumerable<CarouselImage> Get()
        {
            return context.CarouselImages;
        }

        public CarouselImage Get(int id)
        {
            var image = (from e in context.CarouselImages
                         where e.Id == id
                         select e).FirstOrDefault();
            return image;
        }

        public bool Remove(int id)
        {
            var imageToRemove = context.CarouselImages.Find(id);
            if (imageToRemove != null)
            {
                string delFile = Path.Combine(webHostEnvironment.WebRootPath, "images\\carousel", imageToRemove.Name);
                File.Delete(delFile);
                context.CarouselImages.Remove(imageToRemove);
                return context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
