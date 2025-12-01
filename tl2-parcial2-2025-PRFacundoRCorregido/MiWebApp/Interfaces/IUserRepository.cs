using MiWebApp.Models;

namespace MiWebApp.Interfaces;

public interface IUserRepository
{
    Usuario GetUser(string username, string password);
} 