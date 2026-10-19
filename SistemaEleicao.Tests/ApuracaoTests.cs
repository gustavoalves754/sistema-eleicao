using SistemaEleicao.Console.Services;

namespace SistemaEleicao.Tests;

public sealed class ApuracaoTests
{
    [Fact]
    public void Apurar_ComVotosRegistrados_RetornaTotaisEVencedor()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Ana Souza", 10);
        eleicao.CadastrarCandidato("Bruno Lima", 20);
        eleicao.RegistrarEleitor("111");
        eleicao.RegistrarEleitor("222");
        eleicao.RegistrarEleitor("333");
        eleicao.RegistrarVoto("111", 10);
        eleicao.RegistrarVoto("222", 10);
        eleicao.RegistrarVoto("333", 99);

        var resultado = eleicao.Apurar();

        Assert.Equal(3, resultado.TotalVotos);
        Assert.Equal(1, resultado.VotosNulos);
        Assert.Equal("Ana Souza", Assert.Single(resultado.Vencedores).Nome);
        Assert.Equal(2, resultado.Candidatos[0].TotalVotos);
    }
}
