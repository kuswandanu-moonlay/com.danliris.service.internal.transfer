﻿using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.ExternalTransferOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.InternalTransferOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferRequestService;
using Com.Danliris.Service.Internal.Transfer.Test.DataUtils.ExternalTransferOrderDataUtils;
using Com.Danliris.Service.Internal.Transfer.Test.DataUtils.InternalTransferOrderDataUtils;
using Com.Danliris.Service.Internal.Transfer.Test.DataUtils.TransferRequestDataUtils;
using Com.Danliris.Service.Internal.Transfer.Test.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace Com.Danliris.Service.Internal.Transfer.Test
{
    public class ServiceProviderFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public void RegisterEndpoint(IConfigurationRoot Configuration)
        {
            APIEndpoint.Core = Configuration.GetValue<string>("CoreEndpoint") ?? Configuration["CoreEndpoint"];
        }

        public ServiceProviderFixture()
        {
            /*
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json")
                .Build();
            */

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new List<KeyValuePair<string, string>>
                {
                    /*
                    new KeyValuePair<string, string>("Authority", "http://localhost:5000"),
                    new KeyValuePair<string, string>("ClientId", "dl-test"),
                    */
                    new KeyValuePair<string, string>("Secret", "DANLIRISTESTENVIRONMENT"),
                    new KeyValuePair<string, string>("ASPNETCORE_ENVIRONMENT", "Test"),
                    new KeyValuePair<string, string>("CoreEndpoint", "http://localhost:5001/v1/"),
                    new KeyValuePair<string, string>("DefaultConnection", "Server=localhost,1401;Database=com.danliris.db.internal.transfer.service.test;User=sa;password=Standar123.;MultipleActiveResultSets=true;")
                })
                .Build();

            RegisterEndpoint(configuration);
            string connectionString = configuration.GetConnectionString("DefaultConnection") ?? configuration["DefaultConnection"];

            this.ServiceProvider = new ServiceCollection()
                .AddDbContext<InternalTransferDbContext>((serviceProvider, options) =>
                {
                    options.UseSqlServer(connectionString);
                }, ServiceLifetime.Transient)

                .AddTransient<TransferRequestService>(provider => new TransferRequestService(provider))
                .AddTransient<TransferRequestDetailService>(provider => new TransferRequestDetailService(provider))

                .AddTransient<InternalTransferOrderService>(provider => new InternalTransferOrderService(provider))
                .AddTransient<InternalTransferOrderDetailService>(provider => new InternalTransferOrderDetailService(provider))

                .AddTransient<ExternalTransferOrderService>(provider => new ExternalTransferOrderService(provider))
                .AddTransient<ExternalTransferOrderItemService>(provider => new ExternalTransferOrderItemService(provider))
                .AddTransient<ExternalTransferOrderDetailService>(provider => new ExternalTransferOrderDetailService(provider))

                .AddTransient<TransferRequestDataUtil>()
                .AddTransient<TransferRequestDetailDataUtil>()

                .AddTransient<InternalTransferOrderDataUtil>()
                .AddTransient<InternalTransferOrderDetailDataUtil>()

                .AddTransient<ExternalTransferOrderDataUtil>()
                .AddTransient<ExternalTransferOrderItemDataUtil>()
                .AddTransient<ExternalTransferOrderDetailDataUtil>()

                .AddSingleton<HttpClientService>()

                .BuildServiceProvider();

            InternalTransferDbContext dbContext = ServiceProvider.GetService<InternalTransferDbContext>();
            dbContext.Database.Migrate();
        }

        public void Dispose()
        {
        }
    }

    [CollectionDefinition("ServiceProviderFixture Collection")]
    public class ServiceProviderFixtureCollection : ICollectionFixture<ServiceProviderFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}