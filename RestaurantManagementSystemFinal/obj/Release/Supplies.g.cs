﻿#pragma checksum "..\..\Supplies.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "058947822FF34769170985617DA10FA2CD103050"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using RestaurantManagementSystemFinal;
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


namespace RestaurantManagementSystemFinal {
    
    
    /// <summary>
    /// Supplies
    /// </summary>
    public partial class Supplies : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\Supplies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox suppliername;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Supplies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox suppliercompany;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Supplies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView supplist;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\Supplies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox suppliercity;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Supplies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox suppliercontact;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\Supplies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox sup_name;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\Supplies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox sup_price;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\Supplies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox quantity;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\Supplies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox suppliercombo;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\Supplies.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox suppliescombo;
        
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
            System.Uri resourceLocater = new System.Uri("/RestaurantManagementSystemFinal;component/supplies.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Supplies.xaml"
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
            this.suppliername = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.suppliercompany = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.supplist = ((System.Windows.Controls.ListView)(target));
            
            #line 20 "..\..\Supplies.xaml"
            this.supplist.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.supplist_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 30 "..\..\Supplies.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 5:
            this.suppliercity = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.suppliercontact = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.sup_name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.sup_price = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            
            #line 41 "..\..\Supplies.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.quantity = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            
            #line 44 "..\..\Supplies.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 12:
            this.suppliercombo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 13:
            this.suppliescombo = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

