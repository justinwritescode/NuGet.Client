// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Xml.Linq;
using Moq;
using Newtonsoft.Json.Linq;
using NuGet.Configuration;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Test.Utility;
using NuGet.Versioning;
using Test.Utility;
using Xunit;

namespace NuGet.XPlat.FuncTest
{
    public static class Util
    {
        private static readonly string NupkgFileFormat = "{0}.{1}.nupkg";

        public static string GetMockServerResource()
        {
            return GetResource("NuGet.CommandLine.Test.compiler.resources.mockserver.xml");
        }

        public static string GetResource(string name)
        {
            using (var reader = new StreamReader(typeof(Util).GetTypeInfo().Assembly.GetManifestResourceStream(name)))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Restore a solution.
        /// </summary>
        public static CommandRunnerResult RestoreSolution(SimpleTestPathContext pathContext, int expectedExitCode = 0, params string[] additionalArgs)
        {
            return Restore(pathContext, pathContext.SolutionRoot, expectedExitCode, additionalArgs);
        }

        /// <summary>
        /// Run nuget.exe restore {inputPath}
        /// </summary>
        public static CommandRunnerResult Restore(SimpleTestPathContext pathContext, string inputPath, int expectedExitCode = 0, params string[] additionalArgs)
        {
            var nugetExe = GetNuGetExePath();

            var args = new string[]
                {
                    "restore",
                    inputPath,
                    "-Verbosity",
                    "detailed"
                };

            args = args.Concat(additionalArgs).ToArray();

            return RunCommand(pathContext, nugetExe, expectedExitCode, args);
        }

        public static CommandRunnerResult RunCommand(SimpleTestPathContext pathContext, string nugetExe, int expectedExitCode = 0, params string[] arguments)
        {
            // Store the dg file for debugging
            var dgPath = Path.Combine(pathContext.WorkingDirectory, "out.dg");
            var envVars = new Dictionary<string, string>()
                {
                    { "NUGET_PERSIST_DG", "true" },
                    { "NUGET_PERSIST_DG_PATH", dgPath },
                    { "NUGET_HTTP_CACHE_PATH", pathContext.HttpCacheFolder }
                };

            // Act
            var r = CommandRunner.Run(
                nugetExe,
                pathContext.WorkingDirectory.Path,
                string.Join(" ", arguments),
                waitForExit: true,
                environmentVariables: envVars);

            // Assert
            Assert.True(expectedExitCode == r.ExitCode, r.Errors + "\n\n" + r.Output);

            return r;
        }

        public static string GetNuGetExePath()
        {
            const string fileName = "NuGet.exe";
            var targetDir = ConfigurationManager.AppSettings["TestTargetDir"] ?? Directory.GetCurrentDirectory();
            var nugetExe = Path.Combine(targetDir, "NuGet", fileName);
            // Revert to parent dir if not found under layout dir.
            if (!File.Exists(nugetExe)) nugetExe = Path.Combine(targetDir, fileName);
            if (!File.Exists(nugetExe)) throw new FileNotFoundException($"The NuGet executable is not present in '{targetDir}'", fileName);
            return nugetExe;
        }

        /// <summary>
        /// Creates a file with the specified content.
        /// </summary>
        /// <param name="directory">The directory of the created file.</param>
        /// <param name="fileName">The name of the created file.</param>
        /// <param name="fileContent">The content of the created file.</param>
        public static void CreateFile(string directory, string fileName, string fileContent)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var fileFullName = Path.Combine(directory, fileName);
            CreateFile(fileFullName, fileContent);
        }

        public static void CreateFile(string fileFullName, string fileContent)
        {
            using (var writer = new StreamWriter(fileFullName))
            {
                writer.Write(fileContent);
            }
        }

