// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.IO;
using NuGet.Common;

namespace NuGet.Configuration
{
    /// <summary>
    /// Represents a cache context when loading <see cref="SettingsFile" /> objects so that they are only read once or if they change on disk.
    /// </summary>
    public sealed class SettingsLoadingContext : IDisposable
    {
        /// <summary>
        /// A thread-safe cache for files based on their full path and last write time.
        /// </summary>
        private readonly ConcurrentDictionary<FileInfo, (DateTime LastWriteTime, Lazy<SettingsFile> Lazy)> _fileCache = new ConcurrentDictionary<FileInfo, (DateTime, Lazy<SettingsFile>)>(FileSystemInfoFullNameEqualityComparer.Instance);

        private bool _isDisposed;

        /// <summary>
        /// Occurs when a file is read.
        /// </summary>
        internal event EventHandler<string> FileRead;

        public void Dispose()
        {
            Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets or creates a settings file for the specified path.
        /// </summary>
        /// <param name="filePath">The file path to create a <see cref="SettingsFile" /> object for.</param>
        /// <param name="isMachineWide">An optional value indicating whether or not the settings file is machine-wide.</param>
        /// <param name="isReadOnly">An optional value indicating whether or not the settings file is read-only.</param>
        /// <returns></returns>
        /// <exception cref="ObjectDisposedException">When the current object has been disposed.</exception>
        /// <exception cref="ArgumentNullException">When <paramref name="filePath" /> is <c>null</c>.</exception>
        internal SettingsFile GetOrCreateSettingsFile(string filePath, bool isMachineWide = false, bool isReadOnly = false)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(SettingsLoadingContext));
            }

            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            var fileInfo = new FileInfo(filePath);

            // Add a new file to the cache if it doesn't exist.  If the file is already in the cache, read it again if the file has changed
            (DateTime _, Lazy<SettingsFile> Lazy) = _fileCache.AddOrUpdate(
                fileInfo,
                key => (key.LastWriteTime, new Lazy<SettingsFile>(() => LoadSettingsFile(key, isMachineWide, isReadOnly))),
                (key, existingItem) =>
                {
                    if (existingItem.LastWriteTime < key.LastWriteTime)
                    {
                        return (key.LastWriteTime, new Lazy<SettingsFile>(() => LoadSettingsFile(key, isMachineWide, isReadOnly)));
                    }

                    return existingItem;
                });

            SettingsFile settingsFile = Lazy.Value;

            return settingsFile;

            SettingsFile LoadSettingsFile(FileInfo fileInfo, bool isMachineWide, bool isReadOnly)
            {
                FileRead?.Invoke(this, filePath);

                return new SettingsFile(fileInfo.DirectoryName, fileInfo.Name, isMachineWide, isReadOnly);
            }
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _fileCache.Clear();
                }

                _isDisposed = true;
            }
        }
    }
}
