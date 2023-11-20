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


builder.Services.AddCors(options =>
{
    // var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
    //
    // options.AddPolicy("AllowSpecificOrigin",
    //     builder => builder.WithOrigins("https://main--transcendent-entremet-4b0e75.netlify.app",
    //             "http://main--transcendent-entremet-4b0e75.netlify.app")
    //         .AllowAnyMethod()
    //         .AllowAnyHeader()
    //         .AllowCredentials());

    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();


app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();