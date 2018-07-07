// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AppCenter.ReactNative.Shared;
using Newtonsoft.Json.Linq;
using ReactNative.Bridge;

namespace Microsoft.AppCenter.ReactNative.Analytics
{
    public class AppCenterReactNativeAnalyticsModule : NativeModuleBase
    {
        public AppCenterReactNativeAnalyticsModule(string appSecret)
        {
            AppCenterReactNativeShared.ConfigureAppCenter(appSecret);
            AppCenter.Start(typeof(Microsoft.AppCenter.Analytics.Analytics));
            // TODO: add `startEnabled` parameter?
        }

        public override string Name => "AppCenterReactNativeAnalytics";

        [ReactMethod]
        public async void setEnabled(bool enabled, IPromise promise)
        {
            await Microsoft.AppCenter.Analytics.Analytics.SetEnabledAsync(enabled);
            promise.Resolve(null);
        }

        [ReactMethod]
        public async void isEnabled(IPromise promise)
        {
            var enabled = await Microsoft.AppCenter.Analytics.Analytics.IsEnabledAsync();
            promise.Resolve(enabled);
        }

        [ReactMethod]
        public void trackEvent(string eventName, JObject properties, IPromise promise)
        {
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent(eventName, ReactNativeUtils.ToStringDictionary(properties));
            promise.Resolve("");
        }
    }
}
