﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Emando.Vantage.Components.Sync.Properties {
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Emando.Vantage.Components.Sync.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Deleting items....
        /// </summary>
        internal static string DeletingItems {
            get {
                return ResourceManager.GetString("DeletingItems", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fetching items from source {0} ({1}) and target {2} ({3})....
        /// </summary>
        internal static string FetchingItems {
            get {
                return ResourceManager.GetString("FetchingItems", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Inserting items....
        /// </summary>
        internal static string InsertingItems {
            get {
                return ResourceManager.GetString("InsertingItems", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fetched {0} remote items and {1} local items..
        /// </summary>
        internal static string ItemsFetched {
            get {
                return ResourceManager.GetString("ItemsFetched", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Updating items....
        /// </summary>
        internal static string UpdatingItems {
            get {
                return ResourceManager.GetString("UpdatingItems", resourceCulture);
            }
        }
    }
}
