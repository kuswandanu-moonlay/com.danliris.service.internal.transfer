using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferDeliveryOrderService;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel
{
    public class TransferDeliveryOrderItem : StandardEntity, IValidatableObject
    {
        public int DOId { get; set; }
        public string ETOId { get; set; }
        public string ETONo { get; set; }
        public string TRId { get; set; }
        public string TRNo { get; set; }
        public string ITOId { get; set; }
        public string ITONo { get; set; }

        public virtual TransferDeliveryOrder transferDeliveryOrder { get; set; }
        public virtual ICollection<TransferDeliveryOrderDetail> transferDeliveryOrderDetail { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}