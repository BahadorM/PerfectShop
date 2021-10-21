using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IUserRepository
    {
        IEnumerable<Users> Get();
        Users GetByID(int userID);
        Users GetByEmail(string email);
        Users GetUserByUserName(string userName);
        Users GetUserByActiveCode(string activeCode);
        Users GetUserByEmailAndPass(string Email,string password);
        string[] GetUserRolesByUserName(string userName);
        string GetUserNamebyID(string UserID);
        string GetUserNamebyID(int UserID);
        bool Insert(Users user);
        bool Delete(Users user);
        bool Delete(int userID);
        bool Update(Users user);
        bool AnyByEmail(string email);

        
    }
}
