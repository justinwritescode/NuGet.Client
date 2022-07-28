// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace NuGet.CommandLine.XPlat.ReportRenderers.JsonRenderers
{
    internal class JsonRendererV1 : JsonRenderer
    {
        public JsonRendererV1()
            : base(OutputVersion.V1)
        {
        }
    }
}
