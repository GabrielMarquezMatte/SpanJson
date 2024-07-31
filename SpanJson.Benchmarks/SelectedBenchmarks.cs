using System.Text;
using BenchmarkDotNet.Attributes;
using SpanJson.Benchmarks.Serializers;
using SpanJson.Shared.Fixture;
using SpanJson.Shared.Models;

namespace SpanJson.Benchmarks
{
    [Config(typeof(MyConfig))]
    [DisassemblyDiagnoser(2)]
    public class SelectedBenchmarks
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new(12345);

        private static readonly SpanJsonSerializer SpanJsonSerializer = new();
        private static readonly Answer Answer = ExpressionTreeFixture.Create<Answer>();

        private static readonly string AnswerSerializedString = SpanJsonSerializer.Serialize(Answer);

        private static readonly byte[] AnswerSerializedByteArray = Encoding.UTF8.GetBytes(AnswerSerializedString);

        [Benchmark]
        public string SerializeAnswerWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(Answer);
        }

        [Benchmark]
        public byte[] SerializeAnswerWithSpanJsonSerializerUtf8()
        {
            return JsonSerializer.Generic.Utf8.Serialize(Answer);
        }

        [Benchmark]
        public Answer DeserializeAnswerWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Answer>(AnswerSerializedString);
        }

        [Benchmark]
        public Answer DeserializeAnswerWithSpanJsonSerializerUtf8()
        {
            return JsonSerializer.Generic.Utf8.Deserialize<Answer>(AnswerSerializedByteArray);
        }
    }
}