        public static string GetTestablePluginPath()
        {
            const string fileName = "CredentialProvider.Testable.exe";
            var targetDir = ConfigurationManager.AppSettings["TestTargetDir"] ?? Directory.GetCurrentDirectory();
            var plugin = Path.Combine(targetDir, "TestableCredentialProvider", fileName);
            // Revert to parent dir if not found under layout dir.
            if (!File.Exists(plugin)) plugin = Path.Combine(targetDir, fileName);
            if (!File.Exists(plugin)) throw new FileNotFoundException($"The CredentialProvider executable is not present in '{targetDir}'", fileName);
            return plugin;
        }

        public static void CreateNuGetConfig(string workingPath, List<string> sources)
        {
            var doc = new XDocument();
            var configuration = new XElement(XName.Get("configuration"));
            doc.Add(configuration);

            var config = new XElement(XName.Get("config"));
            configuration.Add(config);

            var globalFolder = new XElement(XName.Get("add"));
            globalFolder.Add(new XAttribute(XName.Get("key"), "globalPackagesFolder"));
            globalFolder.Add(new XAttribute(XName.Get("value"), Path.Combine(workingPath, "globalPackages")));
            config.Add(globalFolder);

            var solutionDir = new XElement(XName.Get("add"));
            solutionDir.Add(new XAttribute(XName.Get("key"), "repositoryPath"));
            solutionDir.Add(new XAttribute(XName.Get("value"), Path.Combine(workingPath, "packages")));
            config.Add(solutionDir);

            var packageSources = new XElement(XName.Get("packageSources"));
            configuration.Add(packageSources);
            packageSources.Add(new XElement(XName.Get("clear")));

            foreach (var source in sources)
            {
                var sourceEntry = new XElement(XName.Get("add"));
                sourceEntry.Add(new XAttribute(XName.Get("key"), source));
                sourceEntry.Add(new XAttribute(XName.Get("value"), source));
                packageSources.Add(sourceEntry);
            }

            var packageSourceMapping = new XElement(XName.Get("packageSourceMapping"));
            configuration.Add(packageSourceMapping);
            packageSourceMapping.Add(new XElement(XName.Get("clear")));

            Util.CreateFile(workingPath, "NuGet.Config", doc.ToString());
        }

        /// <summary>
        /// Utility for asserting faulty executions of nuget.exe
        ///
        /// Asserts a non-zero status code and a message on stderr.
        /// </summary>
        /// <param name="result">An instance of <see cref="CommandRunnerResult"/> with command execution results</param>
        /// <param name="expectedErrorMessage">A portion of the error message to be sent</param>
        public static void VerifyResultFailure(CommandRunnerResult result,
                                               string expectedErrorMessage)
        {
            Assert.True(
                result.ExitCode != 0,
                "nuget.exe DID NOT FAIL: Ouput is " + result.Output + ". Error is " + result.Errors);

            Assert.True(
                result.Errors.Contains(expectedErrorMessage),
                "Expected error is " + expectedErrorMessage + ". Actual error is " + result.Errors);
        }

        public static void VerifyPackageExists(
            PackageIdentity packageIdentity,
            string packagesDirectory)
        {
            string normalizedId = packageIdentity.Id.ToLowerInvariant();
            string normalizedVersion = packageIdentity.Version.ToNormalizedString().ToLowerInvariant();

            var packageIdDirectory = Path.Combine(packagesDirectory, normalizedId);
            Assert.True(Directory.Exists(packageIdDirectory));

            var packageVersionDirectory = Path.Combine(packageIdDirectory, normalizedVersion);
            Assert.True(Directory.Exists(packageVersionDirectory));

            var nupkgFileName = GetNupkgFileName(normalizedId, normalizedVersion);

            var nupkgFilePath = Path.Combine(packageVersionDirectory, nupkgFileName);
            Assert.True(File.Exists(nupkgFilePath));

            var nupkgSHAFilePath = Path.Combine(packageVersionDirectory, nupkgFileName + ".sha512");
            Assert.True(File.Exists(nupkgSHAFilePath));

            var nuspecFilePath = Path.Combine(packageVersionDirectory, normalizedId + ".nuspec");
            Assert.True(File.Exists(nuspecFilePath));
        }

