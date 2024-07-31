using System;
using System.ComponentModel;

namespace SpanJson.Formatters.Dynamic
{
    [TypeConverter(typeof(DynamicTypeConverter))]
    public sealed class SpanJsonDynamicUtf8String(in ReadOnlySpan<byte> span) : SpanJsonDynamicString<byte>(span)
    {
    }
}