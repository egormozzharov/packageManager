﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PackageManager.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class CommonResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CommonResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PackageManager.Resources.CommonResources", typeof(CommonResources).Assembly);
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
        ///   Looks up a localized string similar to In the configuration file has an unknown variable: \n.
        /// </summary>
        internal static string MsgConfigFileHasUnknownVariable {
            get {
                return ResourceManager.GetString("MsgConfigFileHasUnknownVariable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Configuration a file is missing.
        /// </summary>
        internal static string MsgConfigFileMissing {
            get {
                return ResourceManager.GetString("MsgConfigFileMissing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No modules to install.
        /// </summary>
        internal static string MsgInstallationModuleNotExistInConfigFile {
            get {
                return ResourceManager.GetString("MsgInstallationModuleNotExistInConfigFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Package is empty.
        /// </summary>
        internal static string MsgPackageEmpty {
            get {
                return ResourceManager.GetString("MsgPackageEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; is not exists in configuration file.
        /// </summary>
        internal static string MsgPropertyNotExistInConfigFile {
            get {
                return ResourceManager.GetString("MsgPropertyNotExistInConfigFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; is not exists in configuration file for this module.
        /// </summary>
        internal static string MsgPropertyNotExistInInstallationModule {
            get {
                return ResourceManager.GetString("MsgPropertyNotExistInInstallationModule", resourceCulture);
            }
        }
    }
}
