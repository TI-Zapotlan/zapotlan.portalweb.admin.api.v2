using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Text.Json.Serialization;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;
using Zapotlan.PortalWeb.Admin.Core.Services;
using Zapotlan.PortalWeb.Admin.Infrastructure.Data;
using Zapotlan.PortalWeb.Admin.Infrastructure.Filters;
using Zapotlan.PortalWeb.Admin.Infrastructure.Mappings;
using Zapotlan.PortalWeb.Admin.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DesarrolloConnection");
// Add services to the container.

builder.Services.AddControllers(options => { 
    options.Filters.Add<GlobalExceptionFilter>();
}).AddNewtonsoftJson(options => {
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
}).AddJsonOptions(options => 
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
);
    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<PortalWebDbContext>(options => 
    options.UseSqlServer(connectionString)
);

builder.Services.AddTransient<IAdministracionRepository, AdministracionRepository>();
builder.Services.AddTransient<IAdministracionService, AdministracionService>();
builder.Services.AddTransient<IAdministracionMapping, AdministracionMapping>();
builder.Services.AddTransient<IAreaService, AreaService>();
builder.Services.AddTransient<IAreaMapping, AreaMapping>();
builder.Services.AddTransient<IPersonaService, PersonaService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
