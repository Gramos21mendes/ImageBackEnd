using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IImageRepository
    {
        Task SaveImage(Image image);
        Task<IEnumerable<Image>> ListImages();
        Image GetImageById(Guid id);
    }
}
