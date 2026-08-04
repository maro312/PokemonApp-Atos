
using Application.Extinsions;
using Application.Options;
using infrastructure.Data.Seeding;
using infrastructure.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using API.Middleware;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Configuration.AddJsonFile("secrets.json", optional: true, reloadOnChange: true);

            builder.Services.AddControllers();
            builder.Services.AddTransient<Seed>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token."
                });
                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
                });
            });

            builder.Services.AddDbContext<PokemonDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(nameof(JwtConfiguration)));

            builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<PokemonDbContext>()
                .AddDefaultTokenProviders();

            var jwtConfig = builder.Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, // For dev only
                    ValidateAudience = false, // For dev only
                    RequireExpirationTime = false, // For dev only
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                jwt.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                        Console.WriteLine($"=== JWT DEBUG: OnMessageReceived ===");
                        Console.WriteLine($"Authorization header: {(authHeader != null ? authHeader[..Math.Min(50, authHeader.Length)] + "..." : "NULL/MISSING")}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine($"=== JWT DEBUG: Token VALIDATED successfully ===");
                        Console.WriteLine($"User: {context.Principal?.Identity?.Name}");
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"=== JWT DEBUG: Authentication FAILED ===");
                        Console.WriteLine($"Exception: {context.Exception}");
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Console.WriteLine($"=== JWT DEBUG: OnChallenge (401 will be returned) ===");
                        Console.WriteLine($"Error: {context.Error}");
                        Console.WriteLine($"ErrorDescription: {context.ErrorDescription}");
                        return Task.CompletedTask;
                    }
                };

            });
            
             
            builder.Services.AddPokemonServices(builder.Configuration);

            var app = builder.Build();
            
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            if (args.Length == 1 && args[0].ToLower() == "seeddata")
                SeedData(app);

            void SeedData(IHost app)
            {
                var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

                using (var scope = scopedFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetService<Seed>();
                    service.SeedDataContext();
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

