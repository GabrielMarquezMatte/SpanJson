using System;
using System.Collections.Generic;

namespace SpanJson.Formatters.Dynamic
{
    public abstract partial class SpanJsonDynamicNumber<TSymbol>(in ReadOnlySpan<TSymbol> span) : SpanJsonDynamic<TSymbol>(span) where TSymbol : struct
    {
        private static readonly DynamicTypeConverter DynamicConverter = new();

        protected override BaseDynamicTypeConverter<TSymbol> Converter => DynamicConverter;

        public sealed class DynamicTypeConverter : BaseDynamicTypeConverter<TSymbol>
        {
            private static readonly Dictionary<Type, ConvertDelegate> Converters = BuildDelegates();

            public override bool TryConvertTo(Type destinationType, ReadOnlySpan<TSymbol> span, out object value)
            {
                if (Converters.TryGetValue(destinationType, out var del))
                {
                    var reader = new JsonReader<TSymbol>(span);
                    value = del(ref reader);
                    return true;
                }
                value = null;
                return false;
            }

            public override bool IsSupported(Type destinationType)
            {
                var fix = Converters.ContainsKey(destinationType);
                if (!fix)
                {
                    var nullable = Nullable.GetUnderlyingType(destinationType);
                    if (nullable != null)
                    {
                        fix |= IsSupported(nullable);
                    }
                }

                return fix;
            }

            private static Dictionary<Type, ConvertDelegate> BuildDelegates()
            {
                Span<Type> allowedTypes =
                [
                    typeof(sbyte),
                    typeof(short),
                    typeof(int),
                    typeof(long),
                    typeof(byte),
                    typeof(ushort),
                    typeof(uint),
                    typeof(ulong),
                    typeof(float),
                    typeof(double),
                    typeof(decimal)
                ];
                return BuildDelegates(allowedTypes);
            }
        }
    }
}