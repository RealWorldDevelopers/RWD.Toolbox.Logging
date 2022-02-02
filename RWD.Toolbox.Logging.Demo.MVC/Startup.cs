using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RWD.Toolbox.Logging.Demo.Communication;


namespace RWD.Toolbox.Logging.Demo.MVC
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddTransient<ICommunicationAgent, CommunicationAgent>();
         services.AddHttpContextAccessor();

         // Add Performance or Usage Filter Globally
         // services.AddControllersWithViews(options => options.Filters.Add(typeof(TrackActionPerformanceFilter)));
         // services.AddControllersWithViews(options => options.Filters.Add(typeof(TrackActionUsageFilter)));

         services.AddControllersWithViews();

      }

      
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         // TODO uncomment in real use
         //if (env.IsDevelopment())
         //{
         //  app.UseDeveloperExceptionPage();
         //}
         //else
         //{
         app.UseExceptionHandler("/Home/Error");
         app.UseHsts();
         //}
                 
         app.UseHttpsRedirection();
         app.UseStaticFiles();

         app.UseRouting();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
         });

      }

   }

}
