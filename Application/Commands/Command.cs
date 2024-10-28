using FluentValidation.Results;
using MediatR;

namespace ImpressioApi_.Application.Commands;

public abstract class Command<TResult> : IRequest<TResult> where TResult : class
{
    protected ValidationResult _validationResult { get; set; } = new();

    public virtual Task<bool> Valida()
    {
        throw new NotImplementedException();
    }
    
    public virtual IEnumerable<string> ObterErros()
    {
        return _validationResult.Errors.Select(el => el.ErrorMessage);
    }
}