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

- `Cadastro do Cliente`: na primeira visita o cliente recebe um atendimento para a realização do cadastro fornecendo dados pessoais. 

- `Entrada no Estacionamento`: para guardar a moto no estacionamneto é emitido um `ticket` com dados da `permanância` da moto informando a `data de entrada`, `placa da moto` e `referência` ao cliente.

-  `Ticket Entregue ao Cliente`: a maioria dos campos são preenchidos na abertura da permanência, os campos para `data de saída` e `valor total` ficam em aberto, já o campo `estado da permanência` recebe o valor `estacionado` indicando que a moto esta no estacionamento. 

-  `Retirada da Moto`: o cliente apresenta o `ticket` entregue na abertura da permanência para o procedimento de `conclusão`, nessa etapa os campos `data de saída` e `valor total` são registrados com seus valores informando a `data e hora` e o `valor total` da estadia da moto no estacionamento.  

## :hammer: Funcionalidades

- ``Operações``: para todas as entidades da aplicação é possível realizar as operações básicas como `consultar listas`, `pesquisar registros individuais`, `cadastrar`, `atualizar` e `excluir`. 
- ``Segurança``: é permitido o cadastro de novos `usuários` e os processos para `Autenticação` e `Autorização` desses mesmos usuários. 

## :movie_camera: Vídeos de Demonstrações

