using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IImageService
    {
        Task<HttpStatusCode> SaveImage();

        Task<IEnumerable<Image>> ListImages();

        HttpResponseMessage DownloadImageById(Guid id, HttpResponseMessage response);
    }
}
