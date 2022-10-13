// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace NuGet.CommandLine.XPlat
{
    internal class ListPackageOutputContent
    {
        internal int Version { get; set; } = ListPackageJsonOutputSerializer.Version;
        internal ListPackageArgs ListPackageArgs { get; set; }
        internal List<ReportProblem> Problems { get; set; }
        internal List<ListPackageProjectModel> Projects { get; set; }
        internal bool AutoReferenceFound { get; set; }
    }
}
