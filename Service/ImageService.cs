using Domain.Entities;
using Ionic.Zip;
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

        public HttpResponseMessage DownloadImageById(Guid id)
        {
            var image = imageRepository.GetImageById(id);

            if (image != null)
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);

                var fileName = image.ImageVirtualPath.Substring(image.ImageVirtualPath.LastIndexOf("\\") + 1);
                var filePath = HttpContext.Current.Server.MapPath($"~/Image/{fileName}");

                var fileBytes = File.ReadAllBytes(filePath);

                var fileMemStream = new MemoryStream(fileBytes);

                result.Content = new StreamContent(fileMemStream);
                var headers = result.Content.Headers;
                headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                headers.ContentDisposition.FileName = fileName;

                //headers.ContentType = new MediaTypeHeaderValue(image.ContentType);
                headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Photo.Resize is a static method to resize the image
                //Image img = Photo.Resize(Image.FromFile(@"d:\path\" + source), width, height);

                //MemoryStream memoryStream = new MemoryStream();

                //img.Save(memoryStream, ImageFormat.Jpeg);

                //httpResponseMessage.Content = new ByteArrayContent(memoryStream.ToArray());

                //httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                //httpResponseMessage.StatusCode = HttpStatusCode.O
                return result;
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage DownloadImages(Guid[] ids)
        {
            var images = imageRepository.GetImagesById(ids);

            if (images.Count() > 0)
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                MemoryStream outPutStream = new MemoryStream();

                using (ZipFile zip = new ZipFile())
                {
                    foreach (var image in images)
                    {
                        var fileName = image.ImageVirtualPath.Substring(image.ImageVirtualPath.LastIndexOf("\\") + 1);
                        var filePath = HttpContext.Current.Server.MapPath($"~/Image/{fileName}");
                        zip.AddFile(filePath, $"Images_" + DateTime.Now);
                    }

                    zip.CompressionMethod = CompressionMethod.BZip2;
                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                    zip.Save(outPutStream);
                }

                result.Content = new StreamContent(outPutStream);
                var headers = result.Content.Headers;
                outPutStream.Position = 0;
                headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                headers.ContentDisposition.FileName = "zipPictures";
                headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                return result;
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
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
