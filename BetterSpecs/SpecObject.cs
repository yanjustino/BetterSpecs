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

        public It ExpectBeEqual()
        {


            return this;
        }
    }

    public class Context : SpecObject
    {
        internal override void Before() => _ident = 4;
    }
}