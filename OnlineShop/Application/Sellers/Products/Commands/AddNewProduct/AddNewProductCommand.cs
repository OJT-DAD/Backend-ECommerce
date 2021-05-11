using Application.Common.Interfaces;
using Domain.Entities;
using Firebase.Auth;
using Firebase.Storage;
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

        //Configure Firebase
        private static string apiKey = "AIzaSyDv0Joa3kL8sy39xyr7kUF-JBKGskMAhsQ";
        private static string Bucket = "onlineshop-a9386.appspot.com";
        public static string authEmail = "ardiansyahrafi4@gmail.com";
        public static string authPassword = "ardiansyahrafi4";
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
                ImageUrl = await SaveImage(request.ImageUrl, cancellationToken),
                ImageName = ImageName(request.ImageUrl),
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

        private string ImageName(IFormFile imageUrl)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageUrl.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmdd") + Path.GetExtension(imageUrl.FileName);
            return imageName;
        }

        private async Task<string> SaveImage(IFormFile imageUrl, CancellationToken cancellationToken)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageUrl.FileName).Take(10).ToArray()).Replace(' ', '-');
            FileStream fileStream = null;
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageUrl.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images/Product", imageName);
            using (fileStream = new FileStream(imagePath, FileMode.OpenOrCreate))
            {
                await imageUrl.CopyToAsync(fileStream);
            }

            fileStream = new FileStream(Path.Combine(imagePath), FileMode.Open);

            //firebase uploading stuffs
            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(authEmail, authPassword);
            var upload = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("Assets")
                .Child("Products")
                .Child($"{imageUrl.FileName}")
                .PutAsync(fileStream, cancellationToken);

            upload.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            return await upload;
        }
    }
}
