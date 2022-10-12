// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;

namespace NuGet.Common
{
    /// <summary>
    /// Represents an implementation of <see cref="EqualityComparer{T}" /> that compares <see cref="FileSystemInfo" /> objects by the value of their <see cref="FileSystemInfo.FullName" /> property based on the current operating system's case sensitivity.
    /// </summary>
    public sealed class FileSystemInfoFullNameEqualityComparer : EqualityComparer<FileSystemInfo>
    {
        /// <summary>
        /// Gets a static singleton for the <see cref="FileSystemInfoFullNameEqualityComparer" /> class.
        /// </summary>
        public static readonly FileSystemInfoFullNameEqualityComparer Instance = new FileSystemInfoFullNameEqualityComparer();

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemInfoFullNameEqualityComparer" /> class.
        /// </summary>
        private FileSystemInfoFullNameEqualityComparer()
        {
        }

        /// <summary>
        /// Determines if the two <see cref="FileSystemInfo" /> objects are equal by comparing their <see cref="FileSystemInfo.FullName" /> properties based on the current operating system's case sensitivity.
        /// </summary>
        /// <param name="x">The first <see cref="FileSystemInfo" /> to compare.</param>
        /// <param name="y">The second <see cref="FileSystemInfo" /> to compare.</param>
        /// <returns><c>true</c> if the two <see cref="FileSystemInfo" /> objects' <see cref="FileSystemInfo.FullName" /> properties are equal, otherwise <c>false</c>.</returns>
        public override bool Equals(FileSystemInfo x, FileSystemInfo y)
        {
            return PathUtility.GetStringComparerBasedOnOS().Equals(x.FullName, y.FullName);
        }

        /// <inheritdoc cref="IEqualityComparer{T}.GetHashCode(T)" />
        public override int GetHashCode(FileSystemInfo obj)
        {
#if NETFRAMEWORK || NETSTANDARD
            return obj.FullName.GetHashCode();
#else
            return obj.FullName.GetHashCode(StringComparison.Ordinal);
#endif
        }
    }
}
