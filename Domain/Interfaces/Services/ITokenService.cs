using ImpressioApi_.Domain.Model;

namespace ImpressioApi_.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken();
}