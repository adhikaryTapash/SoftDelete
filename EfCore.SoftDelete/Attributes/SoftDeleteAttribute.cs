using System;

namespace EfCore.SoftDelete.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SoftDeleteAttribute : Attribute
    {
    }
}
