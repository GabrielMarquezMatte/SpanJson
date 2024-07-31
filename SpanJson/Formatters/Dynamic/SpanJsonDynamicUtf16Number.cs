using System;
using System.ComponentModel;

namespace SpanJson.Formatters.Dynamic
{
    [TypeConverter(typeof(DynamicTypeConverter))]
    public sealed class SpanJsonDynamicUtf16Number(in ReadOnlySpan<char> span) : SpanJsonDynamicNumber<char>(span)
    {
    }
}