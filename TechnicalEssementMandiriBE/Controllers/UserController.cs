using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DbUser;
using TechnicalEssementMandiriBE.App_Code;
using TechnicalEssementMandiriBE.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using static TechnicalEssementMandiriBE.Model.M_User;
using System.Data;

namespace ptnirvanaindonesiaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDataContext _userDataKopiContext;
        public UserController(UserDataContext userDataKopiContext)
        {
            _userDataKopiContext = userDataKopiContext;
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
            var datause = from abb in _userDataKopiContext.Users
                          join abs in _userDataKopiContext.Userlogins on abb.Id_user equals abs.user_id
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
                    abb.kodereferal = UserCode.Kodereferal(_userDataKopiContext, Convert.ToDecimal(e.abb.Id_user));
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
                decimal getiduser = UserCode.Countid(_userDataKopiContext);
                User abb = new User();
                abb.Id_user = getiduser;
                abb.nama_user = register.nama;
                abb.noktp = register.noktp;
                abb.email = register.email;
                abb.no_hp = register.nohp;
                abb.tgl_input = DateOnly.FromDateTime(DateTime.Today);
                _userDataKopiContext.Users.Add(abb);
                _userDataKopiContext.SaveChangesAsync();
                if (register.nohpreferal != "" || register.kodereferal != "")
                {
                    decimal getidmsvoucher = UserCode.Countidmsreward(_userDataKopiContext);
                    if (register.nohpreferal != "")
                    {
                        namauser = UserCode.NamaUser(_userDataKopiContext, register.nohpreferal);
                    }
                    else
                    {
                        namauser = (from bb in _userDataKopiContext.ms_Vouchers
                                    join cc in _userDataKopiContext.Users on bb.id_user equals cc.Id_user
                                    select cc.nama_user).FirstOrDefault();
                    }

                    Ms_voucher ab = new Ms_voucher();
                    ab.id_voucher = getidmsvoucher;
                    ab.id_user = getiduser;
                    ab.kodegenerate = GenerateKodeReferal.Generatekode(namauser);
                    ab.tgl_generate = DateOnly.FromDateTime(DateTime.Today);
                    _userDataKopiContext.ms_Vouchers.Add(ab);
                    _userDataKopiContext.SaveChangesAsync();
                   
                }
                UserLogin ass = new UserLogin();
                ass.id_userlogin = UserCode.Countiduserlogin(_userDataKopiContext);
                ass.user_id = getiduser;
                ass.username = GenerateKodeReferal.Generateusername(register.nama);
                ass.password = GenerateKodeReferal.Generatepassword();
                _userDataKopiContext.Userlogins.Add(ass);
                _userDataKopiContext.SaveChangesAsync();
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
                var checkdata = await _userDataKopiContext.Users.Where(x => x.Id_user == id_user).FirstOrDefaultAsync();
                if (checkdata != null)
                {
                    _userDataKopiContext.Remove(checkdata);
                    _userDataKopiContext.SaveChangesAsync();
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
                var checkdata = await _userDataKopiContext.Users.Where(x => x.Id_user == Updateuser.id).FirstOrDefaultAsync();
                if (checkdata != null)
                {
                    checkdata.nama_user = Updateuser.namauser;
                    _userDataKopiContext.SaveChangesAsync();
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
           
            var datajadi = await _userDataKopiContext.Users.Select(x=> new {x.noktp,x.no_hp,x.email,x.Id_user,x.nama_user}).ToListAsync();
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
