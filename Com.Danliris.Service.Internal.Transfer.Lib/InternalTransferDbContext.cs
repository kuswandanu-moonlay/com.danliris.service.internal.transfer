
using Com.Danliris.Service.Internal.Transfer.Lib.Configs.InternalTransferOrderConfigs;
using Com.Danliris.Service.Internal.Transfer.Lib.Configs.TransferRequestConfig;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;
using Com.Moonlay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Internal.Transfer.Lib
{
    public class InternalTransferDbContext : BaseDbContext
    {
        public InternalTransferDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TransferRequest> TransferRequests { get; set; }
        public DbSet<TransferRequestDetail> TransferRequestDetails { get; set; }

        public DbSet<InternalTransferOrder> InternalTransferOrders { get; set; }
        public DbSet<InternalTransferOrderDetail> InternalTransferOrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TransferRequestConfig());
            modelBuilder.ApplyConfiguration(new TransferRequestDetailConfig());

            modelBuilder.ApplyConfiguration(new InternalTransferOrderConfig());
            modelBuilder.ApplyConfiguration(new InternalTransferOrderDetailConfig());
        }
    }
}