- Apresentação Durante o Desenvolvimento [Projeto Estacionamento de Moto](https://www.youtube.com/watch?v=H34bsYdyjhU&t=281s)
- Apresentação Após o Desenvolvimento [Projeto Estacionamento de Moto](https://www.youtube.com/watch?v=6eEUoA9NbTg&t=489s)

## ✔️ Recursos Utilizados

- `Padrão MVC`
- `Arquitetura em Camadas`
- `.NET 6.0`
- `ASP.NET Core WebAPI`
- `C#`
- `Entity Framework Core`
- `SQL Server`
- `AutoMapper`
- `Microsoft Identity`
- `JWT`
- `Swagger`

## :white_check_mark: Decisões Técnicas

1. Na estrutura do projeto fiz a criação de 4 camadas:

- `Estacionamento.Model`: tem como objetivo definir as classes de `modelo` que fazem parte da regra de negócio.
- `Estacionamento.Data`: é uma subcamada de Mdoel, seu propósito é a aplicação da `persistência` com o banco de dados.
- `Estacionamento.Service`: é uma subcamada que representa a utilização da `regra de negócio`.
- `Estacionamento.WebAPI`: é responável por gerenciar os `controladores` seus `endpoints` e configuração da aplicação.

2. Motivações para a criação das camadas.

- A intenção foi criar uma estrutura organizada e que permita a condição de uso para os seguintes recursos:  

- `Reutilização de Código`: com a divisão do código as funcionalidades podem ser reutilizadas em `diferentes partes` específicas do projeto.
- `Facilidade de Manutenção`: como cada camada tem a sua responsabilidade clara isso facilita a manutenção do código, caso haja uma `mudança nos requisistos` de qualquer camada ou alterações necessárias podem ser feitas sem afetar outras partes do projeto.
- `Testabilidade`: nesse ponto cada camada pode ser testada de forma independente com a aplicação de `testes de unidade` e demais testes de forma separada.
- `Escalabilidade`: permite que a aplicação `cresça` se torne `adaptável` de uma forma mais eficiente com as mudanças da regra de negócio, ou seja, com o acréscimo de novos recursos.

3. O uso do `Microsoft Identity` e o `JWT`

- A escolha do `Identity` foi para aplicar o gerenciamento de identidade dos usuários, na regra de negócio do projeto não inclui a utilização de algum entidade para o usuário, pensando nisso decide aplicar os recurso armazenamento e criação dessas credenciais, e o `JWT` atua de forma colaborativa nesse processo envolvendo `Autenticação` e `Autorização` de usuários.

4. O uso do `Repository Pattern`

- A adição desse padrão tem como objetivo aplicar o encapsulamento da lógica de acesso a dados.

5. O uso do `Repository Pattern`

- O uso deste padrão foi acrescentado para definir uma separação da regra de negócio das demais partes da aplicação.

6. A criação de uma `Claim` para o usuário `admin`

- A criação de uma claim para o usuário `admin@localhost`, tomei essa decisão para acresentar um usuário com permissões `administrativas`, como por exemplo e exclusão de registros, usuários comuns não tem autorização para excluir os registros, apenas o usuário `admin@localhost`, o motivo foi para simular alguns cenários de atuação de softwares em ambientes de produção com regras mais rígidas de segurança. `Observação`: a definição do usuário `admin@localhost` para ser aplicada em ambientes de produção deve se ter outro tipo de abordagem mais apropriada.

7. A definição automatizada de alguns campos na entidade Permanência:

- A decisão de automatizar os campos `data de saída`, `valor total` e `estado da permanância` tem como objetivo agilizar a conclusão da estadia da moto no estacionamento, no meu ponto de vista faria mais sentido ter as definições apliacadas de forma prática. 

8. A criação da entidade `Veiculo` e não `Moto`:

- Para representar as models de meu projeto fiz a criação das classes `Cliente`, `Endereco`, `Veiculo`, `ClienteVeiculo` e `Permanencia`, a ideia foi criar uma classe um pouco mais genérica para representar o veículo já pensando em futuras melhorias, um tipo de cenário pensado foi, se o proprietário do estacionamento decidir trabalhar com carros, essa classe pode ser reaproveitada e adaptada para ser usada como referência em classes do tipo `Moto` e `Carro`.    

## :wrench: Utilização do Projeto

- Após baixar ou clonar o projeto navegue até [appsettings.json](https://github.com/PauloAlves8039/academia-dotnet-projeto-final-backend/blob/master/src/Estacionamento.WebAPI/appsettings.json) e atualize a sua `string de conexão` de acordo com as suas credenciais do `SQL Server`.

- Em seguida execute o comando `Update-Database` para a geração da base de dados, se por um acaso enontrar dificuldades nesse ponto pode ser feita a criação do banco de dados diretamente no `SQL Server` executando o script [script-estacionamento.sql](https://github.com/PauloAlves8039/academia-dotnet-projeto-final-backend/blob/master/Recursos/Banco%20de%20Dados/Script%20do%20Banco%20de%20Dados/script-estacionamento.sql) que foi gerado durante o desenvolvimento.

- Com o projeto configurado e sendo executado pode ser feita a criação de usuários para utilização da WebAPI, para testar todas funcionalidade recomendo a criação de um usuário chamado `admin@localhost` a senha pode ser a de sua preferência, um exemplo, `SuaSenha@2014`

## :muscle: Pontos de melhorias

- Uma implementação mais estruturada para a segurança, a aplicação tem `segurança implementada` mas se for um projeto que tenha pretenções de ser levado para ambientes de produção precisa de uma estruturação mas definida. Tentei aplicar o conceito de segurança da forma que costumo desenvolver, porém me deparei com obstáculo que não tinha encontrado antes e que levaram a decidir mudar de abordagem para a solução desta etapa do desafio.   

- Caso seja necessário a inclusão de uma classe ou uma subcamada extra para tratar das `Configurações` e `Injeção de Dependência`, é uma prática comum em projetos que usam outros tipos de arquitetura, como uma melhoria futura é um ponto interessante.

- A criação de `Testes de Unidade`, durante o treinamento da `UFN` a nossa turma não teve como disciplina a criação de testes de unidade, no qual se trata de um meus principais pontos de interresses que é buscar amadurecimento com essa boa prática que tenho inteção de definri nesse projeto para mais adiante.       

## :floppy_disk: Clonar Repositório

```bash
git clone https://github.com/PauloAlves8039/academia-dotnet-projeto-final-backend.git
```

## Diagrama do Banco de Dados

<p align="center"> <img src="https://github.com/PauloAlves8039/academia-dotnet-projeto-final-backend/blob/master/Recursos/Diagrama/diagrama-estacionamento-de-motos.PNG" /></p>

## WebAPI

<p align="center"> <img src="https://github.com/PauloAlves8039/academia-dotnet-projeto-final-backend/blob/master/Recursos/Screenshots/webapi.PNG" /></p>
  
## Conclusão

Essa aplicação tem como objetivo oferecer melhorias para o uso de um sistema voltado para uma empresa fictícia que atua com o gerenciamento de um estacionamento de motos, a ideia surgiu por conta de uma necessidade que ocorre em localidades que tenham dificuldades para achar um estacionamento organizado, seguro e que ofereça um serviço com qualidade. 

## :boy: Author

<a href="https://github.com/PauloAlves8039"><img src="https://avatars.githubusercontent.com/u/57012714?v=4" width=70></a>
[Paulo Alves](https://github.com/PauloAlves8039)
