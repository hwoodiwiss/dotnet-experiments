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
    public class JsonSerializationBenchmarks
    {
        private static List<Person> _people;
        private static MemoryStream _stream;

        [GlobalSetup]
        public static void Setup()
        {
            _people = Enumerable.Range(0, 1000).Select(i => new Person
            {
                FirstName = i.ToString(),
                LastName = i.ToString(),
            }).ToList();
            _stream = new MemoryStream();
        }

        [IterationSetup]
        public static void ResetStream()
        {
            _stream = new MemoryStream();
        }


        [Benchmark(Baseline = true)]
        public void SerializeWithReflection()
        {
            string json = JsonSerializer.Serialize(_people);
        }

        [Benchmark]
        [BenchmarkCategory("string")]
        public void SerializeStringWithSerializationContext()
        {
            string json = JsonSerializer.Serialize(_people, PersonSerializerContext.Default.IEnumerablePerson);
        }

        [Benchmark]
        [BenchmarkCategory("stream")]
        public void SerializeStreamWithSerializationContext()
        {
            var writer = new Utf8JsonWriter(_stream);
            JsonSerializer.Serialize(writer, _people, PersonSerializerContext.Default.IEnumerablePerson);
        }

    }
}
