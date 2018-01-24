using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NTwitch.Helix.Net.Converters
{
    internal class TwitchContractResolver : DefaultContractResolver
    {
        private static readonly TypeInfo _ienumerable = typeof(IEnumerable<ulong[]>).GetTypeInfo();
        private static readonly MethodInfo _shouldSerialize = typeof(TwitchContractResolver).GetTypeInfo().GetDeclaredMethod("ShouldSerialize");

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (property.Ignored)
                return property;

            if (member is PropertyInfo propInfo)
            {
                var converter = GetConverter(property, propInfo, propInfo.PropertyType, 0);
                if (converter != null)
                {
                    property.Converter = converter;
                    property.MemberConverter = converter;
                }
            }
            else
                throw new InvalidOperationException($"{member.DeclaringType.FullName}.{member.Name} is not a property.");
            return property;
        }

        private static JsonConverter GetConverter(JsonProperty property, PropertyInfo propInfo, Type type, int depth)
        {
            if (type.IsArray)
                return MakeGenericConverter(property, propInfo, typeof(ArrayConverter<>), type.GetElementType(), depth);
            if (type.IsConstructedGenericType)
            {
                Type genericType = type.GetGenericTypeDefinition();
                if (depth == 0 && genericType == typeof(Optional<>))
                {
                    var typeInput = propInfo.DeclaringType;
                    var innerTypeOutput = type.GenericTypeArguments[0];

                    var getter = typeof(Func<,>).MakeGenericType(typeInput, type);
                    var getterDelegate = propInfo.GetMethod.CreateDelegate(getter);
                    var shouldSerialize = _shouldSerialize.MakeGenericMethod(typeInput, innerTypeOutput);
                    var shouldSerializeDelegate = (Func<object, Delegate, bool>)shouldSerialize.CreateDelegate(typeof(Func<object, Delegate, bool>));
                    property.ShouldSerialize = x => shouldSerializeDelegate(x, getterDelegate);

                    return MakeGenericConverter(property, propInfo, typeof(OptionalConverter<>), innerTypeOutput, depth);
                }
                else if (genericType == typeof(Nullable<>))
                    return MakeGenericConverter(property, propInfo, typeof(NullableConverter<>), type.GenericTypeArguments[0], depth);
            }
            
            //Entities
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.ImplementedInterfaces.Any(x => x == typeof(IEntity<ulong>)))
                return UInt64EntityConverter.Instance;
            if (typeInfo.ImplementedInterfaces.Any(x => x == typeof(IEntity<string>)))
                return StringEntityConverter.Instance;

            return null;
        }

        private static bool ShouldSerialize<TOwner, TValue>(object owner, Delegate getter)
        {
            return (getter as Func<TOwner, Optional<TValue>>)((TOwner)owner).IsSpecified;
        }

        private static JsonConverter MakeGenericConverter(JsonProperty property, PropertyInfo propInfo, Type converterType, Type innerType, int depth)
        {
            var genericType = converterType.MakeGenericType(innerType).GetTypeInfo();
            var innerConverter = GetConverter(property, propInfo, innerType, depth + 1);
            return genericType.DeclaredConstructors.First().Invoke(new object[] { innerConverter }) as JsonConverter;
        }
    }
}
