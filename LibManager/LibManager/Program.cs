using LibManager.Data;
using LibManager.Repository.AuthorRep;
using LibManager.Repository.BookRep;
using LibManager.Services.AuthorServ;
using LibManager.Services.BookServ;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace LibManager;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //DI
        builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
        builder.Services.AddScoped<IAuthorService, AuthorService>();

        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<IBookService, BookService>();

        //builder.Services.AddAutoMapper(config =>
        //{
        //    config.AddProfile<MappingProfile>();
        //});

        builder.Services.AddDbContext<LibraryContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("LibraryConnection")));


        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });



        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
            context.Database.Migrate();
            SeedData.SeedSomeData(context);
        }


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}