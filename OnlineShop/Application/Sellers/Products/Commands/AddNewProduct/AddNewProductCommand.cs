using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Commands.AddNewProduct
{
    public class AddNewProductCommand : IRequest<int>
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int StockProduct { get; set; }
    }

    public class AddNewProductCommandHandler : IRequestHandler<AddNewProductCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public AddNewProductCommandHandler(IApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<int> Handle(AddNewProductCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;

            var entity = new Product
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = await SaveImage(request.ImageUrl),
                Price = request.Price,
                StoreId = request.StoreId,
                DateAddedOrUpdated = now
            };

            _context.Products.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            var stock = new Stock
            {
                StockProduct = request.StockProduct,
                ProductId = entity.Id
            };

            _context.Stocks.Add(stock);

            return await _context.SaveChangesAsync(cancellationToken);
   
        }

        private async Task<string> SaveImage(IFormFile imageUrl)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageUrl.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageUrl.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images/Product", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.OpenOrCreate))
            {
                await imageUrl.CopyToAsync(fileStream);
            }

            return imageName;
        }
    }
}
