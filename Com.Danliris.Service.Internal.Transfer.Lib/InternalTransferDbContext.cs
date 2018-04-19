using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Configs.TransferDeliveryOrderConfig;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Configs.ExternalTransferOrderConfig;

using Com.Danliris.Service.Internal.Transfer.Lib.Configs.InternalTransferOrderConfigs;
using Com.Danliris.Service.Internal.Transfer.Lib.Configs.TransferRequestConfig;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Configs;
using Com.Danliris.Service.Internal.Transfer.Lib.Models;
using Com.Moonlay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Internal.Transfer.Lib
{
    public class InternalTransferDbContext : BaseDbContext
    {
        public InternalTransferDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TransferDeliveryOrder> TransferDeliveryOrders { get; set; }
        public DbSet<TransferDeliveryOrderItem> TransferDeliveryOrderItems { get; set; }
        public DbSet<TransferDeliveryOrderDetail> TransferDeliveryOrderDetails { get; set; }
        public DbSet<TransferRequest> TransferRequests { get; set; }
        public DbSet<TransferRequestDetail> TransferRequestDetails { get; set; }

        public DbSet<InternalTransferOrder> InternalTransferOrders { get; set; }
        public DbSet<InternalTransferOrderDetail> InternalTransferOrderDetails { get; set; }

        public DbSet<ExternalTransferOrder> ExternalTransferOrders { get; set; }
        public DbSet<ExternalTransferOrderItem> ExternalTransferOrderItems { get; set; }
        public DbSet<ExternalTransferOrderDetail> ExternalTransferOrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TransferDeliveryOrderConfig());
            modelBuilder.ApplyConfiguration(new TransferDeliveryOrderItemConfig());
            modelBuilder.ApplyConfiguration(new TransferDeliveryOrderDetailConfig());
            modelBuilder.ApplyConfiguration(new ExternalTransferOrderConfig());
            modelBuilder.ApplyConfiguration(new ExternalTransferOrderItemConfig());
            modelBuilder.ApplyConfiguration(new ExternalTransferOrderDetailConfig());

            modelBuilder.ApplyConfiguration(new TransferRequestConfig());
            modelBuilder.ApplyConfiguration(new TransferRequestDetailConfig());

            modelBuilder.ApplyConfiguration(new InternalTransferOrderConfig());
            modelBuilder.ApplyConfiguration(new InternalTransferOrderDetailConfig());

            modelBuilder.ApplyConfiguration(new ExternalTransferOrderConfig());
            modelBuilder.ApplyConfiguration(new ExternalTransferOrderItemConfig());
            modelBuilder.ApplyConfiguration(new ExternalTransferOrderDetailConfig());
        }
    }
}
