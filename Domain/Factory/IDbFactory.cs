using System;
using System.Collections.Generic;
using System.Linq;
using CampusLove.Application.Services;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;
namespace CampusLove.Domain.Factory
{
    public interface IDbFactory
    {
        IUsuarioRepository CrearUsuarioRepository();
        IPaisRepository CrearPaisRepository();
        
    }
}