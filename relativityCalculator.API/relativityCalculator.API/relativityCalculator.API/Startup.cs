using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using relativityCalculator.Core.Contracts;
using relativityCalculator.Infrastructure.Repository;
using relativityCalculator.Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;

namespace relativityCalculator.API
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
		.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
		.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
		.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc()
			.AddXmlSerializerFormatters();
			services.Configure<AppSettings>(Configuration.GetSection("Connections"));
			services.AddDbContext<RelativitiesContext>();
			services.AddTransient<IDbHandler, DbHandler>();
			services.AddTransient<ICalculatorService, Core.Services.CalculatorService>();
			services.AddTransient<IAssessorRepository, AssessorRepository>();
			services.AddTransient<IRelativityRepository, RelativityRepository>();
			services.AddTransient<IAreaRepository, AreaRepository>();
			services.AddTransient<IRelativityConfig, RelativityConfigRepository>();
			services.AddTransient<IAppSettings, AppSettings>();

			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials());
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Version = "v1",
					Title = "Relativity API",
					Description = "Relativity Service API",
					TermsOfService = "None",
					Contact = new Contact
					{
						Name = "SBIBGroupIT",
						Email = "SBISGROUPIT@standardbank.co.za",
						Url = "www.standardbank.co.za"
					},
					License = new License
					{
						Name = "Use under LICX",
						Url = "www.standardbank.co.za"
					}
				});
				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseCors("CorsPolicy");
			var value = Configuration["ApplicationSettings"];
			if (env.IsDevelopment()) app.UseDeveloperExceptionPage();


			app.UseSwagger();
			app.UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", "RelativityCalculator.API"); });

			app.UseMvc();
		}
	}
}
