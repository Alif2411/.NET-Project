
using BLL.DTO;
using DAL.EF.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class UserServices
    {
        GenericRepo<User> repo;
        UserRepo repo2;
        public UserServices(GenericRepo<User> repo, UserRepo repo2)
        {
            this.repo = repo;
            this.repo2 = repo2;
        }

        public List<UserDTO> Get()
        {
            try
            {
                List<User> list = repo.Get();
                return MapperConfig.GetMapper().Map<List<UserDTO>>(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<UserDTO>();
            }
        }

        public UserDTO Get(int id) {
            try
            {
                var data = repo.Get(id);
                return MapperConfig.GetMapper().Map<UserDTO>(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool Add(UserDTO user)
        {
            try
            {
                var found = repo2.FindByName(user.UserName);
                if (found != null)
                {
                    return false;
                }
                User data = MapperConfig.GetMapper().Map<User>(user);
                return repo.Add(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(UserDTO user)
        {
            try
            {
                var existing = repo.Get(user.ID);
                if (existing == null) return false;

                MapperConfig.GetMapper().Map(user, existing);
                return repo.Update(existing);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Delete(int id) {
            try
            {
                return repo.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Login(string username, string password) {
            try
            {
                var res = repo2.Match(username, password);
                if (res != null) {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
