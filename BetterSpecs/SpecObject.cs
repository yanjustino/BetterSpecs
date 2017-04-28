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

        //TODO: This variable will be removed coming soon.
        private readonly Dictionary<string, Func<object>> _functions = new Dictionary<string, Func<object>>();

        //TODO: This code will be removed coming soon. It doesn't have anyone utility in this context.
        [Obsolete("Please change this call to method \"Get<T>(string key)\"")]
        public object this[string key]
        {
            get
            {
                if (_values[key] == null)
                    _values[key] = _functions[key].Invoke();

                return _values[key];
            }
        }

        public T Get<T>(string key)
        {
            object instance = default(T);

            _values.TryGetValue(key, out instance);

            return (T)instance;
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