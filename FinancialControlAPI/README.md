# 💰 Personal Finance Manager API

## 📌 Sobre o Projeto

API REST desenvolvida em ASP.NET Core para gerenciamento de usuários e controle financeiro.

Permite o cadastro de transações, filtragem por período e geração de dados financeiros, seguindo boas práticas de desenvolvimento backend.

Este projeto foi desenvolvido com foco em arquitetura limpa, organização de código e preparação para cenários reais de mercado.

---

## 🚀 Funcionalidades

- ✔ CRUD de Usuários
- ✔ CRUD de Transações
- ✔ Relacionamento entre Usuário e Transações (1:N)
- ✔ Filtro de transações por período
- ✔ Cálculo de resumo financeiro e estatísticas
- ✔ Soft Delete de usuários
- ✔ Tratamento global de erros (400, 404, 500)

---

## 🧠 Diferenciais do Projeto

- ✔ Arquitetura em camadas (Controller → Service → Data)
- ✔ Uso de DTOs para segurança e desacoplamento
- ✔ Tratamento global de exceções com middleware
- ✔ Uso de exceptions customizadas (NotFoundException)
- ✔ Boas práticas REST (status codes e respostas padronizadas)
- ✔ Código organizado e preparado para escalabilidade

---

## 🛠 Tecnologias Utilizadas

- C#
- ASP.NET Core
- Entity Framework Core
- SQL Server
- LINQ
- Swagger (Swashbuckle)

---

## 📂 Estrutura do Projeto

- Controllers → Endpoints da API
- Services → Regras de negócio
- Entities → Modelos do banco
- DTOs → Transferência de dados
- Data → DbContext e conexão

---

## 💾 Banco de Dados

SQL Server com Entity Framework Core e uso de migrations.

---

## ▶ Como Executar

1. Clonar o repositório
2. Configurar a connection string no `appsettings.json`
3. Executar:

```bash
dotnet ef database update
dotnet run