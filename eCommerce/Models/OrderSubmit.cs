//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eCommerce.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderSubmit
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> SmartphoneId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Smartphone Smartphone { get; set; }
        public virtual User User { get; set; }
    }
}
