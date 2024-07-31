using System;
using System.Buffers;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    /// <summary>
    /// Used for types which are not built-in
    /// </summary>
    public sealed class ArrayFormatter<T, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<T[], TSymbol>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        public static readonly ArrayFormatter<T, TSymbol, TResolver> Default = new();

        private static readonly IJsonFormatter<T, TSymbol> ElementFormatter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();

        private static readonly bool IsRecursionCandidate = RecursionCandidate<T>.IsRecursionCandidate;

        public T[] Deserialize(ref JsonReader<TSymbol> reader)
        {
            T[] temp = null;
            T[] result;
            try
            {
                temp = ArrayPool<T>.Shared.Rent(4);
                if (reader.ReadIsNull())
                {
                    return [];
                }
                reader.ReadBeginArrayOrThrow();
                var count = 0;
                while (!reader.TryReadIsEndArrayOrValueSeparator(ref count)) // count is already preincremented, as it counts the separators
                {
                    if (count == temp.Length)
                    {
                        FormatterUtils.GrowArray(ref temp);
                    }

                    temp[count - 1] = ElementFormatter.Deserialize(ref reader);
                }

                result = count == 0 ? [] : FormatterUtils.CopyArray(temp, count);
            }
            finally
            {
                if (temp != null)
                {
                    ArrayPool<T>.Shared.Return(temp);
                }
            }

            return result;
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, T[] value)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            if (IsRecursionCandidate)
            {
                writer.IncrementDepth();
            }
            var valueLength = value.Length;
            writer.WriteBeginArray();
            if (valueLength > 0)
            {
                SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[0], ElementFormatter);
#pragma warning disable HLQ013 // Consider using 'foreach' instead of 'for' for iterating over arrays or spans
                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[i], ElementFormatter);
                }
#pragma warning restore HLQ013 // Consider using 'foreach' instead of 'for' for iterating over arrays or spans
            }
            if (IsRecursionCandidate)
            {
                writer.DecrementDepth();
            }
            writer.WriteEndArray();
        }
    }
}