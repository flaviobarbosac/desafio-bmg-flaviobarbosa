using BmgPromotionApi.Data;
using BmgPromotionApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os servi�os ao cont�iner
builder.Services.AddControllers();

// Registra nossos servi�os customizados
builder.Services.AddSingleton<PromotionStore>();
builder.Services.AddScoped<PromotionService>();

// Configura��o do Swagger (apenas se o pacote estiver instalado)
try
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}
catch
{
    // Se der erro, continua sem Swagger por enquanto
}

var app = builder.Build();

// Pipeline de requisi��es
if (app.Environment.IsDevelopment())
{
    try
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    catch
    {
        // Se der erro, continua sem Swagger UI
    }
}

app.UseAuthorization();
app.MapControllers();

app.Run();