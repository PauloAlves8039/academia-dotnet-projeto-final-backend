<h1 align="center">Academia .NET - Estacionamento de Motos - Backend</h1>

<p align="center">
  <a href="https://learn.microsoft.com/pt-br/dotnet/"><img alt="DotNet 6" src="https://img.shields.io/badge/.NET-5C2D91?logo=.net&logoColor=white&style=for-the-badge" /></a>
  <a href="https://learn.microsoft.com/pt-br/dotnet/csharp/programming-guide/"><img alt="C#" src="https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white&style=for-the-badge" /></a>
  <a href="https://www.microsoft.com/pt-br/sql-server/sql-server-downloads"><img alt="SQL Server" src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white" /></a>
</p>

## :computer: Projeto

Esse projeto faz a simulação de um sistema de gerenciamento para um estacionamento de motos, se trata do desafio final da Academia .NET ministrada pela `Universidade Franciscana - UFN`.

Este repositório esta relacionando ao [Projeto Final Frontend](https://github.com/PauloAlves8039/academia-dotnet-projeto-final-frontend)

## :blue_book: Regra de Negócio

``Cadastro do Cliente``: 
- Na primeira visita o cliente recebe um atendimento para a realização de cadastro fornecendo dados pessoais. 

``Entrada no Estacionamento``: 
- Os campos de data de saída e valor total do serviço ficam em aberto até a retirada da moto.

``Retirada da Moto``:
- Ao retornar, o cliente solicita a retirada da moto.
- Quando a baixa do serviço é realizada os campos que não tinham sido preenchidos recebem seus respectivos valores, em questão a data e hora da data de saída, o cálculo do valor total baseado no tempo de permanência.

``Emissão de Comprovante``:
- Após a baixa do serviço, é emitido um comprovante com todos os dados preenchidos e entregue ao cliente.

``Emissão de Comprovante``:
- Para clientes frequentes, o atendente pode abrir um novo controle de permanência sem a necessidade de um novo cadastro.

## :movie_camera: Vídeo de Demonstração

- Apresentação [Projeto Estacionamento de Moto](https://www.youtube.com/watch?v=H34bsYdyjhU)
  
## ✔️ Recursos Utilizados

- ``Arquitetura MVC``
- ``.NET 6.0``
- ``ASP.NET Core WebAPI``
- ``C#``
- ``Entity Framework Core``
- ``SQL Server``
- ``AutoMapper``
- ``Identity Framework Core``
- ``JWT``
- ``Swagger``

## :floppy_disk: Clonar Repositório

```bash
git clone https://github.com/PauloAlves8039/academia-dotnet-projeto-final-backend.git
```

## Conclusão

Essa aplicação tem como objetivo oferecer melhorias para o uso de um sistema voltado para uma empresa fictícia que atua com o gerenciamento de um estacionamento de motos, a ideia surgiu por conta de uma necessidade que ocorre em localidades que tenham dificuldades para achar um estacionamento organizado, seguro e que ofereça um serviço com qualidade. 

## :boy: Author

<a href="https://github.com/PauloAlves8039"><img src="https://avatars.githubusercontent.com/u/57012714?v=4" width=70></a>
[Paulo Alves](https://github.com/PauloAlves8039)
