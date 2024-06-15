using System;
using System.Linq;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;

namespace PromoCodeFactory.WebHost.Services.Mapping
{
    public class StubMapper : IMapper
    {
        private const string _mappingNotSpecified = "Mapping not specified";
        public TResult Map<TSource, TResult>(TSource source) where TResult : class
        {
            switch (source)
            {
                case EmployeeCreateModel employeeCreateModel when typeof(TResult) == typeof(Employee):
                    return new Employee
                    {
                        AppliedPromocodesCount = employeeCreateModel.AppliedPromocodesCount,
                        Email = employeeCreateModel.Email,
                        FirstName = employeeCreateModel.FullName,
                        LastName = employeeCreateModel.FullName,
                        Roles = employeeCreateModel.Roles.Select(x => new Role()
                        {
                            Name = x.Name,
                            Description = x.Description
                        }).ToList(),
                    } as TResult;
                case EmployeeUpdateModel employeeUpdateModel when typeof(TResult) == typeof(Employee):
                    return new Employee
                    {
                        AppliedPromocodesCount = employeeUpdateModel.AppliedPromocodesCount,
                        Email = employeeUpdateModel.Email,
                        FirstName = employeeUpdateModel.FullName,
                        LastName = employeeUpdateModel.FullName,
                        Roles = employeeUpdateModel.Roles.Select(x => new Role()
                        {
                            Name = x.Name,
                            Description = x.Description
                        }).ToList(),
                    } as TResult;
                case Employee employee when typeof(TResult) == typeof(EmployeeResponse):
                    return new EmployeeResponse
                    {
                        Id = employee.Id,
                        Email = employee.Email,
                        Roles = employee.Roles.Select(x => new RoleItemResponse()
                        {
                            Name = x.Name,
                            Description = x.Description
                        }).ToList(),
                        FullName = employee.FullName,
                        AppliedPromocodesCount = employee.AppliedPromocodesCount
                    } as TResult;
                case Employee employee when typeof(TResult) == typeof(EmployeeShortResponse):
                    return new EmployeeShortResponse()
                    {
                        Id = employee.Id,
                        Email = employee.Email,
                        FullName = employee.FullName,
                    } as TResult;
            }

            throw new InvalidOperationException(_mappingNotSpecified);
        }
    }  // todo stub
}