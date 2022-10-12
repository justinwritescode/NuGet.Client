// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NuGet.Test.Utility;
using Xunit;

namespace NuGet.Configuration.Test
{
    public class SettingsLoadingContextTests
    {
        /// <summary>
        /// Verifies that <see cref="SettingsLoadingContext.GetOrCreateSettingsFile(string, bool, bool)" /> can read a settings file.
        /// </summary>
        [Fact]
        public void GetOrCreateSettingsFile_LoadsFile_WhenExists()
        {
            using var testPathContext = new SimpleTestPathContext();
            using var settingsLoadingContext = new SettingsLoadingContext();

            string filePathThatWasRead = null;

            settingsLoadingContext.FileRead += (_, filePath) => filePathThatWasRead = filePath;

            SettingsFile settingsFile = settingsLoadingContext.GetOrCreateSettingsFile(testPathContext.NuGetConfig);

            settingsFile.ConfigFilePath.Should().Be(testPathContext.NuGetConfig);

            filePathThatWasRead.Should().NotBeNull();
            filePathThatWasRead.Should().Be(testPathContext.NuGetConfig);
        }

        /// <summary>
        /// Verifies that <see cref="SettingsLoadingContext.GetOrCreateSettingsFile(string, bool, bool)" /> only reads a settings file once and returns a cached instance for the same file path.
        /// </summary>
        [Fact]
        public void GetOrCreateSettingsFile_OnlyReadsFileOnce_WhenConfigFileDoesNotChange()
        {
            using var testPathContext = new SimpleTestPathContext();
            using var settingsLoadingContext = new SettingsLoadingContext();

            List<string> filePathsThatWereRead = new List<string>();

            settingsLoadingContext.FileRead += (_, filePath) => filePathsThatWereRead.Add(filePath);

            SettingsFile settingsFile1 = settingsLoadingContext.GetOrCreateSettingsFile(testPathContext.NuGetConfig);

            settingsFile1.ConfigFilePath.Should().Be(testPathContext.NuGetConfig);

            SettingsFile settingsFile2 = settingsLoadingContext.GetOrCreateSettingsFile(testPathContext.NuGetConfig);

            settingsFile2.ConfigFilePath.Should().Be(testPathContext.NuGetConfig);

            filePathsThatWereRead.Should().ContainSingle();
        }

        /// <summary>
        /// Verifies that <see cref="SettingsLoadingContext.GetOrCreateSettingsFile(string, bool, bool)" /> reads a settings file when it changes on disk.
        /// </summary>
        [Fact]
        public void GetOrCreateSettingsFile_ReloadsFile_WhenConfigFileChanges()
        {
            using var testPathContext = new SimpleTestPathContext();
            using var settingsLoadingContext = new SettingsLoadingContext();

            List<string> filePathsThatWereRead = new List<string>();

            settingsLoadingContext.FileRead += (_, filePath) => filePathsThatWereRead.Add(filePath);

            SettingsFile settingsFile1 = settingsLoadingContext.GetOrCreateSettingsFile(testPathContext.NuGetConfig);

            settingsFile1.ConfigFilePath.Should().Be(testPathContext.NuGetConfig);

            SettingsFile settingsFile2 = settingsLoadingContext.GetOrCreateSettingsFile(testPathContext.NuGetConfig);

            settingsFile2.ConfigFilePath.Should().Be(testPathContext.NuGetConfig);

            filePathsThatWereRead.Should().ContainSingle();

            settingsFile2.AddOrUpdate("config", new AddItem("key1", "value1"));

            settingsFile2.SaveToDisk();

            SettingsFile settingsFile3 = settingsLoadingContext.GetOrCreateSettingsFile(testPathContext.NuGetConfig);

            settingsFile3.ConfigFilePath.Should().Be(testPathContext.NuGetConfig);

            filePathsThatWereRead.Count.Should().Be(2);
        }

        /// <summary>
        /// Verifies that <see cref="SettingsLoadingContext.GetOrCreateSettingsFile(string, bool, bool)" /> throws an <see cref="NuGetConfigurationException"/> when a settings file is unreadable.
        /// </summary>
        [Fact]
        public void GetOrCreateSettingsFile_ThrowNuGetConfigurationException_WhenConfigIsUnreadable()
        {
            using var testPathContext = new SimpleTestPathContext();

            using var settingsLoadingContext = new SettingsLoadingContext();

            File.WriteAllText(testPathContext.NuGetConfig, string.Empty);

            Action action = () => _ = settingsLoadingContext.GetOrCreateSettingsFile(testPathContext.NuGetConfig);

            action.Should()
                .Throw<NuGetConfigurationException>()
                .Which
                .Message
                .Should()
                .Contain("NuGet.Config is not valid XML");
        }

        /// <summary>
        /// Verifies that <see cref="SettingsLoadingContext.GetOrCreateSettingsFile(string, bool, bool)" /> throws an <see cref="ArgumentNullException"/> when passed a <c>null</c> value for the file path.
        /// </summary>
        [Fact]
        public void GetOrCreateSettingsFile_ThrowsArgumentNullException_WhenFilePathIsNull()
        {
            var settingsLoadingContext = new SettingsLoadingContext();

            Action action = () => settingsLoadingContext.GetOrCreateSettingsFile(filePath: null);

            action.Should()
                .Throw<ArgumentNullException>()
                .Which
                .ParamName
                .Should()
                .Be("filePath");
        }

        /// <summary>
        /// Verifies that <see cref="SettingsLoadingContext.GetOrCreateSettingsFile(string, bool, bool)" /> throws an <see cref="ObjectDisposedException"/> when it has been disposed.
        /// </summary>
        [Fact]
        public void GetOrCreateSettingsFile_ThrowsObjectDisposedException_WhenDisposed()
        {
            var settingsLoadingContext = new SettingsLoadingContext();

            settingsLoadingContext.Dispose();

            Action action = () => settingsLoadingContext.GetOrCreateSettingsFile(filePath: null);

            action.Should()
                .Throw<ObjectDisposedException>()
                .Which
                .ObjectName
                .Should()
                .Be(nameof(SettingsLoadingContext));
        }
    }
}
