using ChatHub;
using ChatHub.Contracts;
using ChatHub.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactAppChat", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();

    });
});

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IChatMessageCache, ChatMessageCache>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactAppChat");
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHubBase>("/chat-hub");
app.Run();
