using AutoMapper;
using BLL.DTO;
using DAL.EF.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class RoleServices
    {
        private readonly GenericRepo<Role> repo;

        public RoleServices(GenericRepo<Role> repo)
        {
            this.repo = repo;
        }

        public List<RoleDTO> Get()
        {
            var data = repo.Get();
            return MapperConfig.GetMapper().Map<List<RoleDTO>>(data);
        }

        public RoleDTO Get(int id)
        {
            var data = repo.Get(id);
            return MapperConfig.GetMapper().Map<RoleDTO>(data);
        }

        public bool Add(RoleDTO user)
        {
            try
            {
                Role data = MapperConfig.GetMapper().Map<Role>(user);
                return repo.Add(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(RoleDTO user)
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

        public bool Delete(int id)
        {
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
    }
}
