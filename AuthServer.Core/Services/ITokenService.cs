using AuthServer.Core.Configuration;
using AuthServer.Core.Dto;
using AuthServer.Core.Model;

namespace AuthServer.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp userApp);
        ClientTokenDto CreateTokenyClient(Client client);
    }
}
