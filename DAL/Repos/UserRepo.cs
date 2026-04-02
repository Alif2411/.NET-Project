using DAL.EF;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class UserRepo
    {
        Context db;
        public UserRepo(Context db) {  this.db = db; }

        public User Match(string username, string password)
        {
            try
            {
                var found = (from s in db.Users where s.UserName == username && s.Password == password select s).FirstOrDefault();
                if (found != null ) {return found; }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public User FindByName(string username)
        {
            try
            {
                var found = (from s in db.Users where s.UserName == username select s).FirstOrDefault();
                if (found != null) { return found; }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
