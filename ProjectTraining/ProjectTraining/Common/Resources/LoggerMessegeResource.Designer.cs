﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectTraining.Common.Resources {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class LoggerMessegeResource {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LoggerMessegeResource() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("ProjectTraining.Common.Resources.LoggerMessegeResource", typeof(LoggerMessegeResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        public static string Error400 {
            get {
                return ResourceManager.GetString("Error400", resourceCulture);
            }
        }
        
        public static string Error401 {
            get {
                return ResourceManager.GetString("Error401", resourceCulture);
            }
        }
        
        public static string Error404 {
            get {
                return ResourceManager.GetString("Error404", resourceCulture);
            }
        }
        
        public static string Error500 {
            get {
                return ResourceManager.GetString("Error500", resourceCulture);
            }
        }
        
        public static string SomethingWentWrong {
            get {
                return ResourceManager.GetString("SomethingWentWrong", resourceCulture);
            }
        }
        
        public static string OnActionExecuting {
            get {
                return ResourceManager.GetString("OnActionExecuting", resourceCulture);
            }
        }
        
        public static string ErrorSeedingTheDatabase {
            get {
                return ResourceManager.GetString("ErrorSeedingTheDatabase", resourceCulture);
            }
        }
    }
}