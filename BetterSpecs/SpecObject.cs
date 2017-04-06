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
            text = text.PadLeft(_ident + text.Length, ' ');

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(text);
            action.Invoke();
        }
    }

    public class Let
    {
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();
        private readonly Dictionary<string, Func<object>> _functions = new Dictionary<string, Func<object>>();

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
            if (_values[key] == null)
                _values[key] = _functions[key].Invoke();

            return (T)_values[key];
        }

        public void Add(string key, Func<object> action)
        {
            _functions.Add(key, action);
            _values.Add(key, null);
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
    }

    public class Context : SpecObject
    {
        internal override void Before() => _ident = 4;
    }
}

