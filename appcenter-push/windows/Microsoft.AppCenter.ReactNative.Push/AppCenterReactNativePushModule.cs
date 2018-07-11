// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AppCenter.ReactNative.Shared;
using ReactNative.Bridge;
using ReactNative.Modules.Core;

namespace Microsoft.AppCenter.ReactNative.Push
{
    public class AppCenterReactNativePushModule : ReactContextNativeModuleBase
    {
        private const string ErrorNotSupported = "E_NOTSUPPORTED";
        private const string OnPushNotificationReceivedEvent = "AppCenterPushNotificationReceived";

        public AppCenterReactNativePushModule(ReactContext reactContext, string appSecret)
            : base(reactContext)
        {
            AppCenterReactNativeShared.ConfigureAppCenter(appSecret);
            Microsoft.AppCenter.Push.Push.PushNotificationReceived += OnPushNotificationReceived;
        }

        public override string Name => "AppCenterReactNativePush";

        [ReactMethod]
        public async void setEnabled(bool enabled, IPromise promise)
        {
            await Microsoft.AppCenter.Push.Push.SetEnabledAsync(enabled);
            promise.Resolve(null);
        }

        [ReactMethod]
        public async void isEnabled(IPromise promise)
        {
            var enabled = await Microsoft.AppCenter.Push.Push.IsEnabledAsync();
            promise.Resolve(enabled);
        }

        [ReactMethod]
        public void sendAndClearInitialNotification(IPromise promise)
        {
            promise.Reject(ErrorNotSupported, "The lastSessionCrashReport API is not supported in UWP.");
        }

        private void OnPushNotificationReceived(object sender, Microsoft.AppCenter.Push.PushNotificationReceivedEventArgs e)
        {
            // TODO: add initial notification support using Push.CheckLaunchedFromNotification
            Context.GetJavaScriptModule<RCTDeviceEventEmitter>()
                .emit(OnPushNotificationReceivedEvent, AppCenterReactNativePushUtil.ConvertPushNotificationToJObject(e));
        }
    }
}
