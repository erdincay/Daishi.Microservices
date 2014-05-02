﻿#region Includes

using System.Collections.Generic;
using System.IO;

#endregion

namespace Daishi.Microservices.Components.Serialisation {
    public class CharacterFinder {
        private readonly StreamReader _reader;

        public CharacterFinder(StreamReader reader) {
            _reader = reader;
        }

        public IEnumerable<long> Find(char target) {
            int current;
            do {
                current = _reader.Read();
                var character = (char) current;

                if (character.Equals(target))
                    yield return current;
            } while (current > -1);
        }
    }
}