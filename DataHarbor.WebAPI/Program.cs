
using DataHarbor.Common.Repository;
using DataHarbor.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using MassTransit;
using Microsoft.AspNetCore.Http.Json;

namespace DataHarbor.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                        .AllowAnyOrigin() // ("http://localhost:4200")  // Add your frontend URL here
                        .AllowAnyMethod()
                        .AllowAnyHeader(); // Allow any headers
                                           //.AllowCredentials();
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("Keycloak", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(builder.Configuration["Keycloak:Authorizationurl"]),
                            Scopes = new Dictionary<string, string>
                            {
                                {"openid","openid" },{"profile","profile"}
                            }
                        }
                    }
                });

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new  OpenApiReference
                            {
                                    Id="Keycloak" ,
                                Type =ReferenceType.SecurityScheme
                            },In=ParameterLocation.Header,Name="Bearer" , Scheme="Bearer"


                        },[]
                    }

                };
                o.AddSecurityRequirement(securityRequirement);
            });
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(o =>
                 {
                     o.RequireHttpsMetadata = false;
                     o.Audience = builder.Configuration["Keycloak:Audience"];
                     o.MetadataAddress = builder.Configuration["Keycloak:MetadataAddress"];

                     o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                     {
                         ValidIssuer = builder.Configuration["Keycloak:ValidIssuer"],
                     };
                 });

            builder.Services.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.IncludeFields = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddTransient(typeof(IRepository<>), typeof(DocumentRepository<>));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Data Harbor API");
            //    c.OAuthClientId(builder.Configuration["Keycloak:ClientId"]);
            //    c.OAuthClientSecret(builder.Configuration["Keycloak:ClientSecret"]);
            //    c.OAuthUsePkce(); // Required for Authorization Code Flow
            //    c.OAuth2RedirectUrl("http://localhost:44379/swagger/index.html");
            //});
            ////}

            app.UseCors("AllowAll");
            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
