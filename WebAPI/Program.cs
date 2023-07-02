using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.Utilities.Security.Jwt;
using Core.Extensions;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

//  Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//  https://learn.microsoft.com/en-us/aspnet/core/migration/50-to-60-samples?view=aspnetcore-5.0
//  Chapter: Custom dependency injection (DI) container 

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));
builder.Services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });


var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();


//  Extension Methods
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ApplyAuthentication(tokenOptions);
builder.Services.ConfigureCors();
builder.Services.ConfigureSqlContext(builder.Configuration);


var app = builder.Build();

//  Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.ConfigureExceptionHandler();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();

