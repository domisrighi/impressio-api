using FluentValidation.Results;
using ImpressioApi_.Domain.Queries;
using MediatR;

namespace ImpressioApi_.Application.Commands;

public class PaginacaoCommand<TResult> : PaginacaoRequisicao, IRequest<TResult> where TResult : class
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