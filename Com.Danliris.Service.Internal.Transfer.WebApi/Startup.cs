using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.AccessTokenValidation;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferDeliveryOrderService;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.ExternalTransferOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferRequestService;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.InternalTransferOrderServices;

namespace Com.Danliris.Service.Internal.Transfer.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void RegisterEndpoint()
        {
            APIEndpoint.Core = Configuration.GetValue<string>("CoreEndpoint") ?? Configuration["CoreEndpoint"];
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection") ?? Configuration["DefaultConnection"];

            services
                .AddDbContext<InternalTransferDbContext>(options => options.UseSqlServer(connectionString))
                .AddApiVersioning(options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                });

            services
                .AddTransient<TransferDeliveryOrderService>()
                .AddTransient<TransferDeliveryOrderItemService>()
                .AddTransient<TransferDeliveryOrderDetailService>()

                .AddTransient<TransferDeliveryOrderService>()
                .AddTransient<TransferDeliveryOrderItemService>()
                .AddTransient<ExternalTransferOrderDetailService>();
            services
                .AddTransient<TransferRequestService>()
                .AddTransient<TransferRequestDetailService>()
                .AddTransient<InternalTransferOrderService>()
                .AddTransient<InternalTransferOrderDetailService>()
                .AddTransient<ExternalTransferOrderService>()
                .AddTransient<ExternalTransferOrderItemService>()
                .AddTransient<ExternalTransferOrderDetailService>();

            var Secret = Configuration.GetValue<string>("Secret") ?? Configuration["Secret"];
            var Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        IssuerSigningKey = Key
                    };
                });

            services
                .AddMvcCore()
                .AddAuthorization()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .AddJsonFormatters();

            services.AddCors(options => options.AddPolicy("InternalTransferPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders("Content-Disposition", "api-version", "content-length", "content-md5", "content-type", "date", "request-id", "response-time");
            }));

            RegisterEndpoint();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<InternalTransferDbContext>();
                context.Database.Migrate();
            }

            app.UseAuthentication();
            app.UseCors("InternalTransferPolicy");
            app.UseMvc();
        }
    }
}
