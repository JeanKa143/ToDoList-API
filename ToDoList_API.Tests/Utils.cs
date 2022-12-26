using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using ToDoList_BAL.Configurations;

namespace ToDoList_API.Tests
{
    internal static class Utils
    {
        public static IMapper GetMapper()
        {
            var autoMapperConfiguration = new AutoMapperConfiguration();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(autoMapperConfiguration));

            return new Mapper(configuration);
        }

        public static IConfiguration GetConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("17d929b5-b23f-4367-9d8d-9bd882a3e8cd")
                .Build();

            return configuration;
        }

        public static bool ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);

            return Validator.TryValidateObject(model, ctx, validationResults, true);
        }
    }
}
