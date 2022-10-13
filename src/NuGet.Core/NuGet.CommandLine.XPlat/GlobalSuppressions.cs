// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'void AddPackageReferenceCommand.Register(CommandLineApplication app, Func<ILogger> getLogger, Func<IPackageReferenceCommandRunner> getCommandRunner)', validate parameter 'app' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.AddPackageReferenceCommand.Register(Microsoft.Extensions.CommandLineUtils.CommandLineApplication,System.Func{NuGet.Common.ILogger},System.Func{NuGet.CommandLine.XPlat.IPackageReferenceCommandRunner})")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'Task<int> AddPackageReferenceCommandRunner.ExecuteCommand(PackageReferenceArgs packageReferenceArgs, MSBuildAPIUtility msBuild)', validate parameter 'msBuild' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.AddPackageReferenceCommandRunner.ExecuteCommand(NuGet.CommandLine.XPlat.PackageReferenceArgs,NuGet.CommandLine.XPlat.MSBuildAPIUtility)~System.Threading.Tasks.Task{System.Int32}")]
[assembly: SuppressMessage("Build", "CA1308:In method 'LogInternal', replace the call to 'ToLowerInvariant' with 'ToUpperInvariant'.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.CommandOutputLogger.LogInternal(NuGet.Common.LogLevel,System.String)")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'void CommandOutputLogger.LogInternal(LogLevel logLevel, string message)', validate parameter 'message' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.CommandOutputLogger.LogInternal(NuGet.Common.LogLevel,System.String)")]
[assembly: SuppressMessage("Build", "CA1307:The behavior of 'string.IndexOf(char)' could vary based on the current user's locale settings. Replace this call in 'NuGet.CommandLine.XPlat.CommandOutputLogger.LogInternal(NuGet.Common.LogLevel, string)' with a call to 'string.IndexOf(char, System.StringComparison)'.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.CommandOutputLogger.LogInternal(NuGet.Common.LogLevel,System.String)")]
[assembly: SuppressMessage("Build", "CA1822:Member AddPackagesToDict does not access instance data and can be marked as static (Shared in VisualBasic)", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.ListPackageCommandRunner.AddPackagesToDict(System.Collections.Generic.IEnumerable{NuGet.CommandLine.XPlat.FrameworkPackages},System.Collections.Generic.Dictionary{System.String,System.Collections.Generic.IList{NuGet.Protocol.Core.Types.IPackageSearchMetadata}})")]
[assembly: SuppressMessage("Build", "CA1822:Member GetUpdateLevel does not access instance data and can be marked as static (Shared in VisualBasic)", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.ListPackageCommandRunner.GetUpdateLevel(NuGet.Versioning.NuGetVersion,NuGet.Versioning.NuGetVersion)~NuGet.CommandLine.XPlat.UpdateLevel")]
[assembly: SuppressMessage("Build", "CA1822:Member MeetsConstraints does not access instance data and can be marked as static (Shared in VisualBasic)", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.ListPackageCommandRunner.MeetsConstraints(NuGet.Versioning.NuGetVersion,NuGet.CommandLine.XPlat.InstalledPackageReference,NuGet.CommandLine.XPlat.ListPackageArgs)~System.Boolean")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'void MSBuildAPIUtility.AddPackageReferencePerTFM(string projectPath, LibraryDependency libraryDependency, IEnumerable<string> frameworks)', validate parameter 'libraryDependency' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.MSBuildAPIUtility.AddPackageReferencePerTFM(System.String,NuGet.LibraryModel.LibraryDependency,System.Collections.Generic.IEnumerable{System.String})")]
[assembly: SuppressMessage("Build", "CA1307:The behavior of 'string.Equals(string)' could vary based on the current user's locale settings. Replace this call in 'NuGet.CommandLine.XPlat.MSBuildAPIUtility.GetPackageReferencesFromTargets(string, string)' with a call to 'string.Equals(string, System.StringComparison)'.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.MSBuildAPIUtility.GetPackageReferencesFromTargets(System.String,System.String)~System.Collections.Generic.IEnumerable{NuGet.CommandLine.XPlat.InstalledPackageReference}")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'int MSBuildAPIUtility.RemovePackageReference(string projectPath, LibraryDependency libraryDependency)', validate parameter 'libraryDependency' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.MSBuildAPIUtility.RemovePackageReference(System.String,NuGet.LibraryModel.LibraryDependency)~System.Int32")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'int Program.MainInternal(string[] args, CommandOutputLogger log)', validate parameter 'log' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.Program.MainInternal(System.String[],NuGet.CommandLine.XPlat.CommandOutputLogger)~System.Int32")]
[assembly: SuppressMessage("Build", "CA1308:In method 'MainInternal', replace the call to 'ToLowerInvariant' with 'ToUpperInvariant'.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.Program.MainInternal(System.String[],NuGet.CommandLine.XPlat.CommandOutputLogger)~System.Int32")]
[assembly: SuppressMessage("Build", "CA1031:Modify 'MainInternal' to catch a more specific allowed exception type, or rethrow the exception.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.Program.MainInternal(System.String[],NuGet.CommandLine.XPlat.CommandOutputLogger)~System.Int32")]
[assembly: SuppressMessage("Build", "CA1303:Method 'int Program.MainInternal(string[] args, CommandOutputLogger log)' passes a literal string as parameter 'value' of a call to 'void Console.WriteLine(string value)'. Retrieve the following string(s) from a resource table instead: \"Waiting for debugger to attach.\".", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.Program.MainInternal(System.String[],NuGet.CommandLine.XPlat.CommandOutputLogger)~System.Int32")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'void RemovePackageReferenceCommand.Register(CommandLineApplication app, Func<ILogger> getLogger, Func<IPackageReferenceCommandRunner> getCommandRunner)', validate parameter 'app' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.RemovePackageReferenceCommand.Register(Microsoft.Extensions.CommandLineUtils.CommandLineApplication,System.Func{NuGet.Common.ILogger},System.Func{NuGet.CommandLine.XPlat.IPackageReferenceCommandRunner})")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'Task<int> RemovePackageReferenceCommandRunner.ExecuteCommand(PackageReferenceArgs packageReferenceArgs, MSBuildAPIUtility msBuild)', validate parameter 'msBuild' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.CommandLine.XPlat.RemovePackageReferenceCommandRunner.ExecuteCommand(NuGet.CommandLine.XPlat.PackageReferenceArgs,NuGet.CommandLine.XPlat.MSBuildAPIUtility)~System.Threading.Tasks.Task{System.Int32}")]
[assembly: SuppressMessage("Build", "CA1819:Properties should not return arrays", Justification = "<Pending>", Scope = "member", Target = "~P:NuGet.CommandLine.XPlat.PackageReferenceArgs.Frameworks")]
[assembly: SuppressMessage("Build", "CA1819:Properties should not return arrays", Justification = "<Pending>", Scope = "member", Target = "~P:NuGet.CommandLine.XPlat.PackageReferenceArgs.Sources")]
