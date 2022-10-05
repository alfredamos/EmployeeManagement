using AutoMapper;
using EmployeeManagement.Contracts;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Endpoints
{
    public static class MapDepartmentEndPoints
    {
        public static void DepartmentEndPoints(this WebApplication app)
        {

            app.MapGet("/api/departments", GetAllDepartments)
                .Produces<IEnumerable<Department>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError);

            app.MapGet("/api/departments/{id:int}", GetDepartmentById)
                .WithName("GetDepartmentById")
                .Produces<Department>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            app.MapDelete("/api/departments/{id:int}", DeleteDepartment)
                .Produces<Department>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

            app.MapPost("/api/departments", CreateDepartment)
                .Produces<Department>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError);

            app.MapPut("/api/departments/{id:int}", UpdateDepartment)
                .Produces<Department>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);
        }

        private async static Task<IResult> GetAllDepartments(IDepartmentRepository _repo)
        {
            try
            {
                return Results.Ok(await _repo.GetAllDepartments());
            }
            catch (Exception)
            {

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private async static Task<IResult> GetDepartmentById(IDepartmentRepository _repo, int id)
        {
            try
            {
                var department = await _repo.GetDepartmentById(id);
                if (department == null)
                {
                    return Results.NotFound($"Department with Id = {id} is not found.");
                }

                return Results.Ok(department);
            }
            catch (Exception)
            {

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private async static Task<IResult> DeleteDepartment(IDepartmentRepository _repo, int id)
        {
            try
            {
                var department = await _repo.GetDepartmentById(id);
                if (department == null)
                {
                    return Results.NotFound($"Department with Id = {id} is not found.");
                }

                return Results.Ok(await _repo.DeleteAsync(id));
            }
            catch (Exception)
            {

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private async static Task<IResult> CreateDepartment([FromBody] Department department, IDepartmentRepository _repo)
        {
            try
            {
                if (department == null) return Results.StatusCode(StatusCodes.Status400BadRequest);

                var createdDepartment = await _repo.CreateAsync(department);

                return Results.CreatedAtRoute(nameof(GetDepartmentById), new { id = createdDepartment.Id }, createdDepartment);
            }
            catch (Exception)
            {

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private async static Task<IResult> UpdateDepartment([FromBody] Department department, IMapper _mapper, IDepartmentRepository _repo, int id)
        {
            try
            {
                if (id != department.Id) return Results.StatusCode(StatusCodes.Status400BadRequest);

                var departmentToUpdate = await _repo.GetDepartmentById(id);
                if (departmentToUpdate == null)
                {
                    return Results.NotFound($"Department with Id = {id} is not found.");
                }

                _mapper.Map(department, departmentToUpdate);

                return Results.Ok(await _repo.UpdateByAsync(departmentToUpdate));
            }
            catch (Exception)
            {

                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
