using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;
using PromoCodeFactory.WebHost.Services.Mapping;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IRepository<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(_mapper.Map<Employee, EmployeeShortResponse>).ToList();

            return employeesModelList;
        }

        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return NotFound();

            var employeeModel = _mapper.Map<Employee, EmployeeResponse>(employee);

            return employeeModel;
        }

        /// <summary>
        /// Создать сотрудника
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateEmployeeAsync(EmployeeCreateModel model)
        {
            bool result = await _employeeRepository.CreateAsync(_mapper.Map<EmployeeCreateModel, Employee>(model));
            if (result) return Created();

            return BadRequest();
        }

        /// <summary>
        /// Обновить сотрудника
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult> UpdateEmployeeAsync(Guid id, EmployeeUpdateModel model)
        {
            if (_employeeRepository.GetByIdAsync(id) is null) return NotFound();

            var entity = _mapper.Map<EmployeeUpdateModel, Employee>(model);
            entity.Id = id;

            bool result = await _employeeRepository.UpdateByIdAsync(entity);
            if (result) return Ok();

            return BadRequest();
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult> DeleteEmployeeAsync(Guid id)
        {
            if (_employeeRepository.GetByIdAsync(id) is null)
                return NotFound();

            bool result = await _employeeRepository.DeleteAsync(id);
            if (result) return NoContent();

            return BadRequest();
        }
    }
}