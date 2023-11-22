using Microsoft.AspNetCore.Authentication;
using Punk.Application;
using Punk.Infrastructure;
using Punk.Presentation.Handlers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddApplicationInfrastructure(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication("JwtAuthScheme")
    .AddScheme<AuthenticationSchemeOptions, JwtAuthenticationHandler>("JwtAuthScheme", null);

var origins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins(origins)
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});

var app = builder.Build();


app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();