using Domain.Entities;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository imageRepository;
        public ImageService(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public HttpResponseMessage DownloadImageById(Guid id, HttpResponseMessage response)
        {
            var image = imageRepository.GetImageById(id);

            if (image != null)
            {
                //Byte[] data = File.ReadAllBytes(image.ImagePath);

                var stream = new FileStream(image.ImagePath, FileMode.Open);
                //Set the Response Content.
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);

                // Photo.Resize is a static method to resize the image
                //Image img = Photo.Resize(Image.FromFile(@"d:\path\" + source), width, height);

                //MemoryStream memoryStream = new MemoryStream();

                //img.Save(memoryStream, ImageFormat.Jpeg);

                //httpResponseMessage.Content = new ByteArrayContent(memoryStream.ToArray());

                //httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                //httpResponseMessage.StatusCode = HttpStatusCode.O
            }
            return response;
        }

        public async Task<IEnumerable<Image>> ListImages()
        {
            return await imageRepository.ListImages();
        }

        public async Task<HttpStatusCode> SaveImage()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                //Upload Image
                var postedFile = httpRequest.Files["Image"];
                var extension = postedFile.ContentType.Substring(postedFile.ContentType.LastIndexOf('/') + 1);
                //Create custom filename
                var imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.ContentType);
                var filePath = HttpContext.Current.Server.MapPath($"~/Image/{imageName}.{extension}");
                postedFile.SaveAs(filePath);
                await imageRepository.SaveImage(new Image(postedFile.FileName, filePath, postedFile.ContentType));
                return HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
