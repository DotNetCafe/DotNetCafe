# DotNetCafe 

[![.Net Core 3.1](https://github.com/DotNetCafe/DotNetCafe/workflows/.Net%20Core%203.1/badge.svg)](https://github.com/DotNetCafe/DotNetCafe/actions?query=workflow%3A%22.Net+Core+3.1%22)
[![SemVer](https://img.shields.io/github/v/tag/DotNetCafe/DotNetCafe?label=SemVer&sort=semver)](https://github.com/DotNetCafe/DotNetCafe/tree/v0.3.0/)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/DotNetCafe?label=NuGet&logo=nuget&logoColor=blue)](https://www.nuget.org/packages/DotNetCafe/)
[![Nuget](https://img.shields.io/nuget/dt/DotNetCafe?label=Downloads&logo=nuget&logoColor=blue)](https://www.nuget.org/packages/DotNetCafe/v0.3.0)
[![License](https://img.shields.io/github/license/DotNetCafe/DotNetCafe?label=License)](LICENSE)

## Introdução

O **DotNetCafe** é uma biblioteca com tipos complexos bem definidos.

## Compatibilidade

### .Net Core e .NET Standard
.Net Core | .Net Standard
--------- | -------------
3.1       | 2.1

### Tipos Suportados

A tabela abaixo possui os tipos suportados e a versão no qual foi implantado:

Funcionalidade    | CNPJ    | CPF     | CEP
----------------- | ------- | ------- | -------
ctor(string)      | [0.1.0] | [0.2.0] | [0.3.0]
Parse             | [0.1.0] | [0.2.0] | [0.3.0]
TryParse          | [0.1.0] | [0.2.0] | [0.3.0]
IsEmpty           | [0.1.0] | [0.2.0] | [0.3.0]
IComparable       | [0.1.0] | [0.2.0] | [0.3.0]
IComparable\<T>   | [0.1.0] | [0.2.0] | [0.3.0]
IEquatable\<T>    | [0.1.0] | [0.2.0] | [0.3.0]
IFormattable      | [0.1.0] | [0.2.0] | [0.3.0]
==, !=            | [0.1.0] | [0.2.0] | [0.3.0]
\>, <, >=, <=     | [0.1.0] | [0.2.0] | [0.3.0]

## Utilizando a biblioteca

### 1. Configure o CSPROJ

```xml
<PackageReference Include="DotNetCafe" Version="0.3.0" />
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
		Cep cep = Cep.Empty;

		try
		{
			cnpj = new Cnpj("00.000.000/0001-91");
			cpf = new Cpf("100.100.100-17");
			cep = new Cep("01310-940");
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
		Console.WriteLine($"CEP: {cep} (General format)");
		Console.WriteLine($"CEP: {cep:N} (Numeric format)");
	}
}
```

[N.P.]: https://github.com/DotNetCafe/DotNetCafe/tree/master/
[0.1.0]: https://github.com/DotNetCafe/DotNetCafe/tree/v0.1.0/
[0.2.0]: https://github.com/DotNetCafe/DotNetCafe/tree/v0.2.0/
[0.3.0]: https://github.com/DotNetCafe/DotNetCafe/tree/v0.3.0/
