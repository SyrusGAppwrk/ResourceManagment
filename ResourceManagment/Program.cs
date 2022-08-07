using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ResourceManagment.Configurations;
using ResourceManagment.Implementations;
using ResourceManagment.Models;
using ResourceManagment.Repository;
using ResourceManagment.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration["ConnectionStrings:myCon"];
builder.Services.AddDbContext<dbResourceMangamentSystemContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<appwrkco_msContext>(options => options.UseSqlServer(connectionString));

// Jwt Configuration 
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
//Jwt Authentication 
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(option =>
    {
        option.SaveToken = true;
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["JwtConfig:ValidAudience"],
            ValidIssuer = configuration["JwtConfig:ValidIssuer"],
            RequireExpirationTime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Secret"])),
            ValidateIssuerSigningKey = true
        };
    });
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Resource Management app API doc",
        Version = "v1",
        Description = "A Worth Resource Management Application!",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Syrus G",
            Email = "syrus.g@appwrk.com"
        }
    });
});
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserProjectRepository, UserProjectRepository>();
builder.Services.AddScoped<ILoginManger, ILoginMangerClass>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAssignTaskRepository, AssignTaskRepository>();
builder.Services.AddScoped<IDailyTimeLogRepository, DailyTImeLogRespository>();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();

app.UseSwagger();

app.UseSwaggerUI(x =>
{
    var prefix = string.IsNullOrEmpty(x.RoutePrefix) ? "." : "..";
    x.SwaggerEndpoint($"{prefix}/swagger/v1/swagger.json", "Resource Management Application API doc");
});