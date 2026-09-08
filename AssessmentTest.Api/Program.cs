using System.Text;
using AssessmentTest.Api;
using AssessmentTest.Api.Middleware;
using AssessmentTest.Api.OpenApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
    options.AddOperationTransformer<BearerSecurityRequirementTransformer>();
});

builder.Services.AddApiDI(builder.Configuration);

var jwt = builder.Configuration.GetSection("Jwt");
var signingKey = jwt["Key"] ?? throw new InvalidOperationException(
    "Jwt:Key is not configured. Set it with: dotnet user-secrets set \"Jwt:Key\" \"<32+ chars>\"");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),

            // Default is 5 minutes, which silently extends every token's life.
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "AssessmentTest API v1");
    });
}

app.UseHttpsRedirection();

// Order matters: authentication establishes who the caller is, authorization
// then decides what they may do. Reversed, every [Authorize] returns 401.
app.UseAuthentication();

// After UseAuthentication so HttpContext.User is populated, before the endpoint
// so controllers and services see a filled ICurrentUser.
app.UseMiddleware<CurrentUserMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
