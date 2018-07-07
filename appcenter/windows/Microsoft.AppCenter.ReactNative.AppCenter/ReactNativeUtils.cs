// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace Microsoft.AppCenter.ReactNative.AppCenter
{
    static class ReactNativeUtils
    {
        public static CustomProperties ToCustomProperties(JObject properties)
        {
            var customProperties = new CustomProperties();
            foreach (var pair in properties)
            {
                Debug.Assert(pair.Value is JObject);
                var type = pair.Value.Value<string>("type");
                switch (type)
                {
                    case "clear":
                        customProperties.Clear(pair.Key);
                        break;
                    case "string":
                        customProperties.Set(pair.Key, pair.Value.Value<string>("value"));
                        break;
                    case "number":
                        customProperties.Set(pair.Key, pair.Value.Value<double>("value"));
                        break;
                    case "boolean":
                        customProperties.Set(pair.Key, pair.Value.Value<bool>("value"));
                        break;
                    case "date-time":
                        var epochMilliseconds = pair.Value.Value<long>("value");
                        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(epochMilliseconds);
                        customProperties.Set(pair.Key, dateTimeOffset.DateTime); // TODO: should we use `UtcDateTime`?
                        break;
                }
            }

            return customProperties;
        }
    }
}
 