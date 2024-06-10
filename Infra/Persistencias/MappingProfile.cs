using AutoMapper;
using Dominio.Argumentos;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistencias
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           
            CreateMap<CentroCusto, DTOCentroCusto>().ReverseMap();
            CreateMap<Lancamento, DTOLancamento>().ReverseMap().ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => (Dominio.Entidades.LancamentoTipo)src.Tipo));
            CreateMap<Receita, DTOReceita>().ReverseMap();
            CreateMap<Usuario, DTOUsuario>().ReverseMap();
            CreateMap<ContaBancaria, DTOContaBancaria>().ReverseMap();
        }
    }
}
