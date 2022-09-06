using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArandaLogic.General
{
    public interface IGeneral
    {
        bool ValidatedCredials(LoginToken userData);
    }

    public class LoginToken
    {
        public string userName { get; set; } = "ArandaAdmin";
        public string password { get; set; } = "4R4nd44dm1n";
    }
    public class TokenLogic : IGeneral
    {
        public bool ValidatedCredials(LoginToken userData)
        {
            if (userData.userName == "ArandaAdmin" && userData.password == "4R4nd44dm1n")
                return true;
            else
                return false;
        }
    }
}
