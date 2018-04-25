using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferDeliveryOrderService;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel
{
    public class TransferDeliveryOrder : StandardEntity, IValidatableObject
    {
        public string DONo { get; set; }
        public DateTime DOdate { get; set; }
        public string OrderDivisionId { get; set; }
        public string OrderDivisionCode { get; set; }
        public string OrderDivisionName { get; set; }
        public string SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string Remark { get; set; }
        //public bool IsPosted { get; set; }
        
        public virtual ICollection<TransferDeliveryOrderItem> TransferDeliveryOrderItem { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            TransferDeliveryOrderService service = validationContext.GetService<TransferDeliveryOrderService>();

            if (service.DbSet.Count(r => r.DONo != this.DONo && r._IsDeleted.Equals(false)) > 0)
                yield return new ValidationResult("Nomor Surat Jalan sudah ada", new List<string> { "Code" });
        }
    }
}