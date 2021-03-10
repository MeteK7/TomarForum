using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TomarForumBLL;
using TomarForumBLL.Interfaces;
using TomarForumData;
using TomarForumData.EntityModels;
using TomarForumService;
using TomarForumService.Interfaces;

namespace TomarForumUI
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped<IAdminBLL, AdminBLL>();
            services.AddScoped<IHomeBLL, HomeBLL>();
            services.AddScoped<IForumBLL, ForumBLL>();
            services.AddScoped<IPostBLL, PostBLL>();
            services.AddScoped<IProfileBLL, ProfileBLL>();
            services.AddScoped<IForumService, ForumService>();
            services.AddScoped<IForumUserService, ForumUserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();

            services.AddTransient<DataSeeder>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeeder dataSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            dataSeeder.SeedSuperUser();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
