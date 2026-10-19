using SistemaEleicao.Console.Services;

namespace SistemaEleicao.Tests;

public sealed class CandidatoTests
{
    [Fact]
    public void CadastrarCandidato_ComDadosValidos_AdicionaCandidato()
    {
        var eleicao = new EleicaoManager();

        var candidato = eleicao.CadastrarCandidato("Ana Souza", 10);

        Assert.Equal("Ana Souza", candidato.Nome);
        Assert.Equal(10, candidato.Numero);
        Assert.Contains(candidato, eleicao.ListarCandidatos());
    }

    [Fact]
    public void CadastrarCandidato_ComNumeroDuplicado_LancaExcecao()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Ana Souza", 10);

        var exception = Assert.Throws<InvalidOperationException>(
            () => eleicao.CadastrarCandidato("Bruno Lima", 10));

        Assert.Equal("Já existe um candidato cadastrado com o número 10.", exception.Message);
        Assert.Single(eleicao.ListarCandidatos());
    }
}
