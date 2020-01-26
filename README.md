# DotNetCafe 

![SemVer](https://img.shields.io/github/v/tag/DotNetCafe/DotNetCafe?color=6F4E37&label=SemVer)
![License](https://img.shields.io/github/license/DotNetCafe/DotNetCafe?color=6F4E37&label=License)

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

Tabela de métodos e interfaces implementadas em cada tipo.

Funcionalidade    | CNPJ
----------------- | ---- 
ctor(string)      | sim
Parse             | sim
TryParse          | sim
IsEmpty           | sim
IComparable       | sim
IComparable\<T>   | sim
IEquatable\<T>    | sim
IFormattable      | sim
==, !=            | sim
\>, <, >=, <=     | sim

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
