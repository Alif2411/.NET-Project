using BLL.DTO;
using DAL.EF.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class ProposalServices
    {
        GenericRepo<Proposal> repo;
        ProposalRepo repo2;
        public ProposalServices(GenericRepo<Proposal> repo, ProposalRepo repo2)
        {
            this.repo = repo;
            this.repo2 = repo2;
        }

        public List<ProposalDTO> Get()
        {
            try
            {
                List<Proposal> list = repo.Get();
                return MapperConfig.GetMapper().Map<List<ProposalDTO>>(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ProposalDTO>();
            }
        }

        public ProposalDTO Get(int id)
        {
            try
            {
                var data = repo.Get(id);
                return MapperConfig.GetMapper().Map<ProposalDTO>(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool Add(ProposalDTO user)
        {
            try
            {
                Proposal data = MapperConfig.GetMapper().Map<Proposal>(user);
                return repo.Add(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(ProposalDTO user)
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

        public List<ProposalDTO> GetProposals(JobDTO job)
        {
            try
            {
                List<Proposal> list = repo2.GetProposals(MapperConfig.GetMapper().Map<Job>(job));
                return MapperConfig.GetMapper().Map<List<ProposalDTO>>(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ProposalDTO>();
            }
        }

        public List<ProposalDTO> GetProposalsInRange(JobDTO job, int? min, int? max)
        {
            try
            {
                List<Proposal> list = repo2.GetProposalsInRange(MapperConfig.GetMapper().Map<Job>(job), min, max);
                return MapperConfig.GetMapper().Map<List<ProposalDTO>>(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ProposalDTO>();
            }
        }

        



    }
}
