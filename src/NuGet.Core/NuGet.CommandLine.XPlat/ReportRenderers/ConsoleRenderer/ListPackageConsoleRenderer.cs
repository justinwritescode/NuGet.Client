// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;

namespace NuGet.CommandLine.XPlat
{
    internal class ListPackageConsoleRenderer : IReportRenderer
    {
        protected List<ReportProblem> _problems = new();

        public TextWriter TextWriter { get; }

        private ListPackageConsoleRenderer(TextWriter textWriter)
        {
            TextWriter = textWriter != null ? textWriter : Console.Out;
        }

        public static ListPackageConsoleRenderer GetInstance(TextWriter textWriter = null)
        {
            return new ListPackageConsoleRenderer(textWriter);
        }

        public void AddProblem(string errorText, ProblemType problemType)
        {
            _problems.Add(new ReportProblem(string.Empty, errorText, problemType));
        }

        public IEnumerable<ReportProblem> GetProblems()
        {
            return _problems;
        }

        public void End(ListPackageReportModel listPackageReportModel)
        {
            ListPackageConsoleWriter.Render(new ListPackageOutputContentV1()
            {
                ListPackageArgs = listPackageReportModel.ListPackageArgs,
                Problems = _problems,
                Projects = listPackageReportModel.Projects,
                AutoReferenceFound = listPackageReportModel.AutoReferenceFound
            });
        }
    }
}
