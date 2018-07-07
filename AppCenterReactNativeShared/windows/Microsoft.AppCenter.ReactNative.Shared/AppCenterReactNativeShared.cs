// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.AppCenter.ReactNative.Shared
{
    public static class AppCenterReactNativeShared
    {
        public static void ConfigureAppCenter(string appSecret)
        {
            if (AppCenter.Configured)
            {
                return;
            }

            AppCenter.Configure(appSecret);
        }
    }
}
