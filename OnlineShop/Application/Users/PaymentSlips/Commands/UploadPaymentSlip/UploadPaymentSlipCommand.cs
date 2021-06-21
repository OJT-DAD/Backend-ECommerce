using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
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

namespace Application.Users.PaymentSlips.Commands.UploadPaymentSlip
{
    public class UploadPaymentSlipCommand : IRequest<string>
    {
        public int TransactionIndexId { get; set; }
        public IFormFile ImageUrl { get; set; }
    }

    public class UploadPaymentSlipCommandHandler : IRequestHandler<UploadPaymentSlipCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        //Configure Firebase
        private static string apiKey = "AIzaSyDv0Joa3kL8sy39xyr7kUF-JBKGskMAhsQ";
        private static string Bucket = "onlineshop-a9386.appspot.com";
        public static string authEmail = "ardiansyahrafi4@gmail.com";
        public static string authPassword = "ardiansyahrafi4";
        public UploadPaymentSlipCommandHandler(IApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<string> Handle(UploadPaymentSlipCommand request,CancellationToken cancellationToken)
        {
            if (!_context.TransactionIndexs.Any(x => x.Id == request.TransactionIndexId))
                throw new NotFoundException(nameof(TransactionIndex), request.TransactionIndexId);

            var transactionIndexAsset = await _context.TransactionIndexs
                .FindAsync(request.TransactionIndexId);

            var entity = new PaymentSlip
            {
                TransactionIndexId = request.TransactionIndexId,
                PaymentSlipImageName = ImageName(request.ImageUrl),
                PaymentSlipImageUrl = await SaveImage(request.ImageUrl, cancellationToken)
            };

            _context.PaymentSlips.Add(entity);

            transactionIndexAsset.Status = Status.WaitingForConfirmation;

            await _context.SaveChangesAsync(cancellationToken);

            return "Success Uploads your payment slip";
        }

        private string ImageName(IFormFile imageUrl)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageUrl.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageUrl.FileName);

            return imageName;
        }

        private async Task<string> SaveImage(IFormFile imageUrl, CancellationToken cancellationToken)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageUrl.FileName).Take(10).ToArray()).Replace(' ', '-');
            FileStream fileStream = null;
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageUrl.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images/PaymentSlip", imageName);
            using (fileStream = new FileStream(imagePath, FileMode.Create))
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
                .Child("PaymentSlips")
                .Child($"{imageUrl.FileName}")
                .PutAsync(fileStream, cancellationToken);

            upload.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            return await upload;
        }
    }
}
