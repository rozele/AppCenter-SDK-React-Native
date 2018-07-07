// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Microsoft.AppCenter.ReactNative.Analytics
{
    static class ReactNativeUtils
    {
        public static IDictionary<string, string> ToStringDictionary(JObject properties)
        {
            var stringDictionary = new Dictionary<string, string>(properties.Count);
            foreach (var pair in properties)
            {
                var type = pair.Value.Type;
                // Only support storing strings. Non-string data must be stringified in JS.
                if (type == JTokenType.String)
                {
                    stringDictionary.Add(pair.Key, pair.Value.Value<string>());
                }
            }

            return stringDictionary;
        }
    }
}
