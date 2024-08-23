namespace CompanyVault.WebApi.Models.AutoMapperProfiles;

using AutoMapper;
using CompanyVault.WebApi.Models.DTOs.Import;

/// <summary>
/// Is responsible for mapping EmployeeRawImportDto to DepartmentImportDto.
/// </summary>
public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<EmployeeRawImportDto, DepartmentImportDto>()
            .ForMember(dest => dest.Name, opt =>
            {
                opt.MapFrom(src => src.EmployeeDepartment);
            })
            .ForMember(dest => dest.CompanyCode, opt =>
            {
                opt.MapFrom(src => src.CompanyCode);
            });
    }
}
