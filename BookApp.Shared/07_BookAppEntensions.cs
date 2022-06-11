using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Shared
{
    public static class BookAppEntensions
    {
        public static void AddDependencyInjectionContainerForBookApp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<BookAppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

            // db container에 repository 등록
            services.AddTransient<IBookRepository, BookRepository>();
        }

        public static void AddDependencyInjectionContainerForBookApp(this IServiceCollection services, string connectionString)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<BookAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // db container에 repository 등록  
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IHaruUserRepository, HaruUserRepository>();
        }
    }
}
