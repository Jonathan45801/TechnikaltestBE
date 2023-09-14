using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbUser
{
    public class Ms_voucher
    {
        public decimal? id_voucher { get; set; }
        public decimal? id_user { get; set; }
        [MaxLength(88)]
        public string? kodegenerate { get; set; }
        public DateOnly? tgl_generate { get; set; }
        public User User { get; set; }
    }
}
