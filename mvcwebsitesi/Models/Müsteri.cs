using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mvcwebsitesi.Models
{
    public class Müsteri
    {
        [Key]
        public int MüsteriId { get; set; }

        [Required(ErrorMessage = "!")]
        public string AdSoyad { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "!")]
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "!")]
        public string Sifre { get; set; }

    }
}