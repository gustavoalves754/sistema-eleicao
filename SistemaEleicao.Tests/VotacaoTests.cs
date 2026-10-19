using SistemaEleicao.Console.Domain;
using SistemaEleicao.Console.Services;

namespace SistemaEleicao.Tests;

public sealed class VotacaoTests
{
    [Fact]
    public void RegistrarVoto_QuandoEleitorJaVotou_LancaExcecao()
    {
        var eleicao = CriarEleicaoComCandidatoEEleitor();
        eleicao.RegistrarVoto("12345678900", 10);

        var exception = Assert.Throws<InvalidOperationException>(
            () => eleicao.RegistrarVoto("12345678900", 10));

        Assert.Equal("Este eleitor já votou.", exception.Message);
        Assert.Equal(1, eleicao.BuscarCandidato(10)!.TotalVotos);
    }

    [Fact]
    public void RegistrarVoto_ParaCandidatoInexistente_ContabilizaVotoNulo()
    {
        var eleicao = CriarEleicaoComCandidatoEEleitor();

        var tipoVoto = eleicao.RegistrarVoto("12345678900", 99);

        Assert.Equal(TipoVoto.Nulo, tipoVoto);
        Assert.Equal(1, eleicao.TotalVotosNulos);
        Assert.True(eleicao.BuscarEleitor("12345678900")!.JaVotou);
    }

    [Fact]
    public void RegistrarVoto_SemNumero_ContabilizaVotoBranco()
    {
        var eleicao = CriarEleicaoComCandidatoEEleitor();

        var tipoVoto = eleicao.RegistrarVoto("12345678900", null);

        Assert.Equal(TipoVoto.Branco, tipoVoto);
        Assert.Equal(1, eleicao.TotalVotosBrancos);
    }

    private static EleicaoManager CriarEleicaoComCandidatoEEleitor()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Ana Souza", 10);
        eleicao.RegistrarEleitor("12345678900");
        return eleicao;
    }
}
