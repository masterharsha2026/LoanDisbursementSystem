
using LoanDisbursementSystem.Data;
using LoanDisbursementSystem.Helpers;
using LoanDisbursementSystem.Models;
using LoanDisbursementSystem.Repositories;
using LoanDisbursementSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LoanDisbursementSystem
{
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

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"));
            });

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
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("UiCorsPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "http://localhost:4300", "http://localhost:4301")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });


            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ILoanRepository, LoanRepository>();
            builder.Services.AddScoped<IDisbursementRepository, DisbursementRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ILoanService, LoanService>();
            builder.Services.AddScoped<IDisbursementService, DisbursementService>();
            builder.Services.AddScoped<JwtTokenGenerator>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            SeedSampleData(app);

            app.UseCors("UiCorsPolicy");

            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void SeedSampleData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var seedCustomers = new List<Customer>
            {
                new() { CustomerName = "John Carter", Mobile = "9000000001", Email = "john.carter@example.com" },
                new() { CustomerName = "Emma Stone", Mobile = "9000000002", Email = "emma.stone@example.com" },
                new() { CustomerName = "Michael Lee", Mobile = "9000000003", Email = "michael.lee@example.com" }
            };

            foreach (var customer in seedCustomers)
            {
                var exists = dbContext.Customers.Any(c => c.Mobile == customer.Mobile);
                if (!exists)
                {
                    dbContext.Customers.Add(customer);
                }
            }
            dbContext.SaveChanges();

            var customers = dbContext.Customers
                .OrderBy(c => c.Id)
                .Take(3)
                .ToList();

            if (customers.Count >= 3)
            {
                if (!dbContext.Loans.Any(l => l.CustomerId == customers[0].Id && l.LoanAmount == 50000m))
                {
                    dbContext.Loans.Add(new Loan { CustomerId = customers[0].Id, LoanAmount = 50000m, status = "Approved" });
                }

                if (!dbContext.Loans.Any(l => l.CustomerId == customers[1].Id && l.LoanAmount == 75000m))
                {
                    dbContext.Loans.Add(new Loan { CustomerId = customers[1].Id, LoanAmount = 75000m, status = "Approved" });
                }

                if (!dbContext.Loans.Any(l => l.CustomerId == customers[2].Id && l.LoanAmount == 30000m))
                {
                    dbContext.Loans.Add(new Loan { CustomerId = customers[2].Id, LoanAmount = 30000m, status = "Pending" });
                }

                dbContext.SaveChanges();
            }

            var approvedLoans = dbContext.Loans
                .Where(l => l.status == "Approved" || l.status == "Disbursed")
                .OrderBy(l => l.Id)
                .Take(2)
                .ToList();

            if (approvedLoans.Count > 0)
            {
                if (!dbContext.Disbursements.Any(d => d.LoanId == approvedLoans[0].Id))
                {
                    dbContext.Disbursements.Add(new Disbursement
                    {
                        LoanId = approvedLoans[0].Id,
                        Amount = 20000m,
                        Remarks = "Initial disbursement",
                        DisbursementDate = DateTime.UtcNow
                    });

                    approvedLoans[0].status = "Disbursed";
                }

                if (approvedLoans.Count > 1 && !dbContext.Disbursements.Any(d => d.LoanId == approvedLoans[1].Id))
                {
                    dbContext.Disbursements.Add(new Disbursement
                    {
                        LoanId = approvedLoans[1].Id,
                        Amount = 30000m,
                        Remarks = "First tranche",
                        DisbursementDate = DateTime.UtcNow
                    });

                    approvedLoans[1].status = "Disbursed";
                }

                dbContext.SaveChanges();
            }
        }
    }
}
