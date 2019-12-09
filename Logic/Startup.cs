using AutoMapper;
using Db;
using Db.Entities;
using DbManager.Generic;
using Dto;
using Logic.Generic;
using Logic.Users;


using Logic.Feats;
using Logic.UserRoles;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Logic.ProductVersions;
using Microsoft.AspNetCore.Identity;
using Logic.RefreshTokens;
using Logic.Authentication;

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
            cfg.CreateMap<User, UserDto>().ReverseMap();
            cfg.CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
            cfg.CreateMissingTypeMaps = true;
        }

        public static void RepositoryRegistration(IServiceCollection services)
        {
            services.AddScoped<IRepository<User>, GenericRepository<User>>();
            services.AddScoped<IRepository<UserRole>, GenericRepository<UserRole>>();
            services.AddScoped<IRepository<Product>, GenericRepository<Product>>();
            services.AddScoped<IRepository<ProductVersion>, GenericRepository<ProductVersion>>();
            services.AddScoped<IRepository<Feat>, GenericRepository<Feat>>();

            services.AddScoped<IRepository<RefreshToken>, GenericRepository<RefreshToken>>();
        }

        public static void TypeRegistration(IServiceCollection services, IConfiguration config)
        {
            //TODO Change to UserSecret
            services.AddDbContext<HeroDbContext>(options => options.UseSqlServer(config["Connection"]));


            services.AddIdentity<User, IdentityRole>()
           .AddEntityFrameworkStores<HeroDbContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });



            
            services.BuildServiceProvider().GetService<HeroDbContext>().Database
                .Migrate(); // Automatic database migration to latest version

            RepositoryRegistration(services); //Register Repositories

            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IUserRoleLogic, UserRoleLogic>();
            services.AddScoped<IFeatLogic, FeatLogic>();

            services.AddScoped<IProductVersionLogic, ProductVersionLogic>();

            services.AddScoped<IRefreshTokenLogic, RefreshTokenLogic>();

            services.AddScoped<IAuthenticationLogic, AuthenticationLogic>();
          /*  services.AddScoped<ILogic<UserRoleDto>, GenericLogic<IRepository<UserRole>, UserRoleDto, UserRole>>();*/
            services.AddScoped<ILogic<ProductDto>, GenericLogic<IRepository<Product>, ProductDto, Product>>();
            services.AddScoped<ILogic<ProductVersionDto>, GenericLogic<IRepository<ProductVersion>, ProductVersionDto, ProductVersion>>();
            services.AddScoped<ILogic<FeatDto>, GenericLogic<IRepository<Feat>, FeatDto, Feat>>();



        }
    }
}
