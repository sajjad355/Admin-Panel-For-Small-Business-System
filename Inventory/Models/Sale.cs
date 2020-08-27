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
    using System.ComponentModel.DataAnnotations;

    public partial class Sale
    {

        public int id { get; set; }

        [Required(ErrorMessage = "info_id is required")]
        [Display(Name = "info_id:")]
        public int info_id { get; set; }

        [Required(ErrorMessage = "invoice is required")]
        [Display(Name = "invoice:")]
        public string invoice { get; set; }

        [Required(ErrorMessage = "date is required")]
        [Display(Name = "date:")]
        public System.DateTime date { get; set; }

        [Required(ErrorMessage = "product_id is required")]
        [Display(Name = "product_id:")]
        public int product_id { get; set; }

        [Required(ErrorMessage = "quantity is required")]
        [Display(Name = "quantity:")]
        public int quantity { get; set; }

        [Required(ErrorMessage = "total_price is required")]
        [Display(Name = "total_price:")]
        public double total_price { get; set; }

        [Required(ErrorMessage = "unit_price is required")]
        [Display(Name = "unit_price:")]
        public double unit_price { get; set; }

        [Required(ErrorMessage = "category_id is required")]
        [Display(Name = "category_id:")]
        public int category_id { get; set; }

        [Required(ErrorMessage = "pre_unit_price is required")]
        [Display(Name = "pre_unit_price:")]
        public double pre_unit_price { get; set; }

        [Required(ErrorMessage = "remarks is required")]
        [Display(Name = "remarks:")]
        public string remarks { get; set; }

        [Required(ErrorMessage = "new_unit_price is required")]
        [Display(Name = "new_unit_price:")]
        public double new_unit_price { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Information Information { get; set; }
        public virtual Product Product { get; set; }
    }
}
