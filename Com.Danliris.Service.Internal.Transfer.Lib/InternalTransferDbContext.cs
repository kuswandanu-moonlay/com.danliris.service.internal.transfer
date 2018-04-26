
using Com.Danliris.Service.Internal.Transfer.Lib.Configs.InternalTransferOrderConfigs;
using Com.Danliris.Service.Internal.Transfer.Lib.Configs.TransferRequestConfig;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Configs;
using Com.Danliris.Service.Internal.Transfer.Lib.Configs.ExternalTransferOrderConfigs;
using Com.Danliris.Service.Internal.Transfer.Lib.Models;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using Com.Moonlay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Internal.Transfer.Lib.Configs.ShippingOrderConfigs;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.ShippingOrderModel;

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

        public DbSet<ExternalTransferOrder> ExternalTransferOrders { get; set; }
        public DbSet<ExternalTransferOrderItem> ExternalTransferOrderItems { get; set; }
        public DbSet<ExternalTransferOrderDetail> ExternalTransferOrderDetails { get; set; }

        public DbSet<ShippingOrder> ShippingOrders { get; set; }
        public DbSet<ShippingOrderItem> ShippingOrderItems { get; set; }
        public DbSet<ShippingOrderDetail> ShippingOrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TransferRequestConfig());
            modelBuilder.ApplyConfiguration(new TransferRequestDetailConfig());

            modelBuilder.ApplyConfiguration(new InternalTransferOrderConfig());
            modelBuilder.ApplyConfiguration(new InternalTransferOrderDetailConfig());

            modelBuilder.ApplyConfiguration(new ExternalTransferOrderConfig());
            modelBuilder.ApplyConfiguration(new ExternalTransferOrderItemConfig());
            modelBuilder.ApplyConfiguration(new ExternalTransferOrderDetailConfig());

            modelBuilder.ApplyConfiguration(new ShippingOrderConfig());
            modelBuilder.ApplyConfiguration(new ShippingOrderDetailConfig());
            modelBuilder.ApplyConfiguration(new ShippingOrderItemConfig());
        }
    }
}
