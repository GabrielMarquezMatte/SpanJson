using System;
using System.ComponentModel;

namespace SpanJson.Formatters.Dynamic
{
    [TypeConverter(typeof(DynamicTypeConverter))]
    public sealed class SpanJsonDynamicUtf16String(in ReadOnlySpan<char> span) : SpanJsonDynamicString<char>(span)
    {
    }
}