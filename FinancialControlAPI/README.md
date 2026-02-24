Personal Finance Manager API
📌 Descrição

API REST desenvolvida em ASP.NET Core para gerenciamento de usuários e controle financeiro, permitindo cadastro de transações, filtragem por período e cálculo de dados financeiros.

Projeto desenvolvido com foco em boas práticas de desenvolvimento backend, incluindo uso de DTOs, validação de dados, relacionamento entre entidades e integração com banco de dados SQL Server.

🛠 Tecnologias Utilizadas

• C#

• ASP.NET Core

• Entity Framework Core

• SQL Server

• LINQ

• Swagger (Swashbuckle)

📂 Estrutura do Projeto

• Controllers → Responsáveis pelos endpoints da API

• Entities → Representação das tabelas do banco

• DTOs → Objetos de transferência de dados

• Data → Configuração do DbContext e conexão com banco

🚀 Funcionalidades Implementadas

• CRUD de Usuários

• CRUD de Transações

• Relacionamento entre Usuário e Transações

• Filtro de transações por período

• Validações com DataAnnotations

• Respostas HTTP adequadas (200, 201, 204, 400, 404)

💾 Banco de Dados

Banco de dados SQL Server configurado via Entity Framework Core com migrations.

▶ Como Executar o Projeto

1. Clonar o repositório

2. Configurar a connection string no appsettings.json

3. Executar as migrations:

dotnet ef database update

Rodar o projeto:

dotnet run

Acessar o Swagger:

https://localhost:{porta}/swagger

🧠 Conceitos Aplicados

Durante o desenvolvimento deste projeto foram aplicados os seguintes conceitos:

• Arquitetura REST para organização dos endpoints

• Separação de responsabilidades (Controllers, Entities, DTOs, Data)

• Uso de DTOs para evitar exposição direta das entidades

• Relacionamento 1:N entre Usuário e Transações

• Validação de dados com DataAnnotations

• Migrations com Entity Framework Core

• Injeção de Dependência (Dependency Injection)

• Programação assíncrona com async/await

• Boas práticas de retorno HTTP (200, 201, 204, 400, 404)