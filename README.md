# DotNetCafe 

[![.Net Core 3.1](https://github.com/DotNetCafe/DotNetCafe/workflows/.Net%20Core%203.1/badge.svg)](https://github.com/DotNetCafe/DotNetCafe/actions?query=workflow%3A%22.Net+Core+3.1%22)
[![SemVer](https://img.shields.io/github/v/tag/DotNetCafe/DotNetCafe?label=SemVer&sort=semver)](https://github.com/DotNetCafe/DotNetCafe/tree/v0.1.0/)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/DotNetCafe?label=NuGet&logo=nuget&logoColor=blue)](https://www.nuget.org/packages/DotNetCafe/)
[![Nuget](https://img.shields.io/nuget/dt/DotNetCafe?label=Downloads&logo=nuget&logoColor=blue)](https://www.nuget.org/packages/DotNetCafe/0.2.0)
[![License](https://img.shields.io/github/license/DotNetCafe/DotNetCafe?label=License)](LICENSE)

## Introdução

O **DotNetCafe** é uma biblioteca com tipos complexos bem definidos. Pois no dia-a-dia lidamos com inúmeros valores candidatos a tipos complexos que acabam sendo rebaixados ou menosprezados apenas a strings, causando uma bagunça tremenda no código devido a quantidade de métodos auxiliares que são elaborados para o tratamento destes.

Para evitar que seu projeto vire uma bagunça, surge o **DotNetCafe**, e como um bom café, a forma de preparo e a qualidade dos grãos devem ser considerados. A forma de preparo dos tipos definidos no **DotNetCafe** levam em consideração o próprio tipo em si e sua representação no mundo real.

A qualidade do código é concisa e limpa, suficientemente eficaz para efetuar a análise do dado, validação e formatação com uma boa performance. O código não é dificil de ler para não comprometer a manutenção da biblioteca.

## Compatibilidade

### .Net Core e .NET Standard
DotNetCafe | .Net Core | .Net Standard
---------- | --------- | -------------
0.2.0      | 3.1       | 2.1          

### Tipos Suportados

A tabela abaixo possui os tipos suportados e a versão no qual foi implantado:

Funcionalidade    | CNPJ    | CPF
----------------- | ------- | -------
ctor(string)      | [0.1.0] | [0.2.0]
Parse             | [0.1.0] | [0.2.0]
TryParse          | [0.1.0] | [0.2.0]
IsEmpty           | [0.1.0] | [0.2.0]
IComparable       | [0.1.0] | [0.2.0]
IComparable\<T>   | [0.1.0] | [0.2.0]
IEquatable\<T>    | [0.1.0] | [0.2.0]
IFormattable      | [0.1.0] | [0.2.0]
==, !=            | [0.1.0] | [0.2.0]
\>, <, >=, <=     | [0.1.0] | [0.2.0]

*\***N.P.** não publicado.*

## Utilizando a biblioteca

### 1. Configure o CSPROJ

```xml
<PackageReference Include="DotNetCafe" Version="0.2.0" />
```

### 2. Restaure os pacotes

```shell
$ dotnet restore
```

### 3. Divirta-se!

```csharp
using DotNetCafe;
using System;
					
public class Program
{
	public static void Main()
	{
		Cnpj cnpj = Cnpj.Empty;
		Cpf cpf = Cpf.Empty;

		try
		{
			cnpj = new Cnpj("00.000.000/0001-91");
			cpf = new Cpf("100.100.100-17");
		}
		catch (ArgumentException e)
		{
			Console.WriteLine($"Error: {e.Message}");
			Environment.Exit(1);
		}
		catch (FormatException e)
		{
			Console.WriteLine($"Error: {e.Message}");
			Environment.Exit(1);
		}

		Console.WriteLine($"CNPJ: {cnpj} (General format)");
		Console.WriteLine($"CNPJ: {cnpj:N} (Numeric format)");
		Console.WriteLine($"CPF: {cpf} (General format)");
		Console.WriteLine($"CPF: {cpf:B} (New format)");
		Console.WriteLine($"CPF: {cpf:N} (Numeric format)");
	}
}
```

[N.P.]: https://github.com/DotNetCafe/DotNetCafe/tree/master/
[0.1.0]: https://github.com/DotNetCafe/DotNetCafe/tree/v0.1.0/
[0.2.0]: https://github.com/DotNetCafe/DotNetCafe/tree/v0.2.0/
