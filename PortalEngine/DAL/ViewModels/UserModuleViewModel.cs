using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalEngine.DAL.Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PortalEngine.DAL.ViewModels
{
    public class UserModuleViewModel
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public virtual int ID { get; set; }

        [DataType(DataType.Text)]
        [StringLength(20), Required]
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
        public virtual System.DateTime DateCreated { get; set; }

        public virtual bool Status { get; set; }
       
    }
}