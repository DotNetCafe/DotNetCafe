# DotNetCafe 

![.Net Core 3.1](https://github.com/DotNetCafe/DotNetCafe/workflows/.Net%20Core%203.1/badge.svg)
![SemVer](https://img.shields.io/github/v/tag/DotNetCafe/DotNetCafe?label=SemVer&sort=semver)
![License](https://img.shields.io/github/license/DotNetCafe/DotNetCafe?label=License)

## Introdução

O **DotNetCafe** é uma biblioteca com tipos complexos bem definidos. Pois no dia-a-dia lidamos com inúmeros valores candidatos a tipos complexos que acabam sendo rebaixados ou menosprezados apenas a strings, causando uma bagunça tremenda no código devido a quantidade de métodos auxiliares que são elaborados para o tratamento destes.

Para evitar que seu projeto vire uma bagunça, surge o **DotNetCafe**, e como um bom café, a forma de preparo e a qualidade dos grãos devem ser considerados. A forma de preparo dos tipos definidos no **DotNetCafe** levam em consideração o próprio tipo em si e sua representação no mundo real.

A qualidade do código é concisa e limpa, suficientemente eficaz para efetuar a análise do dado, validação e formatação com uma boa performance. O código não é dificil de ler para não comprometer a manutenção da biblioteca.

## Compatibilidade

### .Net Core e .NET Standard
DotNetCafe | .Net Core | .Net Standard
---------- | --------- | -------------
0.1.0      | 3.1       | 2.1          

### Tipos Suportados

A tabela abaixo possui os tipos suportados e a versão em que foi implantado ou modificado:

Funcionalidade    | CNPJ    | CPF
----------------- | ------- | ------
ctor(string)      | [0.1.0] | [N.P.]
Parse             | [0.1.0] | [N.P.]
TryParse          | [0.1.0] | [N.P.]
IsEmpty           | [0.1.0] | [N.P.]
IComparable       | [0.1.0] | [N.P.]
IComparable\<T>   | [0.1.0] | [N.P.]
IEquatable\<T>    | [0.1.0] | [N.P.]
IFormattable      | [0.1.0] | [N.P.]
==, !=            | [0.1.0] | [N.P.]
\>, <, >=, <=     | [0.1.0] | [N.P.]

*\***N.P.** não publicado.*

## Utilizando a biblioteca

### 1. Configure o CSPROJ

```xml
<PackageReference Include="DotNetCafe" Version="0.1.0" />
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
		Cnpj cnpj1 = GetCnpj("191");
		Console.WriteLine($"Success: {cnpj1}");
		
		Cnpj cnpj2 = GetCnpj("00.000.000/0001-91");
		Console.WriteLine($"Success: {cnpj2:N}");
		
		Console.WriteLine(cnpj1 == cnpj2);
	}
	
	private static Cnpj GetCnpj(string input)
	{
		Cnpj cnpj = Cnpj.Empty;	
		try
		{
			cnpj = Cnpj.Parse(input);
		}
		catch (ArgumentException ex)
		{
			Console.Error.WriteLine($"Fail: {input}: {ex.Message}");
			Environment.Exit(1);
		}
		catch (FormatException ex)
		{
			Console.Error.WriteLine($"Fail: {input}: {ex.Message}");
			Environment.Exit(1);
		}
		return cnpj;
	}
}
```

Ou se preferir você pode testar no [.Net Fiddle](https://dotnetfiddle.net/gOFTHr) ;)

[N.P.]: https://github.com/DotNetCafe/DotNetCafe/tree/master/
[0.1.0]: https://github.com/DotNetCafe/DotNetCafe/tree/v0.1.0/