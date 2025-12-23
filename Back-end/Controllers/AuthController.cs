using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sistemas_turnos.DTOs;
using sistemas_turnos.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthController(IUsuarioRepository usuarioRepository, IConfiguration configuration, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
        _mapper = mapper;
    }

    // ✅ Registro
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UsuarioRegisterDTO dto)
    {
        // Validar que el email no exista
        var usuarioExistente = await _usuarioRepository.GetByEmailAsync(dto.Email);
        if (usuarioExistente != null)
            return BadRequest("El email ya está registrado");

        // Crear nuevo usuario
        var usuario = new Usuario
        {
            Nombre_Completo = dto.NombreCompleto,
            Email = dto.Email,
            Password_Hash = HashPassword(dto.Password),
            Rol = dto.Rol ?? "cliente",
            Fecha_Creacion = DateTime.Now
        };

        await _usuarioRepository.AddAsync(usuario);
        await _usuarioRepository.SaveAsync();

        return Ok(new { message = "Usuario registrado exitosamente" });
    }

    // ✅ Login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO dto)
    {
        // Buscar usuario
        var usuario = await _usuarioRepository.GetByEmailAsync(dto.Email);
        if (usuario == null || !VerifyPassword(dto.Password, usuario.Password_Hash))
            return Unauthorized("Email o contraseña inválidos");

        // Generar JWT
        var token = GenerateJwtToken(usuario);

        // Mapear usuario a DTO
        var usuarioDto = new
        {
            Id_Usuarios = usuario.Id_Usuarios,
            Nombre_Completo = usuario.Nombre_Completo,
            Email = usuario.Email,
            Rol = usuario.Rol,
            Fecha_Creacion = usuario.Fecha_Creacion
        };

        return Ok(new { token, user = usuarioDto });
    }

    // 🔐 Hash password
    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    // 🔐 Verify password
    private bool VerifyPassword(string password, string hash)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput.Equals(hash);
    }

    // 🔐 Generate JWT Token
    private string GenerateJwtToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtKey"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[]
            {
                new System.Security.Claims.Claim("id", usuario.Id_Usuarios.ToString()),
                new System.Security.Claims.Claim("email", usuario.Email),
                new System.Security.Claims.Claim("rol", usuario.Rol)
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
