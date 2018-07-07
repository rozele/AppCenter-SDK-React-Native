// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using ReactNative.Bridge;
using ReactNative.Modules.Core;
using ReactNative.UIManager;

namespace Microsoft.AppCenter.ReactNative.Analytics
{
    public class AppCenterReactNativeAnalyticsPackage : IReactPackage
    {
        private readonly string _appSecret;

        public AppCenterReactNativeAnalyticsPackage(string appSecret)
        {
            _appSecret = appSecret;
        }

        public IReadOnlyList<INativeModule> CreateNativeModules(ReactContext reactContext)
        {
            return new List<INativeModule>
            {
                new AppCenterReactNativeAnalyticsModule(_appSecret),
            };
        }

        public IReadOnlyList<IViewManager> CreateViewManagers(ReactContext reactContext)
        {
            return new List<IViewManager>(0);
        }
    }
}
