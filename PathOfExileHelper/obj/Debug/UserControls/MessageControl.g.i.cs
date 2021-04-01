﻿#pragma checksum "..\..\..\UserControls\MessageControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1F5DC086573B7476484F344733562EDC421A4517CFDAB242D795C1E561840F16"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PathOfExileHelper.UserControls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PathOfExileHelper.UserControls {
    
    
    /// <summary>
    /// MessageControl
    /// </summary>
    public partial class MessageControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 56 "..\..\..\UserControls\MessageControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtUser;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\UserControls\MessageControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtMessage;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\UserControls\MessageControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtTime;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\UserControls\MessageControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAskParty;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\UserControls\MessageControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnWhisper;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\UserControls\MessageControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCustom1;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\UserControls\MessageControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCustom2;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\UserControls\MessageControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCustom3;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\UserControls\MessageControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRemoveItem;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PathOfExileHelper;component/usercontrols/messagecontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\MessageControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtUser = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.txtMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.txtTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.btnAskParty = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\UserControls\MessageControl.xaml"
            this.btnAskParty.Click += new System.Windows.RoutedEventHandler(this.ClickAskForParty);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnWhisper = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\..\UserControls\MessageControl.xaml"
            this.btnWhisper.Click += new System.Windows.RoutedEventHandler(this.ClickWhisper);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnCustom1 = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\..\UserControls\MessageControl.xaml"
            this.btnCustom1.Click += new System.Windows.RoutedEventHandler(this.ClickCustomWhisper1);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnCustom2 = ((System.Windows.Controls.Button)(target));
            
            #line 75 "..\..\..\UserControls\MessageControl.xaml"
            this.btnCustom2.Click += new System.Windows.RoutedEventHandler(this.ClickCustomWhisper2);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnCustom3 = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\..\UserControls\MessageControl.xaml"
            this.btnCustom3.Click += new System.Windows.RoutedEventHandler(this.ClickCustomWhisper3);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnRemoveItem = ((System.Windows.Controls.Button)(target));
            
            #line 81 "..\..\..\UserControls\MessageControl.xaml"
            this.btnRemoveItem.Click += new System.Windows.RoutedEventHandler(this.ClickRemoveItem);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

