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

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services
            .AddControllers()
            .AddApplicationPart(typeof(WebApp).Assembly)
            .AddControllersAsServices();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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
