using System;
using Xunit;

namespace SpanJson.Tests
{
    public class NestedClass
    {
        public int InnerValue { get; set; }
    }

    public class NoDefaultConstructorClass(int value, int renamedValue, NestedClass nested)
    {
        public int Value { get; set; } = value;
        public int OtherNamedValue { get; set; } = renamedValue;
        public NestedClass Nested { get; set; } = nested;
    }

    public struct NoDefaultConstructorStruct(int value, int renamedValue, NestedClass nested)
    {
        public int Value { get; set; } = value;
        public int OtherNamedValue { get; set; } = renamedValue;
        public NestedClass Nested { get; set; } = nested;
    }

    public class OptionalDeserializer
    {
        private const string SourceJson = "{\"Value\":42,\"RenamedValue\":1337,\"Nested\":{\"InnerValue\":9001}}";
        private const string ConstructedAsJson = "{\"Value\":42,\"OtherNamedValue\":1337,\"Nested\":{\"InnerValue\":9001}}";

        [Fact]
        public void ShouldBeAbleToSerializeClassWithNoDefaultConstructor() {
            var nested = new NestedClass { InnerValue = 9001 };
            var instance = new NoDefaultConstructorClass(42, 1337, nested);
            var actual = JsonSerializer.Generic.Utf16.Serialize(instance);
            var expected = ConstructedAsJson;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldNotBeAbleToDeserializeClassWithNoDefaultConstructor() {
            Assert.Throws<InvalidOperationException>(() => JsonSerializer.Generic.Utf16.Deserialize<NoDefaultConstructorClass>(SourceJson));
        }

        // Unlike classes, structs always have a "default constructor", even if you provide your own constructor.
        [Fact]
        public void ShouldBeAbleToSerializeStructWithNoDefaultConstructor()
        {
            var nested = new NestedClass { InnerValue = 9001 };
            var instance = new NoDefaultConstructorStruct(42, 1337, nested);
            var actual = JsonSerializer.Generic.Utf16.Serialize(instance);
            var expected = ConstructedAsJson;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeAbleToDeserializeStructWithNoDefaultConstructor()
        {
            var instance = JsonSerializer.Generic.Utf16.Deserialize<NoDefaultConstructorStruct>(SourceJson);
            Assert.Equal(42, instance.Value);
            // Prove that the parameter-having constructor was not called.
            Assert.Equal(0, instance.OtherNamedValue);
            Assert.Equal(9001, instance.Nested.InnerValue);
        }
    }
}