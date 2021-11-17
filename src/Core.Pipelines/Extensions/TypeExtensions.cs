using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Pipelines.Extensions
{
    public static class TypeExtensions
    {
        public static bool HasType(this Type obj, Type obj2)
        {
            Type[] interfaces = obj.GetInterfaces();
            HashSet<Type> result = new HashSet<Type>(interfaces);
            foreach (Type i in interfaces) result.ExceptWith(i.GetInterfaces());
            bool hasType = result.Any(p => p == obj2);
            return hasType;
        }
    }
}
