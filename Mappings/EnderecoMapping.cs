using AutoMapper;
using IntegraBrasilApi.Dtos;
using IntegraBrasilApi.Model;

namespace IntegraBrasilApi.Mappings{

public class EnderecoMapping : Profile
{
    public EnderecoMapping(){
        CreateMap(typeof(ResponseGenerico<>), typeof(ResponseGenerico<>));
        CreateMap<EnderecoResponse, EnderecoModel>();
        CreateMap<EnderecoModel, EnderecoResponse>();
        
    }
}}