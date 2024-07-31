using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

namespace SpanJson.Formatters.Dynamic
{
    public abstract class SpanJsonDynamic<TSymbol>(in ReadOnlySpan<TSymbol> span) : DynamicObject, ISpanJsonDynamicValue<TSymbol> where TSymbol : struct
    {
        [IgnoreDataMember]
        public TSymbol[] Symbols { get; } = span.ToArray();

        protected abstract BaseDynamicTypeConverter<TSymbol> Converter { get; }

        public bool TryConvert(Type outputType, out object result)
        {
            return Converter.TryConvertTo(outputType, Symbols, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                var chars = Unsafe.As<char[]>(Symbols);
                return new string(chars);
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                var bytes = Unsafe.As<byte[]>(Symbols);
                return Encoding.UTF8.GetString(bytes);
            }

            throw new NotSupportedException();
        }

        public virtual string ToJsonValue() => ToString();

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            return TryConvert(binder.ReturnType, out result);
        }
    }
}