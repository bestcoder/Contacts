using Contacts.Data;
using Contacts.Data.Identity;
using Contacts.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Contacts
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

      // Add support for ASP.NET user sessions and accessing each Http context
      services.AddSession();
      services.AddHttpContextAccessor();
      services.AddTransient(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

      // Add support for the built-in .NET Core Identity and its DB context
      services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("Identity")));
      services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<ApplicationDbContext>();

      // Add support for injecting the app's primary DB context
      services.AddDbContext<DemoModel>(options => 
        options.UseSqlServer(Configuration.GetConnectionString("Demo")));

      services.AddRazorPages().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
      }
      // TODO: Uncomment when Https is desired (not required for this assessment)
      // app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      HttpContextHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapRazorPages();
      });
    }

  }
}
