﻿#region Includes

using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

#endregion

namespace Daishi.Microservices.Components.Serialisation.Specs {
    [Binding]
    public class JsonSerialisorSteps {
        private ReasonablyComplexObject _reasonablyComplexObject;
        private StandardJsonSerialisationStrategy _jsonSerialisor;

        [Given(@"I have supplied a reasonably complex POCO")]
        public void GivenIHaveSuppliedAReasonablyComplexPOCO() {
            _reasonablyComplexObject = new ReasonablyComplexObject {
                Name = "Reasonably Complex Object",
                Count = 100,
                Strings = new List<string> {"One", "Two", "Three"},
                Floats = new[] {1f, 2f, 3f}, Level2 = new Level2 {
                    Name = "Level2",
                    Description = "Level #2",
                    Count = 2,
                    Strings = new List<string> {
                        "One", "Two", "Three"
                    },
                    Level3 = new Level3 {
                        Name = "Level3",
                        Description = "Level #3",
                        Count = 3
                    }
                }
            };
        }

        [Given(@"I have instantiated a JsonSerialisor")]
        public void GivenIHaveInstantiatedAJsonSerialisor() {
            var writer = new BinaryWriter(new MemoryStream(), new UTF8Encoding(false));
            _jsonSerialisor = new StandardJsonSerialisationStrategy(writer);
        }

        [When(@"I serialise the POCO")]
        public void WhenISerialiseThePOCO() {
            var serialisableProperties = _reasonablyComplexObject.GetSerializableProperties();
            Json.Serialise(_jsonSerialisor, new JsonPropertiesSerialisor(serialisableProperties));
        }

        [Then(@"the POCO should be serialised")]
        public void ThenThePOCOShouldBeSerialised() {
            string metadata;

            using (var reader = new StreamReader(new MemoryStream(_jsonSerialisor.SerialisedObject), Encoding.UTF8))
                metadata = reader.ReadToEnd();

            Assert.AreEqual(Resources.SerialisedReasonablyComplexObject, metadata);
        }
    }
}