
using Domain.Entities;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ImagesApi.Controllers
{
    [RoutePrefix("api")]
    public class ImageController : ApiController
    {
        private readonly IImageService imageService;
        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetVersion()
        {
            return Ok("Api Version 1.0.0");
        }


        [HttpPost]
        [Route("upload")]
        public async Task<HttpResponseMessage> UploadImage()
        {
            return Request.CreateResponse(await imageService.SaveImage());
        }


        [HttpPost]
        [Route("uploadImages")]
        public async Task<HttpResponseMessage> UploadImages()
        {
            return Request.CreateResponse(await imageService.SaveImages());
        }


        [HttpGet]
        [Route("listImages")]
        public async Task<IEnumerable<Image>> ListImages()
        {
            return await imageService.ListImages();
        }

        [HttpGet]
        [Route("downloadImage/{id}")]
        public HttpResponseMessage DownloadImage(Guid id)
        {
            return imageService.DownloadImageById(id, Request.CreateResponse());
        }
    }
}