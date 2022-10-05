using AutoMapper;
using EmployeeManagement.Contracts;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace EmployeeManagement.Endpoints
{
    public static class MapEmployeeEndPoints
    {
        public static void EmployeeEndPoints(this WebApplication app)
        {
            app.MapGet("/api/emploees", GetAllEmployees)
                .Produces<IEnumerable<Employee>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError);

            app.MapGet("/api/employees/{id:int}", GetEmployeeById)
                .WithName("GetEmployeeById")
                .Produces<Employee>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            app.MapDelete("/api/employees/{id:int}", DeleteEmployee)
                .Produces<Employee>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            app.MapPost("/api/employess", CreateEmployee)
                .Produces<Employee>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError);

            app.MapPut("/api/employees/{id:int}", UpdateEmployee)
                .Produces<Employee>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);
        }

        private async static Task<IResult> GetAllEmployees(IEmployeeRepository _repo)
        {
            try
            {
                return Results.Ok(await _repo.GetAllEmployees());
            }
            catch (Exception)
            {

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private async static Task<IResult> GetEmployeeById(IEmployeeRepository _repo, int id)
        {
            try
            {
                var employee = await _repo.GetEmployeeById(id);
                if (employee == null)
                {
                    return Results.NotFound($"Employee with Id = {id} is not found.");
                }

                return Results.Ok(employee);
            }
            catch (Exception)
            {

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private async static Task<IResult> DeleteEmployee(IEmployeeRepository _repo, int id)
        {
            try
            {
                var employee = await _repo.GetEmployeeById(id);
                if (employee == null)
                {
                    return Results.NotFound($"Employee with Id = {id} is not found.");
                }

                return Results.Ok(await _repo.DeleteAsync(id));
            }
            catch (Exception)
            {

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private async static Task<IResult> CreateEmployee([FromBody] Employee employee, IEmployeeRepository _repo)
        {
            try
            {
                if (employee == null) return Results.StatusCode(StatusCodes.Status400BadRequest);

                var createdEmployee = await _repo.CreateAsync(employee);

                return Results.CreatedAtRoute(nameof(GetEmployeeById), new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception)
            {

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private async static Task<IResult> UpdateEmployee([FromBody] Employee employee, IMapper _mapper, IEmployeeRepository _repo, int id)
        {
            try
            {
                if (id != employee.Id) return Results.StatusCode(StatusCodes.Status400BadRequest);

                var employeeToUpdate = await _repo.GetEmployeeById(id);
                if (employeeToUpdate == null)
                {
                    return Results.NotFound($"Employee with Id = {id} is not found.");
                }

                _mapper.Map(employee, employeeToUpdate);

                return Results.Ok(await _repo.UpdateByAsync(employeeToUpdate));
            }
            catch (Exception)
            {

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
