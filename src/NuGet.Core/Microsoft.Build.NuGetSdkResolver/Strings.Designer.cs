﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.Build.NuGetSdkResolver {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microsoft.Build.NuGetSdkResolver.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to resolve SDK &apos;{0}&apos;. A package was successfully restored but the package could not be located..
        /// </summary>
        internal static string CouldNotFindInstalledPackage {
            get {
                return ResourceManager.GetString("CouldNotFindInstalledPackage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Downloading SDK package &quot;{0}&quot; version &quot;{1}&quot;....
        /// </summary>
        internal static string DownloadingPackage {
            get {
                return ResourceManager.GetString("DownloadingPackage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to determine path to global.json from path &quot;{0}&quot;. {1}.
        /// </summary>
        internal static string FailedToFindPathToGlobalJson {
            get {
                return ResourceManager.GetString("FailedToFindPathToGlobalJson", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to parse &quot;{0}&quot;. {1}.
        /// </summary>
        internal static string FailedToParseGlobalJson {
            get {
                return ResourceManager.GetString("FailedToParseGlobalJson", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to read file &quot;{0}&quot;. {1}.
        /// </summary>
        internal static string FailedToReadGlobalJson {
            get {
                return ResourceManager.GetString("FailedToReadGlobalJson", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Locating MSBuild project SDK &quot;{0}&quot; version &quot;{1}&quot;....
        /// </summary>
        internal static string LocatingSdk {
            get {
                return ResourceManager.GetString("LocatingSdk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The NuGet-based MSBuild project SDK resolver did not resolve the SDK &quot;{0}&quot; because there was no version specified in the project or global.json..
        /// </summary>
        internal static string NoSdkVersionSpecified {
            get {
                return ResourceManager.GetString("NoSdkVersionSpecified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The NuGet-based MSBuild project SDK resolver did not resolve the SDK &quot;{0}&quot; because there was no version specified the file &quot;{1}&quot;..
        /// </summary>
        internal static string NoSdkVersionSpecifiedInGlobalJson {
            get {
                return ResourceManager.GetString("NoSdkVersionSpecifiedInGlobalJson", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to resolve SDK &apos;{0}&apos;. Package restore was successful but a package with the ID of &quot;{1}&quot; was not installed..
        /// </summary>
        internal static string PackageWasNotInstalled {
            get {
                return ResourceManager.GetString("PackageWasNotInstalled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The NuGet-based MSBuild project SDK resolver did not resolve SDK &quot;{0}&quot; because the resolver is disabled by the {1} environment variable..
        /// </summary>
        internal static string SdkResolverIsDisabled {
            get {
                return ResourceManager.GetString("SdkResolverIsDisabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The NuGet-based MSBuild project SDK resolver did not resolve SDK &quot;{0}&quot; because the version specified &quot;{1}&quot; is not a valid NuGet version..
        /// </summary>
        internal static string SdkVersionIsNotValidNuGetVersion {
            get {
                return ResourceManager.GetString("SdkVersionIsNotValidNuGetVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Successfully downloaded SDK package &quot;{0}&quot; version &quot;{1}&quot; to &quot;{2}&quot;..
        /// </summary>
        internal static string SuccessfullyDownloadedPackage {
            get {
                return ResourceManager.GetString("SuccessfullyDownloadedPackage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unhandled exception in NuGet-based MSBuild project SDK resolver..
        /// </summary>
        internal static string UnhandledException {
            get {
                return ResourceManager.GetString("UnhandledException", resourceCulture);
            }
        }
    }
}
