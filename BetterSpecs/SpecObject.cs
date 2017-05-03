using FluentAssertions;
using System;
using System.Collections.Generic;

namespace BetterSpecs
{
    public abstract class SpecObject
    {
        protected int _ident;

        public Action this[string text]
        {
            set
            {
                Before();
                Invoke(value, text);
                After();
            }
        }

        internal virtual void After() { }
        internal virtual void Before() { }

        internal virtual void Invoke(Action action, string text)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(Indent(_ident) + text);
            action.Invoke();
        }
        private string Indent(int count)
        {
            return "".PadLeft(count);
        }
    }

    public class Let
    {
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        [Obsolete("Please change this call to method \"Get<T>(string key)\". This code will be removed as soon.")]
        public object this[string key]
        {
            get
            {
                return ((dynamic)_values[key])();
            }
        }

        public T Get<T>(string key)
        {
            dynamic instance = default(T);

            _values.TryGetValue(key, out instance);

            return (T)instance();
        }

        public void Add(string key, Func<object> action)
        {
            _values.Add(key, action());
        }
    }

    public class Describe : SpecObject
    {
        internal override void Before() => Console.WriteLine();
        internal override void After() => Console.WriteLine();
    }

    public class It : SpecObject
    {
        internal override void Before() => _ident = 8;
        public Expect Expect { get; }
        public It()
        {
            Expect = new Expect();
        }

        public Expect ExpectThatSubject()
        {
            return Expect;
        }
    }

    public class Expect
    {
        public Expect BeEquals(object actualValue, object expectedValue)
        {
            actualValue.Should().Be(expectedValue);

            return this;
        }

        public Expect NotBeEquals(object actualValue, object expectedValue)
        {
            actualValue.Should().NotBe(expectedValue);

            return this;
        }

        public Expect BeNull(object actualValue)
        {
            actualValue.Should().BeNull();

            return this;
        }

        public Expect NotBeNull(object actualValue)
        {
            actualValue.Should().NotBeNull();

            return this;
        }

        public Expect BeGreatThan(int actualValue, int comparedValue)
        {
            actualValue.Should().BeGreaterThan(comparedValue);

            return this;
        }

        public Expect BeLessThan(int actualValue, int comparedValue)
        {
            actualValue.Should().BeLessThan(comparedValue);

            return this;
        }

        public Expect BeGreatThan(long actualValue, long comparedValue)
        {
            actualValue.Should().BeGreaterThan(comparedValue);

            return this;
        }

        public Expect BeLessThan(long actualValue, long comparedValue)
        {
            actualValue.Should().BeLessThan(comparedValue);

            return this;
        }

        public Expect BeGreatThan(decimal actualValue, decimal comparedValue)
        {
            actualValue.Should().BeGreaterThan(comparedValue);

            return this;
        }

        public Expect BeLessThan(decimal actualValue, decimal comparedValue)
        {
            actualValue.Should().BeLessThan(comparedValue);

            return this;
        }
    }

    public class Context : SpecObject
    {
        internal override void Before() => _ident = 4;
    }

    public class Subject
    {
        private object Data { get; set; }

        public void Assign<T>(T instance)
        {
            Data = instance;
        }

        public T Get<T>()
        {
            return (T)Data;
        }
    }
}