using Com.Moonlay.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Com.Danliris.Service.Internal.Transfer.Lib.Models;
using Com.Danliris.Service.Internal.Transfer.Lib.Configs;

namespace Com.Danliris.Service.Internal.Transfer.Lib
{
    public class InternalTransferDbContext : BaseDbContext
    {
        public InternalTransferDbContext(DbContextOptions<InternalTransferDbContext> options) : base(options)
        {
        }

        public DbSet<TransferDeliveryOrder> TransferDeliveryOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TransferDeliveryOrderConfig());
        }
    }


}