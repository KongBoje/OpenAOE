﻿// The MIT License (MIT)
//
// Copyright (c) 2013 Jacob Dufault
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
// associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
// NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

// Modified for use with OpenAOE by Simon Moles
using System;
using System.Collections.Generic;
using OpenAOE.Engine.Data;

namespace OpenAOE.Engine.Utility
{
    /// <summary>
    /// Maps different types of Data to a sequential set of integers.
    /// </summary>
    internal static class DataAccessorFactory
    {
        /// <summary>
        /// Maps a set of types to their respective DataAccessors.
        /// </summary>
        /// <param name="types">The types to map</param>
        /// <returns>A list of DataAccessors that have a 1 to 1 correspondence to the given
        /// types.</returns>
        public static DataAccessor[] MapTypesToDataAccessors(Type[] types)
        {
            DataAccessor[] accessors = new DataAccessor[types.Length];
            for (int i = 0; i < types.Length; ++i)
            {
                accessors[i] = new DataAccessor(types[i]);
            }
            return accessors;
        }

        /// <summary>
        /// The next integer to use.
        /// </summary>
        private static int _nextId = 0;

        /// <summary>
        /// The mapping from Data types to their integers
        /// </summary>
        private static readonly Dictionary<Type, int> _ids = new Dictionary<Type, int>();

        /// <summary>
        /// Returns the identifier/integer for the given type, constructing if it necessary.
        /// </summary>
        /// <param name="type">The type to get.</param>
        /// <returns>The identifier/integer</returns>
        public static int GetId(Type type)
        {
            if(type.IsAbstract || !type.IsSubclassOf(typeof(IComponent)))
            {

                throw new ArgumentException($"Type {type} is abstract or not subclass of {typeof (IComponent)}");
            }

            if (_ids.ContainsKey(type) == false)
            {
                _ids[type] = _nextId++;
            }

            return _ids[type];
        }

        /// <summary>
        /// Looks up the type of data is used for the given accessor.
        /// </summary>
        /// <remarks>
        /// The current implementation of this method runs in O(n) time (instead of O(1)).
        /// </remarks>
        /// <param name="accessor">The data accessor.</param>
        /// <returns>The type used for the given accessor, or an exception if there is
        /// none.</returns>
        public static Type GetTypeFromAccessor(DataAccessor accessor)
        {
            return GetTypeFromId(accessor.Id);
        }

        /// <summary>
        /// Looks up the type of data is used for the given accessor.
        /// </summary>
        /// <remarks>
        /// The current implementation of this method runs in O(n) time (instead of O(1)).
        /// </remarks>
        /// <param name="accessor">The data accessor id.</param>
        /// <returns>The type used for the given accessor, or an exception if there is
        /// none.</returns>
        public static Type GetTypeFromId(int accessorId)
        {
            foreach (var item in _ids)
            {
                if (item.Value == accessorId)
                {
                    return item.Key;
                }
            }

            throw new Exception("No such type of accessor=" + accessorId);
        }
    }
}