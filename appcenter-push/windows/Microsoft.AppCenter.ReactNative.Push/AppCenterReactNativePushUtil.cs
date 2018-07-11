// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AppCenter.Push;
using Newtonsoft.Json.Linq;

namespace Microsoft.AppCenter.ReactNative.Push
{
    static class AppCenterReactNativePushUtil
    {
        public static JObject ConvertPushNotificationToJObject(PushNotificationReceivedEventArgs e)
        {
            if (e == null)
            {
                return new JObject();
            }

            var result = new JObject
            {
                { "title", e.Title },
                { "message", e.Message },
            };

            if (e.CustomData.Count > 0)
            {
                var customProperties = new JObject();
                foreach (var pair in e.CustomData)
                {
                    customProperties.Add(pair.Key, pair.Value);
                }

                result.Add("customProperties", customProperties);
            }

            return result;
        }
    }
}
