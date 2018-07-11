// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AppCenter.ReactNative.Shared;
using ReactNative.Bridge;
using System;
using System.Threading.Tasks;

namespace Microsoft.AppCenter.ReactNative.Crashes
{
    public class AppCenterReactNativeCrashesModule : NativeModuleBase
    {
        private const string ErrorNotSupported = "E_NOTSUPPORTED";

        public AppCenterReactNativeCrashesModule(string appSecret)
        {
            AppCenterReactNativeShared.ConfigureAppCenter(appSecret);
            AppCenter.Start(typeof(Microsoft.AppCenter.Crashes.Crashes));
        }

        public override string Name => "AppCenterReactNativeCrashes";

        [ReactMethod]
        public void lastSessionCrashReport(IPromise promise)
        {
            promise.Reject(ErrorNotSupported, "The lastSessionCrashReport API is not supported in UWP.");
        }

        [ReactMethod]
        public void hasCrashedInLastSession(IPromise promise)
        {
            promise.Reject(ErrorNotSupported, "The hasCrashedInLastSession API is not supported in UWP.");
        }

        [ReactMethod]
        public void setEnabled(bool enabled, IPromise promise)
        {
            promise.Reject(ErrorNotSupported, "The setEnabled API is not supported in UWP.");
        }

        [ReactMethod]
        public void isEnabled(bool enabled, IPromise promise)
        {
            promise.Reject(ErrorNotSupported, "The isEnabled API is not supported in UWP.");
        }

        [ReactMethod]
        public async void generateTestCrash(IPromise promise)
        {
            await Task.Run(() =>
            {
                Microsoft.AppCenter.Crashes.Crashes.GenerateTestCrash();
                promise.Reject(new Exception("generateTestCrash failed to generate a crash"));
            });
        }
    }
}
