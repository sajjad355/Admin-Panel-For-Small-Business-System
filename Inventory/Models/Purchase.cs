//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ATP2_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Purchase
    {
        public int id { get; set; }
        public int info_id { get; set; }
        public string invoice { get; set; }
        public System.DateTime date { get; set; }
        public System.DateTime expire_Date { get; set; }
        public int product_Id { get; set; }
        public int quantity { get; set; }
        public double pre_unit_price { get; set; }
        public string remarks { get; set; }
        public double new_unit_price { get; set; }
        public double total_price { get; set; }
        public int catagory_id { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Information Information { get; set; }
        public virtual Product Product { get; set; }
    }
}