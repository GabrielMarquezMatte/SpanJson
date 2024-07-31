using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace SpanJson.Formatters.Dynamic
{
    public sealed class SpanJsonDynamicArray<TSymbol> : DynamicObject, ISpanJsonDynamicArray where TSymbol : struct
    {
        private static readonly ConcurrentDictionary<Type, Func<object[], ICountableEnumerable>> Enumerables =
            new();

        private readonly object[] _input;

        internal SpanJsonDynamicArray(object[] input)
        {
            _input = input;
        }

        [IgnoreDataMember]
        public object this[int index] => _input[index];

        [IgnoreDataMember]
        public int Length => _input.Length;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Always works
        /// </summary>
        public IEnumerator<object> GetEnumerator()
        {
            for (var i = 0; i < _input.Length; i++)
            {
                yield return _input[i];
            }
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            var returnType = binder.ReturnType;
            if (returnType.IsArray)
            {
                // ReSharper disable ConvertClosureToMethodGroup
                var functor = Enumerables.GetOrAdd(returnType.GetElementType(), CreateEnumerable);
                // ReSharper restore ConvertClosureToMethodGroup
                var enumerable = functor(_input);
                var array = Array.CreateInstance(returnType.GetElementType(), enumerable.Count);
                var index = 0;
                foreach (var value in enumerable)
                {
                    array.SetValue(value, index++);
                }

                result = array;
                return true;
            }

            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                // ReSharper disable ConvertClosureToMethodGroup
                var enumerable = Enumerables.GetOrAdd(returnType.GetGenericArguments()[0], CreateEnumerable);
                // ReSharper restore ConvertClosureToMethodGroup
                result = enumerable(_input);
                return true;
            }

            result = null;
            return false;
        }

        public override string ToString()
        {
            return $"[{string.Join(',', _input.Select(a => a == null ? "null" : a.ToJsonValue()))}]";
        }

        public string ToJsonValue() => ToString();

        private static Func<object[], ICountableEnumerable> CreateEnumerable(Type type)
        {
            var ctor = typeof(Enumerable<>).MakeGenericType(typeof(TSymbol), type).GetConstructor([typeof(object[])]);
            var paramExpression = Expression.Parameter(typeof(object[]), "input");
            var lambda =
                Expression.Lambda<Func<object[], ICountableEnumerable>>(
                    Expression.Convert(Expression.New(ctor, paramExpression), typeof(ICountableEnumerable)),
                    paramExpression);
            return lambda.Compile();
        }

        private readonly struct Enumerable<TOutput> : IReadOnlyCollection<TOutput>, ICountableEnumerable
        {
            private readonly object[] _input;

            public IEnumerator<TOutput> GetEnumerator()
            {
                if (typeof(TOutput) == typeof(bool) || typeof(TOutput) == typeof(bool?))
                {
                    return BoolEnumerator();
                }

                return EnumeratorFactory.Create<TOutput>(_input);
            }

            private IEnumerator<TOutput> BoolEnumerator()
            {
                for (var i = 0; i < _input.Length; i++)
                {
                    yield return (TOutput) _input[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public int Count { get; }
        }

        private struct Enumerator<TConverter, TOutput>(TConverter converter, object[] input) : IEnumerator<TOutput> where TConverter : TypeConverter
        {
            private readonly int _length = input.Length;
            private int _index = 0;

            public bool MoveNext()
            {
                if (_index >= _length)
                {
                    return false;
                }

                var value = input[_index++];
                Current = (TOutput) converter.ConvertTo(value, typeof(TOutput));
                return true;
            }

            public void Reset()
            {
                _index = 0;
            }

            public TOutput Current { get; private set; } = default;

            readonly object IEnumerator.Current => Current;

            public readonly void Dispose()
            {
            }
        }

        private static class EnumeratorFactory
        {
            private static readonly SpanJsonDynamicNumber<TSymbol>.DynamicTypeConverter NumberTypeConverter =
                new();

            private static readonly SpanJsonDynamicString<TSymbol>.DynamicTypeConverter StringTypeConverter =
                new();

            public static IEnumerator<TOutput> Create<TOutput>(object[] input)
            {
                var type = typeof(TOutput);
                if (StringTypeConverter.IsSupported(type))
                {
                    return new Enumerator<SpanJsonDynamicString<TSymbol>.DynamicTypeConverter, TOutput>(StringTypeConverter, input);
                }

                if (NumberTypeConverter.IsSupported(type))
                {
                    return new Enumerator<SpanJsonDynamicNumber<TSymbol>.DynamicTypeConverter, TOutput>(NumberTypeConverter, input);
                }

                return null;
            }
        }

        private interface ICountableEnumerable : IEnumerable
        {
            int Count { get; }
        }
    }
}