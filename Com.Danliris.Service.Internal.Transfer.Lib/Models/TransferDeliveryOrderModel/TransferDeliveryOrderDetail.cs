using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferDeliveryOrderService;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel
{
    public class TransferDeliveryOrderDetail : StandardEntity, IValidatableObject
    {
        public int DOItemId { get; set; }
        public int ETODetailId { get; set; }
        public int ITODetailId { get; set; }
        public int TRDetailId { get; set; }
        public string ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Grade { get; set; }
        public string ProductRemark { get; set; }
        public int RequestedQuantity { get; set; }
        public string UomId { get; set; }
        public string UomUnit { get; set; }
        public int DOQuantity { get; set; }
        public int ShippingOrderQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public virtual TransferDeliveryOrderItem transferDeliveryOrderItem { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}