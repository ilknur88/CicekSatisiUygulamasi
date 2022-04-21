using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace mvcwebsitesi.Models
{
    public class Kategori
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "!")]
        public string KategoriAlani { get; set; }
        public virtual List<CiceklerUrun> CiceklerUruns { get; set; }


    }
}