using System;

namespace Interpreter
{
    public class DictionaryException : Exception
    {
        public DictionaryException(string message)
            : base(message)
        { }
    }

    public class DictionaryKeyNotFoundException : Exception
    {
        public DictionaryKeyNotFoundException(string key)
            : base("Key not found in dictionary: " + key)
        { }
    }

    public class NoSplitAxisException : Exception
    {
        public NoSplitAxisException()
            : base("There is no axis to split on")
        { }
    }
}
