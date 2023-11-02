using JwtMiddleware;
string myPolicyCors = "OndissJWT";

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(opt =>
        opt.AddPolicy(name: myPolicyCors, policy =>
        {
            policy
            .AllowAnyOrigin()
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();

        }));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MyJwtService>();
builder.Services.AddTokenAuthentication(builder.Configuration, myPolicyCors);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors(myPolicyCors);

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers().RequireCors(myPolicyCors);

app.Run();


