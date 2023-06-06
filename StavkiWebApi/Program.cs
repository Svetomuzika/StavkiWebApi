using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stavki.Data.Data;
using Stavki.Infrastructure.Auth;
using Stavki.Infrastructure.Autofac;
using Stavki.Infrastructure.EF;
using Stavki.Infrastructure.Services;
using Stavki.Infrastructure.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

var a = new ChatHub(null).Send(new CommentInfo());

builder.Services.AddDbContext<ApplicationContext>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfire(config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage("Server=(localdb)\\mssqllocaldb;Database=StavkiDB;Trusted_Connection=True;"));

builder.Services.AddHangfireServer();


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModules()));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = AuthOptions.ISSUER,

                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,

                            // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };
                    });

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

builder.Services.AddSignalR();



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseDeveloperExceptionPage();
app.UseCors("AllowAll");
app.UseHangfireDashboard();

app.MapHub<ChatHub>("api/chat");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHangfireDashboard();

JobScheduler.Run();

app.UseAuthentication();
app.UseAuthorization();


app.Run();