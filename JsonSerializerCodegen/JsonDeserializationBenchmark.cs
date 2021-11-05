using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace JsonSerializerCodegen
{
    [MemoryDiagnoser]
    public class JsonDeserializationBenchmarks
    {
        private static string _serialized;
        private static MemoryStream _serializedStream;

        [GlobalSetup]
        public static void Setup()
        {
            var people = Enumerable.Range(0, 1000).Select(i => new Person
            {
                FirstName = i.ToString(),
                LastName = i.ToString(),
            }).ToList();
            _serialized = JsonSerializer.Serialize(people, PersonSerializerContext.Default.IEnumerablePerson);
            _serializedStream = new MemoryStream(Encoding.UTF8.GetBytes(_serialized));
        }

        [IterationSetup]
        public static void ResetStream()
        {
            _serializedStream.Seek(0, SeekOrigin.Begin);
        }

        [Benchmark]
        [BenchmarkCategory("string")]
        public void DeserializeStringWithSerializationContext()
        {
            var list = JsonSerializer.Deserialize(_serialized, PersonSerializerContext.Default.IEnumerablePerson);
        }

        [Benchmark]
        [BenchmarkCategory("stream")]
        public void DeserializeStreamWithSerializationContext()
        {
            var list = JsonSerializer.Deserialize(_serializedStream, PersonSerializerContext.Default.IEnumerablePerson);
        }

        [Benchmark(Baseline = true)]
        [BenchmarkCategory("string")]

        public void DeserializeStringWithReflection()
        {
            var list = JsonSerializer.Deserialize<IEnumerable<Person>>(_serialized);
        }

        [Benchmark]
        [BenchmarkCategory("stream")]
        public void DeserializeStreamWithReflection()
        {
            var list = JsonSerializer.Deserialize<IEnumerable<Person>>(_serializedStream);
        }
    }
}
