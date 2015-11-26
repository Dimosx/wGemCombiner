﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WGemCombiner.Resources {
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
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WGemCombiner.Resources.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to Paste gem combining equations or a parenthesis formula into the text box, then click &apos;Get Instructions&apos;.
        ///
        ///Set the &apos;delay&apos; to at LEAST as many milliseconds as a frame on GC2 is taking. I recommend at least 45-50 for no lag (it will usually work with much lower, but going any lower than the frame time will not actually speed up the process, as the game will only do one step per frame).
        ///
        ///Game gem info tooltips will be hidden and reshown after the combine (&quot;.&quot;), be sure to have them activated before combini [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string HelpCombinerMessage {
            get {
                return ResourceManager.GetString("HelpCombinerMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Working with the combiner.
        /// </summary>
        internal static string HelpCombinerTitle {
            get {
                return ResourceManager.GetString("HelpCombinerTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Gem Combiner was made by Suuper.
        ///
        ///Contributors:
        ///- 12345ieee (some spec parsing, preset recipes)
        ///- CooLTanG (Automate the &apos;Get Instructions&apos; &amp; &apos;Combine&apos; steps, fix window size/resolution issues, customizable hotkey)
        ///- Hellrage (some GUI tweaks, skin, small bug fixes, preset and resource management)
        ///- RobinHood70 (code cleaning and revamping; partial localization support)
        ///
        ///Release version: {0}
        ///
        ///To report a problem, see
        ///https://github.com/gemforce-team/wGemCombiner#report-bugs
        ///
        ///If you wish to cont [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string HelpCreditsMessage {
            get {
                return ResourceManager.GetString("HelpCreditsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Credits.
        /// </summary>
        internal static string HelpCreditsTitle {
            get {
                return ResourceManager.GetString("HelpCreditsTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to In debug mode you can import new presets from a .txt file with recipes.
        ///Your txt must contain one recipe per line. The recipes have to be valid parenthesis formulas (not equations). The parser is somewhat robust but i wouldn&apos;t test it too hard :)
        ///To start importing, choose the &quot;Import...&quot; option from the Preset drop-down, then choose the .txt file with recipes..
        /// </summary>
        internal static string HelpImportingPresetsMessage {
            get {
                return ResourceManager.GetString("HelpImportingPresetsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Importing New Presets.
        /// </summary>
        internal static string HelpImportingPresetsTitle {
            get {
                return ResourceManager.GetString("HelpImportingPresetsTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Example of combining equations:
        ///(val = 1)\t0 = g1 orange
        ///(val = 2)\t1 = 0 + 0
        ///(val = 3)\t2 = 1 + 0
        ///Works if you have first gem as &apos;1&apos; and second as &apos;2&apos;, etc, as well.
        ///
        ///Example of parenthesis formula:
        ///(2+1)+1
        ///or
        ///(1+0)+0
        ///or
        ///(2m+m)+m
        ///If zeros are present they are treated as 1s and 1s as 2s..
        /// </summary>
        internal static string HelpInputFormatMessage {
            get {
                return ResourceManager.GetString("HelpInputFormatMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Input Format.
        /// </summary>
        internal static string HelpInputFormatTitle {
            get {
                return ResourceManager.GetString("HelpInputFormatTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Preset schemes are supported. To use one, first select a color + combine/spec from the top drop-down list.
        ///After selecting a color + combine/spec, the second drop-down list will display the preset options. First number is number of base gems, second number is the growth rate. Ones marked with a &apos;-&apos; are 2^n combines [over 8], and are NEVER the best for growth rate per cost (included for convenience if you want easily comparable gem costs). Selecting a preset will change the textbox&apos;s text. This text is used [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string HelpPresetsMessage {
            get {
                return ResourceManager.GetString("HelpPresetsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Presets.
        /// </summary>
        internal static string HelpPresetsTitle {
            get {
                return ResourceManager.GetString("HelpPresetsTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Gem Combiner now supports speccing.
        ///To spec, place base gems of different color in 1A, 1B, and 1C.
        ///
        ///Order of colors, starting at 1A, should be: orange/yellow, black, red.
        ///
        ///Speccing provides another way of squeezing red out of a gem. Simply select your mana/kill gem combine and replace one of the &apos;k&apos; or &apos;m&apos; with another (valid) color&apos;s letter. Be sure you have your two gems in the proper slots, though!
        ///
        ///EXAMPLE:
        ///Pick Mana Gem Spec
        ///Choose the default 8 pattern
        ///Put orange grade 1 in 1A(bottom right s [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string HelpSpeccingMessage {
            get {
                return ResourceManager.GetString("HelpSpeccingMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Speccing.
        /// </summary>
        internal static string HelpSpeccingTitle {
            get {
                return ResourceManager.GetString("HelpSpeccingTitle", resourceCulture);
            }
        }
    }
}