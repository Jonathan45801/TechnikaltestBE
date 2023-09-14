namespace TechnicalEssementMandiriBE.App_Code
{
    public class GenerateKodeReferal
    {
        public static string Generatekode(string nama)
        {            
            int leng = nama.Length;
            string hasilgenerate = nama.Substring(0, leng > 4 ? 4 : leng) + DateTime.Today.ToString("dd") + DateTime.Today.ToString("MM");
            return hasilgenerate;
        }
        public static string Generateusername(string nama)
        {
            int leng = nama.Length;
            string hasilgenerate = nama.Substring(0, leng > 4 ? 4 : leng);
            return hasilgenerate;

        }
        public static string Generatepassword()
        {
            string defaultsistem = "test111";
            return defaultsistem;
        }
    }
}
