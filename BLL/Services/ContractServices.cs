using BLL.DTO;
using DAL.EF.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class ContractServices
    {
        GenericRepo<Contract> repo;
        public ContractServices(GenericRepo<Contract> repo)
        {
            this.repo = repo;
        }

        public List<ContractDTO> Get()
        {
            try
            {
                List<Contract> list = repo.Get();
                return MapperConfig.GetMapper().Map<List<ContractDTO>>(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ContractDTO>();
            }
        }

        public ContractDTO Get(int id)
        {
            try
            {
                var data = repo.Get(id);
                return MapperConfig.GetMapper().Map<ContractDTO>(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool Add(ContractDTO user)
        {
            try
            {
                Contract data = MapperConfig.GetMapper().Map<Contract>(user);
                return repo.Add(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(ContractDTO user)
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
