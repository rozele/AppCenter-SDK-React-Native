// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AppCenter.ReactNative.Shared;
using Newtonsoft.Json.Linq;
using ReactNative.Bridge;

namespace Microsoft.AppCenter.ReactNative.AppCenter
{
    public class AppCenterReactNativeModule : NativeModuleBase
    {
        public AppCenterReactNativeModule(string appSecret)
        {
            AppCenterReactNativeShared.ConfigureAppCenter(appSecret);
        }

        public override string Name => "AppCenterReactNative";

        [ReactMethod]
        public async void setEnabled(bool enabled, IPromise promise)
        {
            await Microsoft.AppCenter.AppCenter.SetEnabledAsync(enabled);
            promise.Resolve(null);
        }

        [ReactMethod]
        public async void isEnabled(IPromise promise)
        {
            var enabled = await Microsoft.AppCenter.AppCenter.IsEnabledAsync();
            promise.Resolve(enabled);
        }

        [ReactMethod]
        public void setLogLevel(int logLevel)
        {
            Microsoft.AppCenter.AppCenter.LogLevel = (LogLevel)logLevel;
        }

        [ReactMethod]
        public void getLogLevel(IPromise promise)
        {
            promise.Resolve(Microsoft.AppCenter.AppCenter.LogLevel);
        }

        [ReactMethod]
        public async void getInstalledId(IPromise promise)
        {
            var installedId = await Microsoft.AppCenter.AppCenter.GetInstallIdAsync();
            promise.Resolve(installedId);
        }

        [ReactMethod]
        public void setCustomProperties(JObject properties)
        {
            Microsoft.AppCenter.AppCenter.SetCustomProperties(ReactNativeUtils.ToCustomProperties(properties));
        }
    }
}
