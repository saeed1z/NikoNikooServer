using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OnlineServices.Api.Models;
using OnlineServices.Core;
using OnlineServices.Persistence;
using System;
using System.Text;
using OnlineServices.Api.Helpers;
using OnlineServices.Core.Nikoo;

namespace OnlineServices.Api
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
                { options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")); });

            //services.AddDbContext<OnlineServicesDbContext>(options =>
            //    options.UseLazyLoadingProxies().UseSqlServer(
            //        Configuration.GetConnectionString("DB_OnlineServicesConnection"))
            //    );
            services.AddIdentity<IdentityUser, IdentityRole>(
                options =>
                {
                    options.SignIn.RequireConfirmedPhoneNumber = true;
                }).AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<OnlineServicesDbContext>().
               AddTokenProvider("OnlineService", typeof(DataProtectorTokenProvider<IdentityUser>));

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "OnlineServicePolicy",
            //        builder =>
            //        {
            //            builder.WithMethods("PUT", "DELETE", "GET", "POST", "OPTIONS")
            //                   .AllowAnyOrigin()
            //                   .AllowAnyHeader()
            //                   .AllowCredentials()
            //                   .AllowAnyMethod();
            //        });
            //});

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("OnlineServicePolicy",
            //        builder => builder.AllowAnyOrigin()
            //            .AllowAnyHeader()
            //            .AllowAnyMethod()
            //            .AllowCredentials());
            //});


            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddMemoryCache();
            services.AddRazorPages();

            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

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

            #region Container Injection

            services.AddTransient<IPersonService, PersonServices>();
            services.AddTransient<IPersonTypeService, PersonTypeService>();
            services.AddTransient<IStateService, StateService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IBannerService, BannerService>();
            services.AddTransient<IPersonService_Service, PersonService_Service>();
            services.AddTransient<IServiceTypeService, ServiceTypeService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IModelService, ModelService>();
            services.AddTransient<IPackageTemplateService, PackageTemplateService>();
            services.AddTransient<IPackageTemplateDetailService, PackageTemplateDetailService>();
            services.AddTransient<IPersonPackageService, PersonPackageService>();
            services.AddTransient<IPersonPackageDetailService, PersonPackageDetailService>();
            services.AddTransient<IPersonCarService, PersonCarService>();
            services.AddTransient<IServiceRequestService, ServiceRequestService>();
            services.AddTransient<IServiceTypeUnitPriceService, ServiceTypeUnitPriceService>();
            services.AddTransient<IServiceRequestAcceptService, ServiceRequestAcceptService>();
            services.AddTransient<IAspNetUserService, AspNetUserService>();
            services.AddTransient<ITokenManagerService, TokenManagerService>();
            services.AddTransient<IBaseService, BaseService>();
            services.AddTransient<IBaseKindService, BaseKindService>();
            services.AddTransient<IServiceCenterService, ServiceCenterService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IServiceCaptureService, ServiceCaptureService>();
            services.AddTransient<IServiceFactorService, ServiceFactorService>();
            services.AddTransient<IServiceFactorDetailService, ServiceFactorDetailService>();
            services.AddTransient<ISystemSettingService, SystemSettingService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICommercialUserRequestServices, CommercialUserRequestServices>();
            services.AddTransient<ITicketServices, TicketServices>();
            services.AddTransient<ITicketCommentServices, TicketCommentServices>();
            services.AddTransient<INikooPayment, NikooPaymentrRpository>();
            services.AddTransient<SettingMethod, SettingMethod>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
