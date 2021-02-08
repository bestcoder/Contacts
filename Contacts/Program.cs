using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Contacts.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Contacts
{
  public class Program
  {

    public static HostBuilderContext HostContext { get; set; }

    public static async Task Main(string[] args)
    {

      var host = CreateHostBuilder(args).Build();
      // await TestDbContext();
      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      var builder = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args);
      builder.ConfigureWebHostDefaults(webBuilder =>
      {
        webBuilder.UseStartup<Startup>();
      });
      return builder;
    }

    /// <summary>
    /// Quick test to confirm that we can use our context to access the DB as configured.
    /// </summary>
    /// <returns></returns>
    public async static Task TestDbContext()
    {
      using (var ctx = new DemoModel())
      {
        var entity = await ctx.Contacts.FirstOrDefaultAsync();
      }
    }

  }
}
