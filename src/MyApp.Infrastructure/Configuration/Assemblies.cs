using MyApp.Application.Configuration.Data;
using System.Reflection;

namespace MyApp.Infrastructure.Configuration
{
    public static class Assemblies
    {
        public static readonly Assembly Application = typeof(ISqlConnectionFactory).Assembly;
    }
}
