using BLL.DTO;
using DAL.EF.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class JobServices
    {
        GenericRepo<Job> repo;
        public JobServices(GenericRepo<Job> repo)
        {
            this.repo = repo;
        }

        public List<JobDTO> Get()
        {
            try
            {
                List<Job> list = repo.Get();
                return MapperConfig.GetMapper().Map<List<JobDTO>>(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<JobDTO>();
            }
        }

        public JobDTO Get(int id)
        {
            try
            {
                var data = repo.Get(id);
                return MapperConfig.GetMapper().Map<JobDTO>(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool Add(JobDTO user)
        {
            try
            {
                Job data = MapperConfig.GetMapper().Map<Job>(user);
                return repo.Add(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(JobDTO user)
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
