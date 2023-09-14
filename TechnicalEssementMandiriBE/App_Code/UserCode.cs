using Microsoft.EntityFrameworkCore;
using DbUser;
namespace TechnicalEssementMandiriBE.App_Code
{

    public class UserCode
    {
        private readonly UserDataContext _userDataKopiContext;
        public UserCode(UserDataContext userDataKopiContext)
        {
            _userDataKopiContext = userDataKopiContext;
        }

        public static decimal Countid(UserDataContext context)
        {
            return context.Users.Select(x=>x.Id_user).Count() + 1;
        }
        
        public static decimal Countidmsreward(UserDataContext context)
        {
            return context.ms_Vouchers.Select(x => x.id_voucher).Count() + 1;
        }
        public static string NamaUser(UserDataContext context,string nohp)
        {
            return context.Users.Where(x => x.no_hp == nohp).FirstOrDefault().nama_user;
        }
        public static decimal Countiduserlogin(UserDataContext context)
        {
            return context.Userlogins.Select(x => x.id_userlogin).Count() + 1;
        }
        public static string Kodereferal(UserDataContext context,decimal userid)
        {
            string namakodegenerate = string.Empty;
            return namakodegenerate = context.ms_Vouchers.Where(x => x.id_user == userid).FirstOrDefault() == null ? "": context.ms_Vouchers.Where(x => x.id_user == userid).FirstOrDefault().kodegenerate;
        }
    }
}
