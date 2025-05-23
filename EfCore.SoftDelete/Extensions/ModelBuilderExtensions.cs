using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using EfCore.SoftDelete.Attributes;

namespace EfCore.SoftDelete.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplySoftDeleteFilters(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;

                var softDeleteProp = clrType
                    .GetProperties()
                    .FirstOrDefault(p => p.IsDefined(typeof(SoftDeleteAttribute), true)
                                         && p.PropertyType == typeof(bool));

                if (softDeleteProp == null)
                    continue;

                var parameter = Expression.Parameter(clrType, "e");
                var propertyAccess = Expression.Property(parameter, softDeleteProp);
                var isNotDeleted = Expression.Equal(propertyAccess, Expression.Constant(false));
                var lambda = Expression.Lambda(isNotDeleted, parameter);

                entityType.SetQueryFilter(lambda);
            }
        }
    }
}
