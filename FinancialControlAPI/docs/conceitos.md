# Conceitos do Projeto FinancialControlAPI

## Program.cs
Responsável por configurar serviços e o pipeline HTTP da aplicação.

## AddDbContext
Registra o AppDbContext no container de Injeção de Dependência.

## Middleware
Componente que intercepta requisições no pipeline HTTP.

## Serviço
Classe registrada no container que pode ser injetada via construtor.

1? O que aconteceria se você removesse : DbContext ? se removermos a herança de DbContext a classe deixa de ser reconhecida como contexto do ef impossibilitando mapeamento de entidades migrations e operações de persistencia

2 Para que serve DbSetUser? DbSet representa uma coleção de entidades que será mapeada para uma tabela no banco, permitindo consultas e manipulação de dados via ef core

3 O que é DbContextOptions? DbContextOptions contém as configurações do contexto, como string de conexão e provider do banco, e é utilizado para configurar o comportamento do DbContext

4 Quando o SQL realmente é executado? O SQL é executado apenas quando o SaveChanges() é chamado, pois o DbContext utiliza o padrão unit of work e acumula as alterações antes de persistir no banco

Unit of Work é um padrão que rastreia alterações nas entidades e executa todas de uma vez ao chamar SaveChanges, garantindo consistência transacional.