using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineServices.Core;
//using OnlineServices.Core.Implementation;
//using OnlineServices.Core.Interfaces;
using OnlineServices.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineServices
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
            services.AddDbContext<OnlineServicesDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(
                    Configuration.GetConnectionString("DB_OnlineServicesConnection")));
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<DB_OnlineServices>();
            services.AddIdentity<IdentityUser, IdentityRole>(
                options => options.SignIn.RequireConfirmedPhoneNumber = true
                ).AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<OnlineServicesDbContext>().
               AddDefaultTokenProviders();
            services.AddControllersWithViews();
            services.AddMemoryCache();
            services.AddRazorPages();
            services.Configure<IdentityOptions>(x =>
            {
                x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                x.Lockout.MaxFailedAccessAttempts = 5;
                x.Password.RequireDigit = true;
                x.Password.RequireLowercase = false;
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequiredLength = 6;
            });

            services.AddAuthentication();
            services.AddAuthorization(x =>
            {
                x.AddPolicy("adminpolicy", x => x.RequireRole("مدیریت"));
                x.AddPolicy("operatorpolicy", x => x.RequireRole("اپراتور"));  //and
            });
            services.ConfigureExternalCookie(x =>
            {
                x.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = y =>
                    {
                        y.Response.Redirect("/Account/Login");
                        return Task.CompletedTask;
                    }
                };
            });
            #region Container Injection
            services.AddTransient<IPersonService, PersonServices>();
            services.AddTransient<IPersonTypeService, PersonTypeService>();
            services.AddTransient<IStateService, StateService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IPersonService_Service, PersonService_Service>();
            services.AddTransient<IServiceTypeService, ServiceTypeService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IModelService, ModelService>();
            services.AddTransient<IBannerService, BannerService>();
            services.AddTransient<IPackageTemplateService, PackageTemplateService>();
            services.AddTransient<IPackageTemplateDetailService, PackageTemplateDetailService>();
            services.AddTransient<IPersonPackageService, PersonPackageService>();
            services.AddTransient<IPersonPackageDetailService, PersonPackageDetailService>();
            services.AddTransient<IPersonCarService, PersonCarService>();
            services.AddTransient<IServiceRequestService, ServiceRequestService>();
            services.AddTransient<IServiceTypeUnitPriceService, ServiceTypeUnitPriceService>();
            services.AddTransient<IServiceRequestAcceptService, ServiceRequestAcceptService>();
            services.AddTransient<IBaseService, BaseService>();
            services.AddTransient<IServiceCenterService, ServiceCenterService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IServiceCaptureService, ServiceCaptureService>();
            services.AddTransient<ISystemSettingService, SystemSettingService>();
            services.AddTransient<ICommercialUserRequestServices, CommercialUserRequestServices>();
            services.AddTransient<ITicketServices, TicketServices>();
            services.AddTransient<ITicketCommentServices, TicketCommentServices>();
            services.AddTransient<IAspNetUserService, AspNetUserService>();
            services.AddTransient<ITokenManagerService, TokenManagerService>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
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

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
            //InitIdentity(userManager, roleManager).Wait();
        }
        private async Task InitIdentity(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            List<string> roles = new List<string>
            {
            "مدیریت","اپراتور","کارشناس یا کارمند","راننده","کاربر","کاربر تجاری",
            };
            foreach (var item in roles)
            {
                if (await roleManager.RoleExistsAsync(item) == false)
                {
                    IdentityRole role = new IdentityRole(item);
                    await roleManager.CreateAsync(role);
                }
            }
            var useradmin = await userManager.FindByNameAsync("Admin@gmail.com");
            if (useradmin == null)
            {
                useradmin = new IdentityUser()
                {
                    PhoneNumber="09000000000",
                    PhoneNumberConfirmed=true,
                    Email = "admin@admin.com",
                    UserName = "admin",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(useradmin, "102030");
                await userManager.AddToRoleAsync(useradmin, "مدیریت");
            }
        }

    }
}
