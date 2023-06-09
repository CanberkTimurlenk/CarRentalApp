using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Core.Utilities.IoC;
using Core.Extensions;
using WebAPI.ContextFactory;
using Microsoft.EntityFrameworkCore.Design;
using DataAccess.Concrete.EntityFramework.Contexts;
using WebAPI.Extensions.ServicesExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//  https://learn.microsoft.com/en-us/aspnet/core/migration/50-to-60-samples?view=aspnetcore-5.0
//  Chapter: Custom dependency injection (DI) container 

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });
builder.Services.AddSingleton<IDesignTimeDbContextFactory<CarAppContext>> (new CarAppContextFactory());

//  Extension Methods
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ApplyAuthentication(tokenOptions);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();

