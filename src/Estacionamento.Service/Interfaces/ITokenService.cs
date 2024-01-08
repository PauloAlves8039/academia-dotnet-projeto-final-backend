using Estacionamento.Service.Dtos.Account;

namespace Estacionamento.Service.Interfaces
{
    public interface ITokenService
    {
        UsuarioToken GerarToken(string email);
    }
}
