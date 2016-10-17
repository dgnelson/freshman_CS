﻿using System.Collections;
using System.Collections.Generic;

namespace Interpreter
{
    /// <summary>
    /// An abstract dictionary of objects
    /// </summary>
    public abstract class Dictionary : IEnumerable<KeyValuePair<string, object>>
    {
        /// <summary>
        /// Add object to dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public abstract void Store(string key, object value);

        /// <summary>
        /// Finds and returns the value associated with the key, or null
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract object Lookup(string key);

        /// <summary>
        /// Returns the number of items stored in the dictionary.
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// Enumerates all the entries in the dictionary so you can loop over them.
        /// </summary>
        public abstract IEnumerator<KeyValuePair<string, object>> GetEnumerator();

         IEnumerator IEnumerable.GetEnumerator()
         {
             return ((IEnumerable<KeyValuePair<string, object>>)this).GetEnumerator();
         }
    }
}
