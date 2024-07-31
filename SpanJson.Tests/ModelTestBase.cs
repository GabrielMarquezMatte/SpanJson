using System.Collections.Generic;
using System.Linq;
using SpanJson.Shared.Models;

namespace SpanJson.Tests
{
    public abstract class ModelTestBase
    {
        public static IEnumerable<object[]> GetModels()
        {
            var models = typeof(AccessToken).Assembly
                .GetTypes()
                .Where(t => string.Equals(t.Namespace, typeof(AccessToken).Namespace, System.StringComparison.Ordinal) && !t.IsEnum && !t.IsInterface &&
                            !t.IsAbstract)
                .ToList();
            return models.Where(a => a != null).Select(a => new object[] {a});
        }
    }
}