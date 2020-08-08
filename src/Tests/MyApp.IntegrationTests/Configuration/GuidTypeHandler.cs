using Dapper;
using System;

namespace MyApp.IntegrationTests.Configuration
{
    public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value)
        {
            return new Guid((string)value);
        }

        public override void SetValue(System.Data.IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToString();
        }
    }
}
