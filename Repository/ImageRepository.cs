using Dapper;
using Domain.Entities;
using Repository.DataContexts;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace Repository
{
    public class ImageRepository : IImageRepository
    {
        public readonly DataContext dataContext;

        public ImageRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public Image GetImageById(Guid id)
        {

            return dataContext.Connection.Query<Image>("SELECT * FROM Images WHERE Id = @Id", new { Id = id}, commandType: CommandType.Text).FirstOrDefault();
        }

        public IEnumerable<Image> GetImagesById(Guid[] ids)
        {
            return dataContext.Connection.Query<Image>("SELECT * FROM Images WHERE Id IN @Ids", new { Ids = ids }, commandType: CommandType.Text);
        }

        public async Task<IEnumerable<Image>> ListImages()
        {
            return await dataContext.Connection.QueryAsync<Image>("SELECT * FROM Images", commandType: CommandType.Text);
        }

        public async Task SaveImage(Image image)
        {

            await dataContext.Connection.ExecuteAsync("INSERT INTO Images (Id, ImageName, ImagePath, ImageVirtualPath, ContentType) VALUES (@Id, @ImageName, @ImagePath, @ImageVirtualPath, @ContentType)", new
            {
                Id = image.ImageId,
                ImageName = image.ImageName,
                ImagePath = image.ImagePath,
                ContentType = image.ContentType,
                ImageVirtualPath = image.ImageVirtualPath
            }, commandType: CommandType.Text);
        }
    }
}

