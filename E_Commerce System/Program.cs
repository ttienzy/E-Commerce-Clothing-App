using Application.BLL.Contracts;
using Application.BLL.Contracts.IAuthenticationServices;
using Application.BLL.Contracts.TokenService;
using Application.BLL.Helper;
using Application.BLL.PaymentServices.PayContracts;
using Application.BLL.PaymentServices.PayServices;
using Application.BLL.Services;
using Application.BLL.Services.AssociationToken;
using Application.BLL.Services.AuthenServices;
using Application.DAL.DbContextData;
using Application.DAL.Domain.Contracts;
using Application.DAL.Domain.Models;
using Application.DAL.Domain.Repositories;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddDbContext<dbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:Database"],m => m.MigrationsAssembly("E_Commerce System")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

    options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
    options.User.RequireUniqueEmail = false;

})
    .AddEntityFrameworkStores<dbContext>()
    .AddDefaultTokenProviders();

Cloudinary cloudinary = new Cloudinary(builder.Configuration["Cloudinary:Url"]);
cloudinary.Api.Secure = true;

builder.Services.AddSingleton<ICloudinary>(cloudinary);
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITradingProductSupplierInfo, TradingProductSupplierInfo>();
builder.Services.AddScoped<IDiscountServices, DiscountService>();
builder.Services.AddScoped<IInventoryServices, InventoryServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddScoped<IAuthentication, AuthenticationServices>();
builder.Services.AddScoped<IRoleServices, RoleServices>();
builder.Services.AddScoped<IAuthentication, AuthenticationServices>();
builder.Services.AddScoped<IAddressUserServices, AddressUserServices>();
builder.Services.AddScoped<IOrderInvoiceServices, OrderInvoiceServices>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IPaymentServices, PaymentServices>();
builder.Services.AddScoped<IPageManagers, PageManagers>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") 
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();

    });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultScheme =
    options.DefaultForbidScheme =
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:secret_key"] ?? "Tkjsrhiwe@+091nkjfbewkwehrkqhoiw%$&%_"))
    };
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
