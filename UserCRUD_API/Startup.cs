using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Application;
using Application.Commands;
using Application.Commands.Handler;
using Application.Commands.Usuario;
using Application.DTO;
using Application.Interfaces;
using Application.Reads.Queries;
using AutoMapper;
using Domain.Entities;
using Infra;
using Infra.Interfaces;
using Infra.Repository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace UserCRUD_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IRequestHandler<ObterTodosUsersQuery, List<UserDTO>>, ObterTodosUsersQueryHandler>();
            services.AddTransient<IRequestHandler<ObterUserPorIdQuery, UserDTO>, ObterUserPorIdQueryHandler>();
            services.AddTransient<IRequestHandler<CadastrarUsuarioCommand, CommandResult>, CadastrarUsuarioCommandHandler>();
            services.AddTransient<IRequestHandler<DeletarUsuarioCommand, CommandResult>, DeletarUsuarioCommandHandler>();
            services.AddControllers();
            services.AddMediatR(typeof(Startup));
            services.AddSingleton<IRepository<User>, UsuarioRepository>();

            var awsOptions = Configuration.GetAWSOptions();
            services.AddDefaultAWSOptions(awsOptions);

            var config = new AmazonDynamoDBConfig
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast2
            };
            var client = new AmazonDynamoDBClient(config);
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddSingleton<DynamoDBContext>();
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
            services.AddSingleton<IAmazonDynamoDB>(client);
            services.AddScoped<IMediator, Mediator>();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>().ReverseMap();
            });

            services.AddAutoMapper(typeof(CadastrarUsuarioCommand));
            services.AddAutoMapper(typeof(ObterUserPorIdQuery));
            services.AddAutoMapper(typeof(ObterTodosUsersQuery));
            services.AddAutoMapper(typeof(Startup));


            services.AddCors(options => options.AddDefaultPolicy(
                builder => builder.AllowAnyOrigin()
                ));
            services.AddDbContext<Context>(opt =>
               opt.UseInMemoryDatabase("UserList"));

            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            services.AddControllers();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Lucas Coacci",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/coaccilucas"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(x => x.LogTo(Console.WriteLine, LogLevel.Information));

            return services;
        }
    }
}

