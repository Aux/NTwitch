using System;
using System.Linq;
using System.Reflection;

namespace NTwitch
{
    internal static class ReflectionHelper
    {
        public static T CreateInstance<T>(params object[] parameters)
        {
            var constructor = typeof(T).GetTypeInfo().DeclaredConstructors.First();
            var obj = constructor.Invoke(parameters);
            return (T)obj;
        }

        public static object CreateInstance(Type type, params object[] parameters)
        {
            var constructor = type.GetTypeInfo().DeclaredConstructors.First();
            var obj = constructor.Invoke(parameters);
            return Convert.ChangeType(obj, type);
        }
    }
}
