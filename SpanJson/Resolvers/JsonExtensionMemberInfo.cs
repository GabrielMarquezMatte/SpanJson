using System;

namespace SpanJson.Resolvers
{
    public class JsonExtensionMemberInfo(string memberName, Type memberType, NamingConventions namingConvention, bool excludeNulls)
    {
        public string MemberName { get; } = memberName;
        public Type MemberType { get; } = memberType;
        public NamingConventions NamingConvention { get; } = namingConvention;
        public bool ExcludeNulls { get; } = excludeNulls;
    }
}
