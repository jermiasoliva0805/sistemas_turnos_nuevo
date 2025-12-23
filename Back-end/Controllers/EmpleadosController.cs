using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sistemas_turnos.DTOs;
using sistemas_turnos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class EmpleadosController : ControllerBase
{
    private readonly IEmpleadoRepository _empleadoRepository;
    private readonly IMapper _mapper;

    public EmpleadosController(IEmpleadoRepository empleadoRepository, IMapper mapper)
    {
        _empleadoRepository = empleadoRepository;
        _mapper = mapper;
    }

    // ✅ Obtener todos los empleados
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var empleados = await _empleadoRepository.GetAllAsync();
        var empleadosDto = _mapper.Map<List<EmpleadoDTO>>(empleados);
        return Ok(empleadosDto);
    }

    // ✅ Obtener empleado por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var empleado = await _empleadoRepository.GetByIdAsync(id);
        if (empleado == null)
            return NotFound("Empleado no encontrado");

        var empleadoDto = _mapper.Map<EmpleadoDTO>(empleado);
        return Ok(empleadoDto);
    }

    // ✅ Crear empleado
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmpleadoDTO dto)
    {
        var empleado = _mapper.Map<Empleado>(dto);
        await _empleadoRepository.AddAsync(empleado);
        await _empleadoRepository.SaveAsync();
        return Ok(_mapper.Map<EmpleadoDTO>(empleado));
    }

    // ✅ Actualizar empleado
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EmpleadoDTO dto)
    {
        var empleado = await _empleadoRepository.GetByIdAsync(id);
        if (empleado == null)
            return NotFound("Empleado no encontrado");

        _mapper.Map(dto, empleado);
        _empleadoRepository.Update(empleado);
        await _empleadoRepository.SaveAsync();
        return Ok(_mapper.Map<EmpleadoDTO>(empleado));
    }

    // ✅ Eliminar empleado
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var empleado = await _empleadoRepository.GetByIdAsync(id);
        if (empleado == null)
            return NotFound("Empleado no encontrado");

        _empleadoRepository.Delete(empleado);
        await _empleadoRepository.SaveAsync();
        return Ok("Empleado eliminado exitosamente");
    }
}
