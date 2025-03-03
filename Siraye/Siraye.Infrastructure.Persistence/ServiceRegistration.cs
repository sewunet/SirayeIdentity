﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Siraye.Application.Interfaces;
using Siraye.Application.Interfaces.Repositories;
using Siraye.Infrastructure.Persistence.Contexts;
using Siraye.Infrastructure.Persistence.Repositories;
using Siraye.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Siraye.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }
            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<ITaskRepositoryAsync, TaskRepositoryAsync>();
            #endregion
        }
    }
}
