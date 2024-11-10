using System.Transactions;
using AutoMapper;
using ImpressioApi_.Domain.DTO.Queries;
using ImpressioApi_.Domain.DTO.Write;
using ImpressioApi_.Domain.Interfaces.Queries;
using ImpressioApi_.Domain.Interfaces.Repositories;
using ImpressioApi_.Domain.Model;
using MediatR;

namespace ImpressioApi_.Application.Commands.ObraArte.Write;

public class EditarObraArteHandler: IRequestHandler<EditarObraArteCommand, CommandResult<EditarObraArteRespostaDTO>>
{
    private readonly IMapper _mapper;
    private readonly IObraArteRepository _obraDeArteRepository;
    private EditarObraArteCommand _request = null!;
    private CancellationToken _cancellationToken;
    private CommandResult<EditarObraArteRespostaDTO> _result = null!;

    public EditarObraArteHandler(IMapper mapper, IObraArteRepository obraArteRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _obraDeArteRepository = obraArteRepository ?? throw new ArgumentNullException(nameof(obraArteRepository));
    }

    public async Task<CommandResult<EditarObraArteRespostaDTO>> Handle(EditarObraArteCommand request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 2, 0), TransactionScopeAsyncFlowOption.Enabled);
        
        try
        {
            _request = request;
            var valida = await _request.Valida();
            _cancellationToken = cancellationToken;
            _result = new CommandResult<EditarObraArteRespostaDTO>();

            if (!valida)
            {
                return _result.AdicionarErros(_request.ObterErros());
            }

            var obraArte = await _obraDeArteRepository.GetById(_request.IdObraArte);

            var obraArteModel = _mapper.Map<ObraArteModel>(obraArte);
            
            obraArteModel.DescricaoObraArte = request.DescricaoObraArte ?? obraArteModel.DescricaoObraArte;
            if (_request.Publico)
            {
                obraArteModel.Publico = _request.Publico;
            }
            

            _obraDeArteRepository.Update(obraArteModel);
            var sucesso = await _obraDeArteRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                return _result.AdicionarErro("Falha ao atualizar obra de arte.");
            }
            return _result.Sucesso("Obra de arte atualizada com sucesso!");
        }
        finally
        {
            if (_result.Success)
            {
                scope.Complete();
            }
        }
    }
}