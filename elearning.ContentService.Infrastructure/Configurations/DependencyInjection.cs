using elearning.ContentService.Domain.Questions.Repositories;
using elearning.ContentService.Infrastructure.Persistence;
using elearning.ContentService.Infrastructure.Persistence.Repositories.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace elearning.ContentService.Infrastructure.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ContentDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IQuestionRepository, QuestionRepository>();

            return services;
        }
    }
}
