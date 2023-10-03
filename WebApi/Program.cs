using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// at the beginning list of Best Stories IDs is downloaded and stored in the APP;
// any error causes that the app can't run
try { 
    HackerNewsClient.InitBestStoriesIDs();
}
catch (Exception e)
{
    Console.WriteLine("Can't load Best Stories IDs: " + e.Message);
    app.StopAsync();
}
// once IDs are ready, server can be started
app.Run();
