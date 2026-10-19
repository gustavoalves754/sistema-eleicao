using SistemaEleicao.Console.Domain;
using SistemaEleicao.Console.Services;

var eleicao = new EleicaoManager();
var executando = true;

while (executando)
{
    ExibirMenu();
    var opcao = System.Console.ReadLine()?.Trim();
    System.Console.WriteLine();

    try
    {
        switch (opcao)
        {
            case "1":
                CadastrarCandidato(eleicao);
                break;
            case "2":
                RegistrarEleitor(eleicao);
                break;
            case "3":
                RegistrarVoto(eleicao);
                break;
            case "4":
                ExibirApuracao(eleicao);
                break;
            case "0":
                executando = false;
                System.Console.WriteLine("Sistema encerrado.");
                break;
            default:
                System.Console.WriteLine("Opção inválida.");
                break;
        }
    }
    catch (Exception exception) when (exception is ArgumentException
        or InvalidOperationException
        or KeyNotFoundException)
    {
        System.Console.WriteLine($"Não foi possível concluir a operação: {exception.Message}");
    }

    System.Console.WriteLine();
}

static void ExibirMenu()
{
    System.Console.WriteLine("================================");
    System.Console.WriteLine("       SISTEMA DE ELEIÇÃO");
    System.Console.WriteLine("================================");
    System.Console.WriteLine("1 - Cadastrar candidato");
    System.Console.WriteLine("2 - Registrar eleitor");
    System.Console.WriteLine("3 - Votar");
    System.Console.WriteLine("4 - Exibir apuração");
    System.Console.WriteLine("0 - Sair");
    System.Console.Write("Escolha uma opção: ");
}

static void CadastrarCandidato(EleicaoManager eleicao)
{
    System.Console.Write("Nome do candidato: ");
    var nome = System.Console.ReadLine();
    System.Console.Write("Número do candidato: ");

    if (!int.TryParse(System.Console.ReadLine(), out var numero))
    {
        System.Console.WriteLine("Número inválido.");
        return;
    }

    var candidato = eleicao.CadastrarCandidato(nome, numero);
    System.Console.WriteLine($"Candidato {candidato.Nome} cadastrado com o número {candidato.Numero}.");
}

static void RegistrarEleitor(EleicaoManager eleicao)
{
    System.Console.Write("Título ou CPF do eleitor: ");
    var eleitor = eleicao.RegistrarEleitor(System.Console.ReadLine());
    System.Console.WriteLine($"Eleitor {eleitor.Identificador} registrado.");
}

static void RegistrarVoto(EleicaoManager eleicao)
{
    System.Console.Write("Título ou CPF do eleitor: ");
    var identificador = System.Console.ReadLine() ?? string.Empty;
    System.Console.Write("Número do candidato (Enter para branco): ");
    var entrada = System.Console.ReadLine();

    int? numeroCandidato;

    if (string.IsNullOrWhiteSpace(entrada))
    {
        numeroCandidato = null;
    }
    else if (int.TryParse(entrada, out var numero))
    {
        numeroCandidato = numero;
    }
    else
    {
        System.Console.WriteLine("Número inválido.");
        return;
    }

    var tipoVoto = eleicao.RegistrarVoto(identificador, numeroCandidato);
    System.Console.WriteLine(tipoVoto switch
    {
        TipoVoto.Nominal => "Voto registrado para o candidato.",
        TipoVoto.Branco => "Voto branco registrado.",
        _ => "Candidato inexistente. Voto contabilizado como nulo."
    });
}

static void ExibirApuracao(EleicaoManager eleicao)
{
    var resultado = eleicao.Apurar();

    System.Console.WriteLine("--- Apuração ---");

    foreach (var candidato in resultado.Candidatos)
    {
        System.Console.WriteLine(
            $"{candidato.Numero} - {candidato.Nome}: {candidato.TotalVotos} voto(s)");
    }

    System.Console.WriteLine($"Votos brancos: {resultado.VotosBrancos}");
    System.Console.WriteLine($"Votos nulos: {resultado.VotosNulos}");
    System.Console.WriteLine($"Total de votos: {resultado.TotalVotos}");

    if (resultado.Vencedores.Count == 0)
    {
        System.Console.WriteLine("Ainda não há candidato vencedor.");
        return;
    }

    var nomesVencedores = resultado.Vencedores.Select(candidato => candidato.Nome).ToArray();

    System.Console.WriteLine(nomesVencedores.Length == 1
        ? $"Candidato vencedor: {nomesVencedores[0]}"
        : $"Empate entre: {string.Join(", ", nomesVencedores)}");
}
