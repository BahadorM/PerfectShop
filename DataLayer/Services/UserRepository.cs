using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;

namespace DataLayer
{
    public class UserRepository : IUserRepository
    {
        DbShopContext _db;
        public UserRepository(DbShopContext db)
        {
            this._db = db;
        }
        public IEnumerable<Users> Get()
        {
            return _db.Users;
        }
        public Users GetByID(int userID)
        {
            return _db.Users.Single(u => u.UserID == userID);
        }
        public bool Delete(Users user)
        {
            try
            {
                _db.Entry(user).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int userID)
        {
            Delete(GetByID(userID));
            return true;
        }



        public bool Insert(Users user)
        {
            try
            {
                _db.Entry(user).State = EntityState.Added;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Users user)
        {
            try
            {
                _db.Entry(user).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool AnyByEmail(string email)
        {
            if (_db.Users.Any(u => u.Email == email.ToLower().Trim()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Users GetUserByActiveCode(string activeCode)
        {
            return _db.Users.Single(u => u.AciveCode == activeCode);
        }

        public Users GetUserByEmailAndPass(string Email, string password)
        {
            return _db.Users.SingleOrDefault(u => u.Email == Email.ToLower().Trim() && u.Password == password);
        }

        public Users GetByEmail(string email)
        {
            return _db.Users.SingleOrDefault(u => u.Email == email);
        }

        public string GetUserNamebyID(string UserID)
        {
            int Userid = Convert.ToInt32(UserID);
            return _db.Users.Where(u => u.UserID == Userid).Select(p => p.UserName).SingleOrDefault();
        }

        public string GetUserNamebyID(int UserID)
        {
            return _db.Users.Where(u => u.UserID == UserID).Select(p => p.UserName).SingleOrDefault();
        }

        public Users GetUserByUserName(string userName)
        {
            return _db.Users.Single(u => u.UserName == userName);
        }

        public string[] GetUserRolesByUserName(string userName)
        {
            return _db.Users.Where(u => u.UserName == userName).Select(p => p.Roles.RoleName).ToArray();
        }
    }
}
