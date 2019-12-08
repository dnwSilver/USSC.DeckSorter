using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RiskFirst.Hateoas;
using USSC.DeckSorter.BusinessLogic;
using USSC.DeckSorter.Controllers;
using USSC.DeckSorter.Responses;

namespace USSC.DeckSorter
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
            services.AddControllers();
            services.AddLinks(config =>
            {
                config.AddPolicy<DeckResponse>(policy =>
                {
                    policy.RequireRoutedLink("self", nameof(DeckController.GetDeck), x => new {id = x.Id})
                        .RequireRoutedLink("shuffle", nameof(DeckController.ShuffleDeck), x => new {id = x.Id})
                        .RequireRoutedLink("delete", nameof(DeckController.DeleteDeck), x => new {id = x.Id})
                        .RequireRoutedLink("all", nameof(DeckController.GetAllDecks));
                });
            });

            services.Configure<DeckSettings>(Configuration);
            services.AddSingleton<DeckMapper>();
            services.AddTransient<IDeckService, DeckService>();
            services.AddTransient<IDeckRepository, DeckRepository>();
            services.AddTransient<IShuffleAlgorithm, RandomSnuffle>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}