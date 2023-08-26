using System;
using System.Collections.Generic;
using Unity.Services.CloudCode;
using UnityEngine;

namespace CeltaGames
{
    public class CloudCodeTesting : MonoBehaviour
    {
        StringTest _welcomeMessage;

        public async void SendHelloWorld(string exampleName)
        {
            var arguments = new Dictionary<string, object> { { "name", exampleName } };
            _welcomeMessage = await CloudCodeService.Instance.CallEndpointAsync<StringTest>("HelloWorld", arguments);
            Debug.Log(_welcomeMessage.Example);
        }

        public class StringTest
        {
            public string Example;

            public StringTest()
            {
                Example = "Rafa";
            }

            public StringTest(string name)
            {
                Example = name;
            }
        }
    }
}
