using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mvcwebsitesi.Models
{
    public class CiceklerUrun
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Ürün")]
        [Required(ErrorMessage = "!")]
        public string UrunAd { get; set; }

        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "!")]
        public string Aciklama { get; set; }

        [Required(ErrorMessage = "!")]
        public double Fiyat { get; set; }

        [Required(ErrorMessage = "!")]
        public string Resim { get; set; }


        [Display(Name = "Onay Durumu")]
        [Required(ErrorMessage = "!")]
        public bool OnayliMi { get; set; }


        [Display(Name = "Öncelik Durumu")]
        [Required(ErrorMessage = "!")]
        public bool ÖncelikliMi { get; set; }


        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "!")]
        public int KategoriId { get; set; }


        [Display(Name = "Ana Sayfa Durumu")]
        [Required(ErrorMessage = "!")]
        public bool AnaSayfaMi { get; set; }

        public virtual Kategori Kategori { get; set; }

    }
}