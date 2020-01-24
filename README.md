# DotNetCafe 

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

```shell
$ dotnet-script
```

```csharp
> #r "src/DotNetCafe/bin/Debug/netstandard2.1/DotNetCafe.dll"
> using DotNetCafe;
> var x = Cnpj.Parse("191");
> x
["00.000.000/0001-91"]
> var y = Cnpj.Parse("00.000.000/0001-91");
> x == y
true
```
