using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbUser
{
    public class User
    {
        public decimal? Id_user { get; set; }
        [MaxLength(100)]
        public string? nama_user { get; set; }
        [MaxLength(100)]
        public string? no_hp { get; set; }
        [MaxLength(100)]
        public string? email { get; set; }
        public string? password { get; set; }
        public string? noktp { get; set; }
        public DateOnly? tgl_input { get; set; }
        public List<Ms_voucher> Fk_voucherss { get; set; }
        public List<UserLogin> Fk_userlogins { get; set; }
    }
   
}
