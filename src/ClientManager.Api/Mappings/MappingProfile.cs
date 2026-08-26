using AutoMapper;
using ClientManager.Api.DTOs;
using ClientManager.Core.Entities;

namespace ClientManager.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Cliente Mappings
        CreateMap<Cliente, ClienteDto>();
        CreateMap<CreateClienteDto, Cliente>();
        CreateMap<UpdateClienteDto, Cliente>();

        // Projeto Mappings
        CreateMap<Projeto, ProjetoDto>()
            .ForMember(dest => dest.ClienteNome, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.Nome : null));
        CreateMap<CreateProjetoDto, Projeto>();
        CreateMap<UpdateProjetoDto, Projeto>();
    }
}
