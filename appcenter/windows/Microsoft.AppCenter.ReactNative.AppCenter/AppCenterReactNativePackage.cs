// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using ReactNative.Bridge;
using ReactNative.Modules.Core;
using ReactNative.UIManager;
using System.Collections.Generic;

namespace Microsoft.AppCenter.ReactNative.AppCenter
{
    public class AppCenterReactNativePackage : IReactPackage
    {
        private readonly string _appSecret;

        public AppCenterReactNativePackage(string appSecret)
        {
            _appSecret = appSecret;
        }

        public IReadOnlyList<INativeModule> CreateNativeModules(ReactContext reactContext)
        {
            return new List<INativeModule>
            {
                new AppCenterReactNativeModule(_appSecret),
            };
        }

        public IReadOnlyList<IViewManager> CreateViewManagers(ReactContext reactContext)
        {
            return new List<IViewManager>(0);
        }
    }
}
