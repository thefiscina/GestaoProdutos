using GestaoProduto.Dominio;
using GestaoProduto.Dominio.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace GestaoProduto.Infraestrutura
{
    public class Initializer
    {
        IServiceCollection _services;
        IConfiguration _configuration;
        public Initializer(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }
        public virtual void DataBaseContext()
        {
            var server = _configuration["database:postgresql:server"];
            var port = _configuration["database:postgresql:port"];
            var database = _configuration["database:postgresql:database"];
            var username = _configuration["database:postgresql:username"];
            var password = _configuration["database:postgresql:password"];
            var ssl = _configuration["database:postgresql:ssl"];
            var certificate = _configuration["database:postgresql:certificate"];


            _services.AddDbContext<GestaoProdutoContext>(options =>
            {
                options.UseNpgsql($"Server={server}; Database={database}; User Id={username}; Password={password};SSL Mode={ssl}; Trust Server Certificate={certificate};", opt =>
                {
                    opt.CommandTimeout(180);
                    opt.EnableRetryOnFailure(5);
                });
            });
        }

        public virtual void Services()
        {
            _services.AddScoped<IProductService, ProductService>();        
            _services.AddScoped<AppSetting>();
        }

        public virtual void Settings()
        {
            IConfigurationSection tokenSettingsSection = _configuration.GetSection("TokenSettings");
            _services.Configure<TokenSettings>(tokenSettingsSection);

            //Acessando o AppSetting
            IConfigurationSection appSettingsSection = _configuration.GetSection("AppSettings");
            _services.Configure<AppSetting>(appSettingsSection);
        }
    }
}
