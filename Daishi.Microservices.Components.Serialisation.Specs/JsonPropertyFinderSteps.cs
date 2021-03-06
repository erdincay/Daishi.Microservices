﻿#region Includes

using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

#endregion

namespace Daishi.Microservices.Components.Serialisation.Specs {
    [Binding]
    public class JsonPropertyFinderSteps {
        private string _jsonProperty;
        private BinaryReader _reader;
        private JsonPropertyFinder _finder;

        [Given(@"I have supplied a JSON property")]
        public void GivenIHaveSuppliedAJSONProperty() {
            _jsonProperty = "address";
        }

        [When(@"I invoke a JsonPropertyFinder")]
        public void WhenIInvokeAJsonPropertyFinder() {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(Resources.SimpleJSON));
            _reader = new BinaryReader(stream);
            _finder = new JsonPropertyFinder(new CharacterFinder(_reader), new WordBuilder(_reader),
                new JsonPropertyValidator(_reader));
        }

        [Then(@"the JsonPropertyFinder will return the property's position in the Stream")]
        public void ThenTheJsonPropertyFinderWillReturnThePropertySPositionInTheStream() {
            var positions = _finder.Find(_jsonProperty);
            var firstValidPosition = positions.First(p => p > -1);

            Assert.AreEqual(37L, firstValidPosition);
            _reader.Dispose();
        }
    }
}