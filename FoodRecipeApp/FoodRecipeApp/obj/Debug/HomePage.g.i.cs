﻿#pragma checksum "..\..\HomePage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A134B6EB52F78D05B3561EDBCDEED4ADCEA95DFB3AB287094892F971E6DDEACB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FoodRecipeApp;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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


namespace FoodRecipeApp {
    
    
    /// <summary>
    /// HomePage
    /// </summary>
    public partial class HomePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 49 "..\..\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button searchButton;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock keywordPlaceholderTextBlock;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox keywordTextBox;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton favoriteToggle;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox filter;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox paging;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock viewTotalPages;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView dishesView;
        
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
            System.Uri resourceLocater = new System.Uri("/FoodRecipeApp;component/homepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\HomePage.xaml"
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
            
            #line 13 "..\..\HomePage.xaml"
            ((FoodRecipeApp.HomePage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.searchButton = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\HomePage.xaml"
            this.searchButton.Click += new System.Windows.RoutedEventHandler(this.searchButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.keywordPlaceholderTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.keywordTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 60 "..\..\HomePage.xaml"
            this.keywordTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.TextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 60 "..\..\HomePage.xaml"
            this.keywordTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.TextBox_LostFocus);
            
            #line default
            #line hidden
            
            #line 60 "..\..\HomePage.xaml"
            this.keywordTextBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.keywordTextBox_KeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.favoriteToggle = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 62 "..\..\HomePage.xaml"
            this.favoriteToggle.Click += new System.Windows.RoutedEventHandler(this.favoriteToggle_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.filter = ((System.Windows.Controls.ComboBox)(target));
            
            #line 70 "..\..\HomePage.xaml"
            this.filter.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.filter_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 86 "..\..\HomePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.previousButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.paging = ((System.Windows.Controls.ComboBox)(target));
            
            #line 108 "..\..\HomePage.xaml"
            this.paging.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.paging_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.viewTotalPages = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            
            #line 120 "..\..\HomePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.nextButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.dishesView = ((System.Windows.Controls.ListView)(target));
            
            #line 152 "..\..\HomePage.xaml"
            this.dishesView.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.dishesListView_PreviewMouseLeftButtonUp);
            
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
            case 12:
            
            #line 161 "..\..\HomePage.xaml"
            ((System.Windows.Controls.Grid)(target)).PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.favButton_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            break;
            case 13:
            
            #line 169 "..\..\HomePage.xaml"
            ((System.Windows.Controls.Grid)(target)).PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.dishView_PreviewMouseLeftButtonUp);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

