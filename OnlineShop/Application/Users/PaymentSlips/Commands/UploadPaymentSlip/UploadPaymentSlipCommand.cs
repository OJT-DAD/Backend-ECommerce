using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
                ImageUrl = await SaveImage(request.ImageUrl)
            };

            _context.PaymentSlips.Add(entity);

            transactionIndexAsset.Status = Status.WaitingForConfirmation;

            await _context.SaveChangesAsync(cancellationToken);

            return "Success Uploads your payment slip";
        }

        private async Task<string> SaveImage(IFormFile imageUrl)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageUrl.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageUrl.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageUrl.CopyToAsync(fileStream);
            }

            return imageName;
        }
    }
}
