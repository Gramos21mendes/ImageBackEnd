using System;
using System.Configuration;

namespace Domain.Entities
{
    public class Image
    {
        public Image(Guid id, string imageName, string imagePath, string imageVirtualPath, string contentType) : this(imageName, imagePath, contentType)
        {
            ImageId = id;
            ImageName = imageName;
            ImagePath = imagePath;
            ContentType = contentType;
            ImageVirtualPath = imageVirtualPath;
        }

        public Image(string imageName, string imagePath, string contentType)
        {
            ImageId = Guid.NewGuid();
            ImageName = imageName;
            ImagePath = imagePath;
            ImageVirtualPath = AlterateToVirtualPath(imagePath);
            ContentType = contentType;
        }


        public Guid ImageId { get; set; }
        public string ImageName { get; private set; }
        public string ImageVirtualPath { get; set; }
        public string ImagePath { get; set; }
        public string ContentType { get; set; }

        public string AlterateToVirtualPath(string path)
        {
            //return $"{ConfigurationManager.AppSettings["VirtualPath"]}{path.Substring(path.LastIndexOf("\\Image") + 1)}";
            return $"{ConfigurationManager.AppSettings["VirtualPathHome"]}{path.Substring(path.LastIndexOf("\\Image") + 1)}";
        }

    }
}
