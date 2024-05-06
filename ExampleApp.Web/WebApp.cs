using ExampleApp.Web.Persistence;

namespace ExampleApp.Web;

public class WebApp
{
    private readonly WebApplication app;

    public WebApp()
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
        {
            WebRootPath = "Frontend/build"
        });

        builder.Services
            .AddControllers()
            .AddApplicationPart(typeof(WebApp).Assembly)
            .AddControllersAsServices();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<TodoContext>();

        app = builder.Build();

        app.UseCors(x =>
        {
            x.AllowAnyHeader();
            x.AllowAnyMethod();
            x.AllowAnyOrigin();
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseDefaultFiles();
        app.UseStaticFiles(new StaticFileOptions()
        {
            RequestPath = "",
        });
        app.UseAuthorization();
        app.MapControllers();
    }

    public void Run(int port) => app.Run($"http://localhost:{port}/");
}
