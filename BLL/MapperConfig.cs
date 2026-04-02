using AutoMapper;
using BLL.DTO;
using DAL.EF.Models;

namespace BLL
{
    public class MapperConfig
    {
        static MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDTO>().ReverseMap();
            cfg.CreateMap<Role, RoleDTO>().ReverseMap();
            cfg.CreateMap<Proposal, ProposalDTO>().ReverseMap();
            cfg.CreateMap<Job, JobDTO>().ReverseMap();
            cfg.CreateMap<Payment, PaymentDTO>().ReverseMap();
            cfg.CreateMap<Contract, ContractDTO>().ReverseMap();
        });

        public static Mapper GetMapper()
        {
            return new Mapper(config);
        }

    }
}
