namespace TechnicalEssementMandiriBE.Model
{
    public class M_User
    {
        public class Register
        {
            public string nama { get; set; }
            public string nohp { get; set; }
            public string email { get; set; }
            public string noktp { get; set; }
            public string nohpreferal { get; set; }
            public string kodereferal { get; set; }
        }
        public class DataUser
        {
            public decimal id { get; set; }
            public string namauser { get; set; }
            public string nohp { get; set; }
            public string email { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string noktp { get; set; }
            public string kodereferal { get; set; }
        }
        public class DataUserall
        {
            public decimal id { get; set; }
            public string namauser { get; set; }
            public string nohp { get; set; }
            public string email { get; set; }
            public string noktp { get; set; }
        }
        public class Datausergroupby
        {
            public decimal id { get; set; }
            public string namauser { get; set; }
            public string nohp { get; set; }
            public string email { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string noktp { get; set; }
        }
        public class Updateuser
        {
            public decimal id { get; set; }
            public string namauser { get; set; }
        }
    }
}
