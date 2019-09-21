using AutoMapper;
using Db;
using Db.Entities;
using DbManager.Generic;
using Dto;
using Logic.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Logic
{
    public static class Startup
    {
        public static void Configuration(IConfiguration config)
        {
            Mapper.Initialize(CreateMappings);
            Mapper.AssertConfigurationIsValid();
        }
        private static void CreateMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<User, UserDto>();
          
            cfg.CreateMissingTypeMaps = true;
        }

        public static void RepositoryRegistration(IServiceCollection services)
        {
            services.AddScoped<IRepository<User>, GenericRepository<User>>();
            
        }

        public static void TypeRegistration(IServiceCollection services, IConfiguration config)
        {
            //TODO Change to UserSecret
            services.AddDbContext<HeroDbContext>(options => options.UseSqlServer(config["Connection"]));
            services.BuildServiceProvider().GetService<HeroDbContext>().Database

                .Migrate(); // Automatic database migration to latest version
            RepositoryRegistration(services); //Register Repositories

            services.AddScoped<IUserLogic, UserLogic>();
      
        }
    }
}
