namespace CompanyVault.WebApi.Models.AutoMapperProfiles;

using AutoMapper;
using CompanyVault.WebApi.Models.DTOs.Import;

/// <summary>
/// Is responsible for mapping EmployeeRawImportDto to CompanyImportDto
/// </summary>
public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<EmployeeRawImportDto, CompanyImportDto>()
            .ForMember(dest => dest.Id, opt =>
            {
                opt.MapFrom(src => src.CompanyId);
            })
            .ForMember(dest => dest.Code, opt =>
            {
                opt.MapFrom(src => src.CompanyCode);
            })
            .ForMember(dest => dest.Description, opt =>
            {
                opt.MapFrom(src => src.CompanyDescription);
            });
    }
}
