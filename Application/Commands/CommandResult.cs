namespace ImpressioApi_.Application.Commands;

public class CommandResult
{
    public bool Success { get; set; } = true;
    public List<string> Messages { get; set; } = new();

    public virtual CommandResult Sucesso()
    {
      Success = true;

      return this;
    }

    public virtual CommandResult Sucesso(string mensagem)
    {
      Sucesso();
      Messages.Add(mensagem);

      return this;
    }

    public virtual CommandResult Sucesso(IEnumerable<string> mensagens)
    {
      Sucesso();
      Messages.AddRange(mensagens);

      return this;
    }

    private protected virtual CommandResult Erro()
    {
      Success = false;

      return this;
    }

    public virtual CommandResult AdicionarErro(string erro)
    {
      Erro();
      Messages.Add(erro);

      return this;
    }

    public virtual CommandResult AdicionarErros(IEnumerable<string> erros)
    {
      Erro();
      Messages.AddRange(erros);

      return this;
    }
}
public class CommandResult<T> : CommandResult where T : class
{
    public T? Data { get; set; }

    public override CommandResult<T> Sucesso()
    {
      base.Sucesso();

      return this;
    }

    public CommandResult<T> Sucesso(T? data)
    {
      Sucesso();
      Data = data;

      return this;
    }

    public override CommandResult<T> Sucesso(string mensagem)
    {
      base.Sucesso(mensagem);

      return this;
    }

    public override CommandResult<T> Sucesso(IEnumerable<string> mensagens)
    {
      base.Sucesso(mensagens);

      return this;
    }

    public CommandResult<T> Sucesso(T? data, string mensagem)
    {
      Sucesso(data);
      Sucesso(mensagem);

      return this;
    }

    public CommandResult<T> Sucesso(T? data, IEnumerable<string> mensagens)
    {
      Sucesso(data);
      Sucesso(mensagens);

      return this;
    }

    private protected override CommandResult<T> Erro()
    {
      base.Erro();
      Data = null;

      return this;
    }

    public override CommandResult<T> AdicionarErro(string erro)
    {
      Erro();
      base.AdicionarErro(erro);

      return this;
    }

    public override CommandResult<T> AdicionarErros(IEnumerable<string> erros)
    {
      Erro();
      base.AdicionarErros(erros);

      return this;
    }
}