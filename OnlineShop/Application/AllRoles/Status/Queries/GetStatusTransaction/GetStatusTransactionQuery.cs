using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.PaymentSlips.Queries.GetConfirmationPaymentSlip
{
    public class GetStatusTransactionQuery : IRequest<string>
    {
        public int TransactionIndexId { get; set; }
    }

    public class GetStatusTransactionQueryHandler : IRequestHandler<GetStatusTransactionQuery, string>
    {
        private readonly IApplicationDbContext _context;

        public GetStatusTransactionQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetStatusTransactionQuery request, CancellationToken cancellationToken)
        {
            var validationExist = await _context.TransactionIndexs.AnyAsync(x => x.Id == request.TransactionIndexId);
            if (!validationExist)
                throw new NotFoundException(nameof(TransactionIndex), request.TransactionIndexId);

            var transactionIndexAsset = await _context.TransactionIndexs
                .FindAsync(request.TransactionIndexId);

            if (transactionIndexAsset.Status == Status.PaymentSlipInvalid)
                return "Payment slip yang kamu masukkan tidak valid. Mohon masukkan ulang payment slip!";

            if (transactionIndexAsset.Status == Status.OnProccess)
                return "Pembayaran telah berhasil, barang sedang diproses";

            if (transactionIndexAsset.Status == Status.OnDelivery)
                return "Barang sedang dalam perjalanan";

            if (transactionIndexAsset.Status == Status.Arrived)
                return "Barang telah sampai di tempat tujuan";

            if (transactionIndexAsset.Status == Status.ItemReceived)
                return "Barang telah diterima";

            if (transactionIndexAsset.Status == Status.TransactionCancel)
                return "Transaksi dibatalkan";

            if (transactionIndexAsset.Status == Status.TransactionFailed)
                return "Transaksi gagal";

            if (transactionIndexAsset.Status == Status.WaitingForConfirmation)
                return "Menunggu konfirmasi dari seller";

            if (transactionIndexAsset.Status == Status.WaitingForPayment)
                return "Menunggu pembayaran";

            return "Status Kosong";
        }
    }
}
