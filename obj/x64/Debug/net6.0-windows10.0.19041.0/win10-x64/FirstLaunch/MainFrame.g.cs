﻿#pragma checksum "E:\sks_toolkit\sks_toolkit\FirstLaunch\MainFrame.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A43391431D03260F5095BBF6842082C186F54CB68B99F5F4521F00158EF0115A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sks_toolkit.FirstLaunch
{
    partial class MainFrame : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2306")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // FirstLaunch\MainFrame.xaml line 12
                {
                    this.MainFrameGrid = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Grid>(target);
                }
                break;
            case 3: // FirstLaunch\MainFrame.xaml line 35
                {
                    this.StartSet = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Grid>(target);
                }
                break;
            case 4: // FirstLaunch\MainFrame.xaml line 51
                {
                    this.LanguageSetting = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Grid>(target);
                }
                break;
            case 5: // FirstLaunch\MainFrame.xaml line 61
                {
                    this.NextStepButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.NextStepButton).Click += this.NextStepButton_Click;
                }
                break;
            case 6: // FirstLaunch\MainFrame.xaml line 80
                {
                    this.BackButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                }
                break;
            case 7: // FirstLaunch\MainFrame.xaml line 94
                {
                    this.FinishButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                }
                break;
            case 8: // FirstLaunch\MainFrame.xaml line 103
                {
                    this.FinishWord = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 9: // FirstLaunch\MainFrame.xaml line 89
                {
                    this.BackWord = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 10: // FirstLaunch\MainFrame.xaml line 70
                {
                    this.NextStepWord = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 11: // FirstLaunch\MainFrame.xaml line 58
                {
                    this.IsRightLanguage = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 12: // FirstLaunch\MainFrame.xaml line 42
                {
                    this.FirstGreeting = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 13: // FirstLaunch\MainFrame.xaml line 43
                {
                    this.ToStartSet = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 14: // FirstLaunch\MainFrame.xaml line 44
                {
                    this.HowToStart = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 15: // FirstLaunch\MainFrame.xaml line 31
                {
                    this.WelcomeWord = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 3.0.0.2306")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

