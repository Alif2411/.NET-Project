using BLL.Services;
using DAL.EF;
using DAL.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped(typeof(GenericRepo<>));
builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<ProposalRepo>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<JobServices>();
builder.Services.AddScoped<ProposalServices>();
builder.Services.AddScoped<ContractServices>();
builder.Services.AddScoped<PaymentServices>();
builder.Services.AddScoped<RoleServices>();

builder.Services.AddDbContext<Context>(
    Opt =>
    {
        Opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
    }
    );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
