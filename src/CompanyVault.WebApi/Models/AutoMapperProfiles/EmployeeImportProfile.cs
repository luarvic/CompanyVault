namespace CompanyVault.WebApi.Models.AutoMapperProfiles;

using AutoMapper;
using CompanyVault.WebApi.Models.DTOs.Import;

/// <summary>
/// Is responsible for mapping EmployeeRawImportDto to EmployeeImportDto.
/// </summary>
public class EmployeeImportProfile : Profile
{
    public EmployeeImportProfile()
    {
        CreateMap<EmployeeRawImportDto, EmployeeImportDto>()
            .ForMember(dest => dest.Number, opt =>
            {
                opt.MapFrom(src => src.EmployeeNumber);
            })
            .ForMember(dest => dest.FirstName, opt =>
            {
                opt.MapFrom(src => src.EmployeeFirstName);
            })
            .ForMember(dest => dest.LastName, opt =>
            {
                opt.MapFrom(src => src.EmployeeLastName);
            })
            .ForMember(dest => dest.Email, opt =>
            {
                opt.MapFrom(src => src.EmployeeEmail);
            })
            .ForMember(dest => dest.CompanyCode, opt =>
            {
                opt.MapFrom(src => src.CompanyCode);
            })
            .ForMember(dest => dest.DepartmentCode, opt =>
            {
                opt.MapFrom(src => src.EmployeeDepartment);
            })
            .ForMember(dest => dest.ManagerCode, opt =>
            {
                opt.MapFrom(src => src.ManagerEmployeeNumber);
            });
    }
}
