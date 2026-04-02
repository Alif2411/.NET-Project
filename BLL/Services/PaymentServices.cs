using BLL.DTO;
using DAL.EF.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class PaymentServices
    {
        GenericRepo<Payment> repo;
        public PaymentServices(GenericRepo<Payment> repo)
        {
            this.repo = repo;
        }

        public List<PaymentDTO> Get()
        {
            try
            {
                List<Payment> list = repo.Get();
                return MapperConfig.GetMapper().Map<List<PaymentDTO>>(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<PaymentDTO>();
            }
        }

        public PaymentDTO Get(int id)
        {
            try
            {
                var data = repo.Get(id);
                return MapperConfig.GetMapper().Map<PaymentDTO>(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool Add(PaymentDTO user)
        {
            try
            {
                Payment data = MapperConfig.GetMapper().Map<Payment>(user);
                return repo.Add(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(PaymentDTO user)
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
