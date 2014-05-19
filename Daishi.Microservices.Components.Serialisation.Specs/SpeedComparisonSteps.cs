﻿#region Includes

using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;

#endregion

namespace Daishi.Microservices.Components.Serialisation.Specs {
    [Binding]
    public class SpeedComparisonSteps {
        private Stopwatch _stopwatch;
        private long _result1, _result2;

        [Given(@"I have parsed a JSON object using these components")]
        public void GivenIHaveParsedAJSONObjectUsingTheseComponents() {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();

            var parser = new StandardJsonParser();
            Json.Parse(parser, Resources.LargeJSON, "response");

            _stopwatch.Stop();
            _result1 = _stopwatch.ElapsedMilliseconds;
        }

        [Given(@"I have parsed a JSON object using JSON\.NET")]
        public void GivenIHaveParsedAJSONObjectUsingJSON_NET() {
            _stopwatch.Start();

            var jsonObject = JObject.Parse(Resources.LargeJSON);
            var response = jsonObject["response"].ToString(Formatting.None);

            _stopwatch.Stop();
            _result2 = _stopwatch.ElapsedMilliseconds;
        }

        [Then(@"the response value associated with these components should be the lowest value")]
        public void ThenTheResponseValueAssociatedWithTheseComponentsShouldBeTheLowestValue() {
            Console.WriteLine(string.Concat("These components: ", _result1));
            Console.WriteLine(string.Concat("JSON.NET: ", _result2));
            Assert.Greater(_result2, _result1);
        }
    }
}