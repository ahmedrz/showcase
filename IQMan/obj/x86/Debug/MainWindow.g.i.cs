﻿#pragma checksum "..\..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C3280554A0759873603EC6628C78EF2C66AD09E09D3AD54105877AA825822204"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Animation;
using Telerik.Windows.Controls.Behaviors;
using Telerik.Windows.Controls.Carousel;
using Telerik.Windows.Controls.Data.PropertyGrid;
using Telerik.Windows.Controls.DragDrop;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.LayoutControl;
using Telerik.Windows.Controls.Legend;
using Telerik.Windows.Controls.MultiColumnComboBox;
using Telerik.Windows.Controls.Primitives;
using Telerik.Windows.Controls.RadialMenu;
using Telerik.Windows.Controls.TransitionEffects;
using Telerik.Windows.Controls.TreeListView;
using Telerik.Windows.Controls.TreeView;
using Telerik.Windows.Controls.Wizard;
using Telerik.Windows.Data;
using Telerik.Windows.DragDrop;
using Telerik.Windows.DragDrop.Behaviors;
using Telerik.Windows.Input.Touch;
using Telerik.Windows.Shapes;


namespace IQMan {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 115 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadAutoCompleteBox cmbLine;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadAutoCompleteBox txtVessel;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadAutoCompleteBox txtOriginPort;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadGridView bolGrid;
        
        #line default
        #line hidden
        
        
        #line 155 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadContextMenu GridContextMenu;
        
        #line default
        #line hidden
        
        
        #line 224 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadDataPager radDataPager;
        
        #line default
        #line hidden
        
        
        #line 230 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadButton btnSave;
        
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
            System.Uri resourceLocater = new System.Uri("/IQMan;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
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
            
            #line 2 "..\..\..\MainWindow.xaml"
            ((IQMan.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 6 "..\..\..\MainWindow.xaml"
            ((IQMan.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cmbLine = ((Telerik.Windows.Controls.RadAutoCompleteBox)(target));
            return;
            case 5:
            this.txtVessel = ((Telerik.Windows.Controls.RadAutoCompleteBox)(target));
            
            #line 119 "..\..\..\MainWindow.xaml"
            this.txtVessel.SearchTextChanged += new System.EventHandler(this.txtVessel_SearchTextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtOriginPort = ((Telerik.Windows.Controls.RadAutoCompleteBox)(target));
            
            #line 126 "..\..\..\MainWindow.xaml"
            this.txtOriginPort.SearchTextChanged += new System.EventHandler(this.txtOrigiinPort_SearchTextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.bolGrid = ((Telerik.Windows.Controls.RadGridView)(target));
            return;
            case 8:
            this.GridContextMenu = ((Telerik.Windows.Controls.RadContextMenu)(target));
            
            #line 157 "..\..\..\MainWindow.xaml"
            this.GridContextMenu.Opened += new System.Windows.RoutedEventHandler(this.GridContextMenu_Opened);
            
            #line default
            #line hidden
            
            #line 158 "..\..\..\MainWindow.xaml"
            this.GridContextMenu.ItemClick += new Telerik.Windows.RadRoutedEventHandler(this.GridContextMenu_ItemClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.radDataPager = ((Telerik.Windows.Controls.RadDataPager)(target));
            return;
            case 10:
            
            #line 229 "..\..\..\MainWindow.xaml"
            ((Telerik.Windows.Controls.RadButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RadButton_Click_1);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnSave = ((Telerik.Windows.Controls.RadButton)(target));
            
            #line 230 "..\..\..\MainWindow.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 2:
            
            #line 29 "..\..\..\MainWindow.xaml"
            ((Telerik.Windows.Controls.RadContextMenu)(target)).Opened += new System.Windows.RoutedEventHandler(this.GridVehicleContextMenu_Opened);
            
            #line default
            #line hidden
            
            #line 30 "..\..\..\MainWindow.xaml"
            ((Telerik.Windows.Controls.RadContextMenu)(target)).ItemClick += new Telerik.Windows.RadRoutedEventHandler(this.vehicleMenu_ItemClick);
            
            #line default
            #line hidden
            break;
            case 3:
            
            #line 71 "..\..\..\MainWindow.xaml"
            ((Telerik.Windows.Controls.RadContextMenu)(target)).Opened += new System.Windows.RoutedEventHandler(this.GridConsignmentContextMenu_Opened);
            
            #line default
            #line hidden
            
            #line 72 "..\..\..\MainWindow.xaml"
            ((Telerik.Windows.Controls.RadContextMenu)(target)).ItemClick += new Telerik.Windows.RadRoutedEventHandler(this.consignmentMenu_ItemClick);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

