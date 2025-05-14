using System;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;
namespace CampusLove.Application.Services;
using CampusLove.Utilidades;

public class AuthService
{
    private readonly IUsuarioRepository _repo;
    public AuthService(IUsuarioRepository repo)
    {
        _repo = repo;
    }
    public bool Login(string usuario_email, string clave)
    {
        var usuario = _repo.ObtenerPorEmail(usuario_email)
        ?? _repo.ObtenerPorUsuario(usuario_email);
        
        if(usuario is null)
        {
            Console.WriteLine("❌ Usuario no encontrado.");
            return false;
        }
        if (usuario.Clave != clave)
        {
             Console.WriteLine("❌ Contraseña incorrecta.");
            return false;
        }
       
        
        return true;

    }

    public bool Registrar(Usuario nuevoUsuario)
    {
      if (nuevoUsuario.Edad < 16)
        {
            Console.WriteLine("🚫 Debes tener al menos 16 años para registrarte.");
            return false;
        }

        try
        {
            _repo.Crear(nuevoUsuario);
            Console.WriteLine("✅ Registro exitoso.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error al registrar: " + ex.Message);
            return false;
        }
    }
}
