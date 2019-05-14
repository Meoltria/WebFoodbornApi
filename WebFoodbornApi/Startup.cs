using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using WebFoodbornApi.AutoMapper;
using WebFoodbornApi.Common;
using WebFoodbornApi.Data;

namespace WebFoodbornApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private JWTTokenOptions tokenOptions;
        private OppointmentApiOptions apiOptions;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(option => option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss");

            //添加ApiContext的注入
            services.AddDbContext<ApiContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ApiConnection"), b => b.UseRowNumberForPaging()));

            //添加AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguraion());
            });
            services.AddSingleton<IMapper>(mapper => mapperConfig.CreateMapper());

            //配置Swagger生成帮助文档
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "WebFoodbornApi接口文档",
                    Description = "RESTful API for WebFoodbornApi",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "益康软件", Email = "czbok@163.com", Url = "http://www.yygl.com" }
                });

                options.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
                    "WebFoodbornApi.XML")); // 注意：此处替换成所生成的XML documentation的文件名。
                options.DescribeAllEnumsAsStrings();

                options.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
            });

            //配置跨域
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-TotalCount", "X-TotalPage")
                    .AllowCredentials());
            });

            //JWT相关
            tokenOptions = new JWTTokenOptions()
            {
                Issuer = "WebFoodbornApi", // 签发者名称
                Audience = "WebFoodbornApi",//使用者名称
                Expiration = TimeSpan.FromDays(3),//指定Token过期时间
                SecretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("Jwt")["SecretKey"])),
            };
            services.AddSingleton<JWTTokenOptions>(tokenOptions);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = tokenOptions.SecretKey,
                ValidateAudience = true,
                ValidAudience = tokenOptions.Audience, // 设置接收者必须是 WebOppointmentApi
                ValidateIssuer = true,
                ValidIssuer = tokenOptions.Issuer, // 设置签发者必须是 WebOppointmentApi
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            //添加JWT身份验证
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.IncludeErrorDetails = true;
                o.TokenValidationParameters = tokenValidationParameters;
                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    }
                };
            });

            //鉴权规则设置

            //第三方Api调用相关
            apiOptions = new OppointmentApiOptions()
            {
                HospitalId = Configuration.GetSection("FoodbornApi")["HospitalId"].ToString(),
                HospitalName = Configuration.GetSection("FoodbornApi")["HospitalName"].ToString(),
                Version = Configuration.GetSection("FoodbornApi")["Version"].ToString(),
                SecretKey = Configuration.GetSection("FoodbornApi")["SecretKey"].ToString(),
            };
            services.AddSingleton<OppointmentApiOptions>(apiOptions);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApiContext context)
        {
#pragma warning disable CS0618 // 类型或成员已过时
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
#pragma warning restore CS0618 // 类型或成员已过时

            //使用跨域
            app.UseCors("CorsPolicy");

            //注入Swagger生成API文档,此方法需要写在app.UseMvc()方法前
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebFoodbornApi v1");
            });

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
