# Sistema de Eleição

Aplicação de console em .NET 10 que simula uma eleição. O sistema cadastra candidatos e eleitores, impede votos duplicados, registra votos nominais, brancos e nulos e apresenta a apuração com o candidato vencedor ou um eventual empate.

## Funcionalidades

- Cadastro de candidatos com validação de número único.
- Registro de eleitores por título ou CPF simulado.
- Bloqueio de segundo voto para o mesmo eleitor.
- Contabilização de voto em candidato inexistente como nulo.
- Registro de voto branco.
- Apuração com totais por candidato, brancos, nulos e vencedor.
- Testes unitários com xUnit.

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0)

Verifique a instalação:

```bash
dotnet --version
```

## Compilar

Na raiz do repositório, execute:

```bash
dotnet build SistemaEleicao.sln
```

## Executar

```bash
dotnet run --project SistemaEleicao.Console/SistemaEleicao.Console.csproj
```

Use o menu para cadastrar pelo menos um candidato e um eleitor antes de registrar o voto. No momento da votação, pressione apenas `Enter` no campo do número do candidato para votar em branco.

## Executar os testes

```bash
dotnet test SistemaEleicao.sln
```

## Estrutura do projeto

```text
SistemaEleicao.Console/
├── Domain/       Entidades e resultado da eleição
├── Services/     Regras de cadastro, votação e apuração
└── Program.cs    Menu interativo

SistemaEleicao.Tests/
└── Testes unitários xUnit
```

Os dados são mantidos em memória e reiniciados sempre que a aplicação é encerrada.
