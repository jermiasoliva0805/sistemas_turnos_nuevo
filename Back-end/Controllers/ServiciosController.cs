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
public class ServiciosController : ControllerBase
{
    private readonly IServicioRepository _servicioRepository;
    private readonly IMapper _mapper;

    public ServiciosController(IServicioRepository servicioRepository, IMapper mapper)
    {
        _servicioRepository = servicioRepository;
        _mapper = mapper;
    }

    // ✅ Obtener todos los servicios
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var servicios = await _servicioRepository.GetAllAsync();
        var serviciosDto = _mapper.Map<List<ServicioDTO>>(servicios);
        return Ok(serviciosDto);
    }

    // ✅ Obtener servicio por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var servicio = await _servicioRepository.GetByIdAsync(id);
        if (servicio == null)
            return NotFound("Servicio no encontrado");

        var servicioDto = _mapper.Map<ServicioDTO>(servicio);
        return Ok(servicioDto);
    }

    // ✅ Crear servicio
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ServicioDTO dto)
    {
        var servicio = _mapper.Map<Servicio>(dto);
        await _servicioRepository.AddAsync(servicio);
        await _servicioRepository.SaveAsync();
        return Ok(_mapper.Map<ServicioDTO>(servicio));
    }

    // ✅ Actualizar servicio
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ServicioDTO dto)
    {
        var servicio = await _servicioRepository.GetByIdAsync(id);
        if (servicio == null)
            return NotFound("Servicio no encontrado");

        _mapper.Map(dto, servicio);
        _servicioRepository.Update(servicio);
        await _servicioRepository.SaveAsync();
        return Ok(_mapper.Map<ServicioDTO>(servicio));
    }

    // ✅ Eliminar servicio
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var servicio = await _servicioRepository.GetByIdAsync(id);
        if (servicio == null)
            return NotFound("Servicio no encontrado");

        _servicioRepository.Delete(servicio);
        await _servicioRepository.SaveAsync();
        return Ok("Servicio eliminado exitosamente");
    }
}
