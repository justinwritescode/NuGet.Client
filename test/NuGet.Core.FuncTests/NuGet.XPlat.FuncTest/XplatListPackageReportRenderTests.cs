// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using NuGet.CommandLine.XPlat;
using NuGet.Test.Utility;
using Xunit;
using System.Collections.Generic;
using System.IO;
using NuGet.Frameworks;
using FluentAssertions;
using System;
using NuGet.Configuration.Test;

namespace NuGet.XPlat.FuncTest
{
    public class XplatListPackageReportRenderTests
    {
        private const int _successExitCode = 0;

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task XPlat_ListPackage_ConsoleOutput_Succeeds(bool useExplicitOutputFormatType)
        {
            // Arrange
            using (var pathContext = new SimpleTestPathContext())
            {
                // Set up solution, project, and packages
                var solution = new SimpleTestSolutionContext(pathContext.SolutionRoot);
                var packagesForSource = new List<SimpleTestPackageContext>();
                var framework = FrameworkConstants.CommonFrameworks.NetCoreApp20;
                string consoleOutputFileName = Path.Combine(pathContext.SolutionRoot, "consoleOutput.txt");

                var projectA = SimpleTestProjectContext.CreateNETCore("projectA", pathContext.SolutionRoot, framework);

                await SimpleTestPackageUtility.CreateFolderFeedV3Async(
                   pathContext.PackageSource,
                   new[]
                   {
                       new SimpleTestPackageContext()
                    {
                        Id = "PackageA",
                        Version = "1.0.0"
                    },
                    new SimpleTestPackageContext()
                    {
                        Id = "PackageB",
                        Version = "1.0.0"
                    }
                   });

                projectA.AddPackageToAllFrameworks(new[]
                {
                    new SimpleTestPackageContext()
                    {
                        Id = "PackageA",
                        Version = null,
                    },
                    new SimpleTestPackageContext()
                    {
                        Id = "PackageB",
                        Version = null
                    },
                });


                solution.Projects.Add(projectA);
                solution.Create(pathContext.SolutionRoot);

                using FileStream fileStream = new FileStream(consoleOutputFileName, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fileStream);

                var log = new TestCommandOutputLogger();

                var listArgs = new List<string>
                {
                    "package",
                    "list",
                    projectA.ProjectPath
                };

                if (useExplicitOutputFormatType)
                {
                    listArgs.Add("--format");
                    listArgs.Add("Console");
                }

                // Act
                var r = Util.RestoreSolution(pathContext);
                sw.AutoFlush = true;
                Console.SetOut(sw);

                var exitCode = Program.MainInternal(listArgs.ToArray(), log);

                // Close previous output stream and redirect output to standard output.
                Console.Out.Close();
                sw = new StreamWriter(Console.OpenStandardOutput());
                sw.AutoFlush = true;
                Console.SetOut(sw);

                // Assert
                r.Success.Should().BeTrue();
                Assert.Equal(_successExitCode, exitCode);
                SettingsTestUtils.RemoveWhitespace(File.ReadAllText(consoleOutputFileName)).Should()
                    .Be(SettingsTestUtils.RemoveWhitespace(@"Project 'projectA' has the following package references
   [netcoreapp2.0]: 
   Top-level Package      Requested   Resolved
   > PackageA                         1.0.0   
   > PackageB                         1.0.0   "));
            }
        }

        //[Fact]
        //public void ConsoleRenderer_ListPackage_SingleProject_Succeeds()
        //{
        //    // Arrange
        //    using (var pathContext = new SimpleTestPathContext())
        //    {
        //        string fileName = Path.Combine(pathContext.SolutionRoot, "consoleOutput.txt");
        //        string frameWork = "net5.0";
        //        using FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate);
        //        using StreamWriter writer = new StreamWriter(stream);
        //        writer.AutoFlush = true;
        //        string projectName = Path.Combine(pathContext.SolutionRoot, "someproj.csproj");
        //        ListPackageConsoleRenderer listPackageConsoleRenderer = ListPackageConsoleRenderer.GetInstance(writer);
        //        var packageRefArgs = new ListPackageArgs(
        //                    path: projectName,
        //                    packageSources: new List<PackageSource>() { new PackageSource(pathContext.PackageSource) },
        //                    frameworks: new List<string>() { frameWork },
        //                    reportType: ReportType.Default,
        //                    renderer: listPackageConsoleRenderer,
        //                    includeTransitive: false,
        //                    prerelease: false,
        //                    highestPatch: false,
        //                    highestMinor: false,
        //                    NullLogger.Instance,
        //                    CancellationToken.None);

        //        ListPackageReportModel listPackageReportModel = new ListPackageReportModel(packageRefArgs);
        //        ListPackageProjectModel projectModel = listPackageReportModel.CreateProjectReportData(projectName);
        //        projectModel.SetFrameworkPackageMetaData(new List<ListPackageReportFrameworkPackage>()
        //        {
        //            new ListPackageReportFrameworkPackage(frameWork)
        //            {
        //                TopLevelPackages =  null,
        //                TransitivePackages = null,
        //            }
        //        });

        //        // Act
        //        listPackageConsoleRenderer.End(listPackageReportModel);

        //        // Assert
        //        using StreamReader reader = new StreamReader(stream);
        //        string line;
        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            System.Console.WriteLine(line);
        //        }
        //    }
        //}
    }
}
