using ImpressioApi_.Application.Commands.Login.Validations;

namespace ImpressioApi_.Application.Commands.Usuario.Write
{
    public class LoginUsuarioCommand : Command<CommandResult>
    {
        public required string EmailUsuario { get; set; }
        public required string Senha { get; set; }
        
        public override async Task<bool> Valida()
        {
            var validation = new LoginUsuarioCommandValidation();
            _validationResult = await validation.ValidateAsync(this);

            return _validationResult.IsValid;
        }
    }
}