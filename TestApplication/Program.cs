using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.ObjectModel;
using System.Data;

namespace TestApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var builtConfig = config.Build();
                    var appSettingsPath = builtConfig["ConfigFileLocation"];

                    if (string.IsNullOrWhiteSpace(appSettingsPath))
                    {
                        return;
                    }

                    config.AddJsonFile(appSettingsPath);
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseSerilog((context, configuration) =>
                {
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        configuration
                            .MinimumLevel.Debug()
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                            .MinimumLevel.Override("System", LogEventLevel.Warning)
                            .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information);
                    }
                    else
                    {
                        configuration
                            .MinimumLevel.Information()
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                            .MinimumLevel.Override("System", LogEventLevel.Warning);
                    }

                    var sqlConnectionString = context.Configuration.GetConnectionString("DbConnection");

                    var serilogSqlSinkOptions = new MSSqlServerSinkOptions { TableName = "ApplicationLog" };
                    var columnOpts = new ColumnOptions();
                    columnOpts.Store.Remove(StandardColumn.Properties);
                    columnOpts.Store.Add(StandardColumn.LogEvent);
                    columnOpts.AdditionalColumns = new Collection<SqlColumn>
                    {
                        new("Query", SqlDbType.NVarChar),
                        new("ExecutionTime", SqlDbType.Decimal),
                        new("Dashboard", SqlDbType.NVarChar, dataLength: 100)
                    };

                    var serilogPath = context.Configuration.GetSection("Serilog").GetValue<string>("Path");

                  
#if DEBUG
                    Serilog.Debugging.SelfLog.Enable(Console.WriteLine);
#endif

                  
                });
    }
}