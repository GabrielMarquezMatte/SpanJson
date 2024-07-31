using System;
using System.Reflection;

namespace SpanJson.Resolvers
{
    public class JsonMemberInfo(string memberName, Type memberType, MethodInfo shouldSerialize, string name, bool excludeNull, bool canRead, bool canWrite,
        Type customSerializer, object customSerializerArguments)
    {
        public string MemberName { get; } = memberName;
        public Type MemberType { get; } = memberType;
        public MethodInfo ShouldSerialize { get; } = shouldSerialize;
        public string Name { get; } = name;
        public bool ExcludeNull { get; } = excludeNull;

        public Type CustomSerializer { get; } = customSerializer;
        public object CustomSerializerArguments { get; } = customSerializerArguments;

        public bool CanRead { get; } = canRead;
        public bool CanWrite { get; set; } = canWrite;
    }
}