using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DbUser;
using TechnicalEssementMandiriBE.App_Code;
using TechnicalEssementMandiriBE.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace TechnicalEssementMandiriBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDataContext _userDataKopContext;
        public UserController(UserDataContext userDataContext)
        {
            _userDataKopContext = userDataContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<M_User.DataUser>> datauser()
        {
            List<M_User.DataUser> datas = new List<M_User.DataUser>();
            //var datause = from abb in _userDataKopiContext.Users
            //              group abb by abb.Id_user into pg
            //              join abs in _userDataKopiContext.Userlogins on pg.FirstOrDefault().Id_user equals abs.user_id
            //              select new Datausergroupby
            //              {
            //                  nohp = pg.FirstOrDefault().no_hp,
            //                  email = pg.FirstOrDefault().email,
            //                  password = abs.password,
            //                  username = abs.username,
            //                  id = Convert.ToDecimal(pg.FirstOrDefault().Id_user),
            //                  noktp = pg.FirstOrDefault().noktp,
            //                  namauser = pg.FirstOrDefault().nama_user,
            //              };
            var datause = from abb in _userDataKopContext.Users
                          join abs in _userDataKopContext.Userlogins on abb.Id_user equals abs.user_id
                          select new { abb, abs.password, abs.username };
            var datajadi = await datause.ToListAsync();
            datajadi.ForEach((e) =>
                {
                    M_User.DataUser abb = new M_User.DataUser();
                    abb.nohp = e.abb.no_hp;
                    abb.email = e.abb.email;
                    abb.password = e.password;
                    abb.username = e.username;
                    abb.id = Convert.ToDecimal(e.abb.Id_user);
                    abb.noktp = e.abb.noktp;
                    abb.namauser = e.abb.nama_user;
                    abb.kodereferal = UserCode.Kodereferal(_userDataKopContext, Convert.ToDecimal(e.abb.Id_user));
                    datas.Add(abb);
                });
            return datas;

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InputData([FromBody]M_User.Register register)
        {
            try
            {
                string namauser = string.Empty;
                decimal getiduser = UserCode.Countid(_userDataKopContext);
                decimal iduserlogin = UserCode.Countiduserlogin(_userDataKopContext);
                decimal getidmsvoucher = UserCode.Countidmsreward(_userDataKopContext);
                User abb = new User();
                abb.Id_user = getiduser;
                abb.nama_user = register.nama;
                abb.noktp = register.noktp;
                abb.email = register.email;
                abb.no_hp = register.nohp;
                abb.tgl_input = DateOnly.FromDateTime(DateTime.Today);
                _userDataKopContext.Users.Add(abb);
                _userDataKopContext.SaveChanges();

                if (register.nohpreferal != "" || register.kodereferal != "")
                {
                    
                    if (register.nohpreferal != "")
                    {
                        namauser = UserCode.NamaUser(_userDataKopContext, register.nohpreferal);
                    }
                    else
                    {
                        namauser = (from bb in _userDataKopContext.ms_Vouchers
                                    join cc in _userDataKopContext.Users on bb.id_user equals cc.Id_user
                                    select cc.nama_user).FirstOrDefault();
                    }

                    Ms_voucher ab = new Ms_voucher();
                    ab.id_voucher = getidmsvoucher;
                    ab.id_user = getiduser;
                    ab.kodegenerate = GenerateKodeReferal.Generatekode(namauser);
                    ab.tgl_generate = DateOnly.FromDateTime(DateTime.Today);
                    _userDataKopContext.ms_Vouchers.Add(ab);
                    _userDataKopContext.SaveChanges();
                }
                UserLogin ass = new UserLogin();
                ass.id_userlogin = iduserlogin;
                ass.user_id = getiduser;
                ass.username = GenerateKodeReferal.Generateusername(register.nama);
                ass.password = GenerateKodeReferal.Generatepassword();
                _userDataKopContext.Userlogins.Add(ass);
                
                _userDataKopContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, "sukses");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Deletedatauser(decimal id_user)
        {
            try
            {
                var checkdata = await _userDataKopContext.Users.Where(x => x.Id_user == id_user).FirstOrDefaultAsync();
                if (checkdata != null)
                {
                    _userDataKopContext.Remove(checkdata);
                    _userDataKopContext.SaveChangesAsync();
                }
                return StatusCode(StatusCodes.Status200OK, "sukses");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }


        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser(M_User.Updateuser Updateuser)
        {
            try
            {
                var checkdata = await _userDataKopContext.Users.Where(x => x.Id_user == Updateuser.id).FirstOrDefaultAsync();
                if (checkdata != null)
                {
                    checkdata.nama_user = Updateuser.namauser;
                    _userDataKopContext.SaveChangesAsync();
                }
                return StatusCode(StatusCodes.Status200OK, "sukses");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }


        }

        [Route("alluser"),HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<M_User.DataUserall>> Alluser()
        {
            List<M_User.DataUserall> datas = new List<M_User.DataUserall>();
           
            var datajadi = await _userDataKopContext.Users.Select(x=> new {x.noktp,x.no_hp,x.email,x.Id_user,x.nama_user}).ToListAsync();
            datajadi.ForEach((e) =>
            {
                M_User.DataUserall abb = new M_User.DataUserall();
                abb.nohp = e.no_hp;
                abb.email = e.email;
                abb.id = Convert.ToDecimal(e.Id_user);
                abb.noktp = e.noktp;
                abb.namauser = e.nama_user;
                datas.Add(abb);
            });
            return  datas;
        }
    }
}
