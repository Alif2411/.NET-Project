using DAL.EF;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class ProposalRepo
    {
        Context db;
        public ProposalRepo(Context db) { this.db = db; }

        public List<Proposal> GetProposals(Job job)
        {
            try
            {
                var found = (from s in db.Proposals where s.JobId == job.ID select s).ToList();
                if (found != null) { return (List<Proposal>)found; }
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

        public List<Proposal> GetProposalsInRange(Job job, int? min, int? max )
        {
            try
            {
                var found = (from s in db.Proposals where s.JobId == job.ID select s).ToList();
                if (min != null && max == null) {
                    found = (from s in db.Proposals where s.JobId == job.ID && s.BidAmount >= min select s).ToList();
                }

                else if (max != null && min == null)
                {
                    found = (from s in db.Proposals where s.JobId == job.ID && s.BidAmount <= max select s).ToList();
                }
                
                else
                {
                    found = (from s in db.Proposals where s.JobId == job.ID && s.BidAmount >= min && s.BidAmount <= max select s).ToList();
                }
                if (found != null) { return (List<Proposal>)found; }
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
