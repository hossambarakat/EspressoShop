using System;
using System.Linq;
using System.Net.Http;
using EspressoShop.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;

namespace EspressoShop.Web
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
            services.AddHttpClient();

            services.AddTransient<IProductServiceClient, ProductServiceClient>();
            services.AddTransient<IReviewsServiceClient, ReviewsServiceClient>();
            services.AddHttpClient<IProductServiceClient, ProductServiceClient>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(Configuration.GetValue<string>("ProductCatalogUrl"));
                });
            //.AddPolicyHandler(GetRetryPolicy());

            services.AddHttpClient<IReviewsServiceClient, ReviewsServiceClient>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(Configuration.GetValue<string>("ReviewsUrl"));
                });
            //.AddPolicyHandler(GetRetryPolicy());

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddControllersWithViews();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(1), (r, ts) =>
                 {

                 });
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.Use((context, next) =>
            {
                if (context.Request.Headers.Keys.Contains("x-b3-traceid"))
                {
                    context.Response.Headers["x-b3-traceid"] = context.Request.Headers["x-b3-traceid"].FirstOrDefault();
                    context.Response.Headers["x-request-id"] = context.Request.Headers["x-b3-traceid"].FirstOrDefault();
                }
                return next.Invoke();
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