        public static void VerifyPackageDoesNotExist(
            PackageIdentity packageIdentity,
            string packagesDirectory)
        {
            string normalizedId = packageIdentity.Id.ToLowerInvariant();
            var packageIdDirectory = Path.Combine(packagesDirectory, normalizedId);
            Assert.False(Directory.Exists(packageIdDirectory));
        }

        public static string GetNupkgFileName(string normalizedId, string normalizedVersion)
        {
            return string.Format(NupkgFileFormat, normalizedId, normalizedVersion);
        }

        /// <summary>
        /// Create a basic csproj file for net45.
        /// </summary>
        public static string GetCSProjXML(string projectName)
        {
            return @"<?xml version=""1.0"" encoding=""utf-8""?>
                <Project ToolsVersion=""14.0"" DefaultTargets=""Build"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
                  <Import Project=""$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props"" Condition=""Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')"" />
                  <PropertyGroup>
                    <Configuration Condition="" '$(Configuration)' == '' "">Debug</Configuration>
                    <Platform Condition="" '$(Platform)' == '' "">AnyCPU</Platform>
                    <ProjectGuid>29b6f645-ae2a-4653-a142-d0de9341adba</ProjectGuid>
                    <OutputType>Library</OutputType>
                    <AppDesignerFolder>Properties</AppDesignerFolder>
                    <RootNamespace>$NAME$</RootNamespace>
                    <AssemblyName>$NAME$</AssemblyName>
                    <TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion>
                    <FileAlignment>512</FileAlignment>
                    <DebugSymbols>true</DebugSymbols>
                    <DebugType>full</DebugType>
                    <Optimize>false</Optimize>
                    <OutputPath>bin\Debug\</OutputPath>
                    <DefineConstants>DEBUG;TRACE</DefineConstants>
                    <ErrorReport>prompt</ErrorReport>
                    <WarningLevel>4</WarningLevel>
                  </PropertyGroup>
                  <ItemGroup>
                    <Reference Include=""System""/>
                    <Reference Include=""System.Core""/>
                    <Reference Include=""System.Xml.Linq""/>
                    <Reference Include=""System.Data.DataSetExtensions""/>
                    <Reference Include=""Microsoft.CSharp""/>
                    <Reference Include=""System.Data""/>
                    <Reference Include=""System.Net.Http""/>
                    <Reference Include=""System.Xml""/>
                  </ItemGroup>
                  <Import Project=""$(MSBuildToolsPath)\Microsoft.CSharp.targets"" />
                 </Project>".Replace("$NAME$", projectName);
        }

        public static void CreateConfigFile(string path, string configFileName, string targetFramework, IEnumerable<PackageIdentity> packages)
        {
            var fileContent = IsProjectJson(configFileName)
                ? GetProjectJsonFileContents(targetFramework, packages)
                : GetPackagesConfigFileContents(targetFramework, packages);

            CreateFile(path, configFileName, fileContent);
        }

        public static string GetProjectJsonFileContents(string targetFramework, IEnumerable<PackageIdentity> packages)
        {
            var dependencies = string.Join(", ", packages.Select(package => $"'{package.Id}': '{package.Version}'"));
            return $@"
{{
  'dependencies': {{
    {dependencies}
  }},
  'frameworks': {{
    '{targetFramework}': {{ }}
  }}
}}";
        }

        public static string GetPackagesConfigFileContents(string targetFramework, IEnumerable<PackageIdentity> packages)
        {
            var dependencies = string.Join("\n", packages.Select(package => $@"<package id=""{package.Id}"" version=""{package.Version}"" targetFramework=""{targetFramework}"" />"));
            return $@"
<packages>
  {dependencies}
</packages>";
        }

        private static bool IsProjectJson(string configFileName)
        {
            // Simply test the extension as that is all we care about
            return string.Equals(Path.GetExtension(configFileName), ".json", StringComparison.OrdinalIgnoreCase);
        }
    }
}
