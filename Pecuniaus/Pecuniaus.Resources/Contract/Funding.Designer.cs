﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pecuniaus.Resources.Contract {
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
    public class Funding {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Funding() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Pecuniaus.Resources.Contract.Funding", typeof(Funding).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bank Account Name.
        /// </summary>
        public static string AccountName {
            get {
                return ResourceManager.GetString("AccountName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bank Account Number.
        /// </summary>
        public static string AccountNumber {
            get {
                return ResourceManager.GetString("AccountNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bank Name.
        /// </summary>
        public static string BankName {
            get {
                return ResourceManager.GetString("BankName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bank Name is required.
        /// </summary>
        public static string BankNameReq {
            get {
                return ResourceManager.GetString("BankNameReq", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Administrative Expenses.
        /// </summary>
        public static string ExpenseAmount {
            get {
                return ResourceManager.GetString("ExpenseAmount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Funding Complete*.
        /// </summary>
        public static string FundingComplete {
            get {
                return ResourceManager.GetString("FundingComplete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Funding Task.
        /// </summary>
        public static string FundingTask {
            get {
                return ResourceManager.GetString("FundingTask", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mca Amount.
        /// </summary>
        public static string McaAmount {
            get {
                return ResourceManager.GetString("McaAmount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Authorised Owner.
        /// </summary>
        public static string OwnerName {
            get {
                return ResourceManager.GetString("OwnerName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Send Request for Funding.
        /// </summary>
        public static string SendRequestforFunding {
            get {
                return ResourceManager.GetString("SendRequestforFunding", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Total Funding Amount.
        /// </summary>
        public static string TotalOwnedAount {
            get {
                return ResourceManager.GetString("TotalOwnedAount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Upload proof of wireTransfer.
        /// </summary>
        public static string WireTransfer {
            get {
                return ResourceManager.GetString("WireTransfer", resourceCulture);
            }
        }
    }
}
