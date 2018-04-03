using Com.Danliris.Service.Internal.Transfer.Lib.Services;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Com.Danliris.Service.Internal.Transfer.Lib.Models
{
    public class TransferDeliveryOrder : StandardEntity, IValidatableObject
    {
        public string Code { get; set; }
        public DateTime DeliveryOrderDate { get; set; }
        public DateTime ArrivedDate { get; set; }
        public string SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string Remark { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            TransferDeliveryOrderService service = validationContext.GetService<TransferDeliveryOrderService>();

            if (service.DbSet.Count(r => r.Code != this.Code && r._IsDeleted.Equals(false)) > 0)
                yield return new ValidationResult("Nomor Surat Jalan sudah ada", new List<string> { "Code" });
        }
    }
}