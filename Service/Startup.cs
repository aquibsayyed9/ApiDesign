using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzPay.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using EzPay.Repository;
using Microsoft.AspNetCore.Http;
using EzPay.Business.Interfaces;
using EzPay.Business.Classes;
using EzPay.DataAccess.Interfaces;
using EzPay.DataAccess.Classes;
using AutoMapper;
using EzPay.Mapping;
using EzPay.Repository.Interfaces;
using EzPay.EFCore;
using EzPay.DTO;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace EzPayService
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
            services.AddControllers();

            services.AddEntityFrameworkSqlServer();
            services.AddDbContextPool<MainDbContext>((serviceProvider, options) => { options.UseSqlServer(Configuration.GetConnectionString("connStr")); });
            services.AddScoped<DbContext, MainDbContext>();
            //Repository registration
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<ITowerRepository, TowerRepository>();
            services.AddScoped<IChargeTypeRepository, ChargeTypeRepository>();
            services.AddScoped<IParameterRepository, ParameterRepository>();
            services.AddScoped<IApiDetailsRepository, ApiDetailsRepository>();
            services.AddScoped<IPaymentTransactionsRepository, PaymentTransactionsRepository>();
            services.AddScoped<IPaymentGatewayRepository, PaymentGatewayRepository>();
            services.AddScoped<IOtpTransactionRepository, OtpTransactionRepository>();
            //Business registration
            services.AddScoped<ICompanyBusiness, CompanyBusiness>();
            services.AddScoped<IRegionBusiness, RegionBusiness>();
            services.AddScoped<ITowerBusiness, TowerBusiness>();
            services.AddScoped<IChargeTypeBusiness, ChargeTypeBusiness>();
            services.AddScoped<IParameterBusiness, ParameterBusiness>();
            services.AddScoped<IPaymentBusiness, PaymentBusiness>();
            services.AddScoped<IEmailBusines, EmailBusiness>();
            services.AddScoped<IOtpTransactionBusiness, OtpTransactionBusiness>();
            //services.AddScoped<IBalancesBusinessAccess, BalancesBusinessAccess>();
            services.AddScoped<IApiDetailsBusinessAccess, ApiDetailsBusinessAccess>();
            services.AddScoped<ICyberSource, CyberSource>();
            services.AddScoped<IComTrustBusiness, ComTrustBusiness>();
            //Data registration
            services.AddScoped<ICompanyDataAccess, CompanyDataAccess>();
            services.AddScoped<IRegionDataAccess, RegionDataAccess>();
            services.AddScoped<ITowerDataAccess, TowerDataAccess>();
            services.AddScoped<IChargeTypeDataAccess, ChargeTypeDataAccess>();
            services.AddScoped<IParameterDataAccess, ParameterDataAccess>();
            services.AddScoped<IApiDetailsDataAccess, ApiDetailsDataAccess>();
            services.AddScoped<IPaymentDataAccess, PaymentDataAccess>();
            services.AddScoped<IOtpDataAccess, OtpDataAccess>();

            //Register AppSettings class for reading configurations
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var mappingConfiguration = new MapperConfiguration(config => config.AddProfile(new Mappers()));
            IMapper mapper = mappingConfiguration.CreateMapper();
            services.AddSingleton(mapper);
            services.AddHttpClient<IBalancesBusinessAccess, BalancesBusinessAccess>();

            services.AddCors(cors => cors.AddPolicy("EzPayCors", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod()
                .AllowAnyHeader();
            }));
            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader());
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseStaticFiles(); // For the wwwroot folder

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                                    Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
                RequestPath = new PathString("/app-images")
            });

            app.UseHttpsRedirection();

            app.UseCors("EzPayCors");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
