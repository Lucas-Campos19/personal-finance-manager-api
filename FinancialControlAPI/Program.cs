using FinancialControlAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); // monta a estrutura da aplicação antes dela rodar

// Add services to the container.

//AddDbContext registra o contexto com liftime scoped um contexto por requisição http evita conflitos de concorrencia evita sobreposição de dados
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Registra o AppDbContext no container de injeção de dependência

builder.Services.AddControllers(); // habilita controller
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); //permite que endpoints sejam descobertos automaticamente
builder.Services.AddSwaggerGen(); // gera documentação OpenAPI automaticamente

var app = builder.Build(); // a aplicação é construida, essa linha finaliza o container monta o pipeline http prepara o servidor

// Configure the HTTP request pipeline.
// verifica se está rodando a aplicação em ambiente de produção ou desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // força redirecionamento de http para https

app.UseAuthorization(); // ativa middleware de autorização

app.MapControllers(); // maperia rotas de api automaticamente, sem isso os endpoint não funcionariam

app.Run(); // inicio da aplicaçaõ
