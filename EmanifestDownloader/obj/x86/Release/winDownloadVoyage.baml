   M S B A M L   `   `   `  ���� e  aTelerik.Windows.Controls, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7eHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.ControlsTelerik.Windows.Controls   EmanifestDownloader+   %EmanifestDownloader.winDownloadVoyage   t pTelerik.Windows.Controls.DataServices60, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7w sTelerik.Windows.Controls.EntityFramework60, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation          R	 NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\
 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 	 
  btelerikControlsHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.Controls 	 
  R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	 
   @d2http://schemas.microsoft.com/expression/blend/2008 	 
   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	 
   -   +   %Telerik.Windows.Controls.StyleManager    Theme-   'Telerik.Windows.Controls.ThemeConverter$  Expression_Dark $��Download Voyage��   WindowBackground#���     ControlForeground_Normal#���  $	��350��$	��600��(  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�)  #Telerik.Windows.Controls.WindowBase   WindowState$ 	Maximized=�.���  � RowDefinitions �� $	��100 ��� $��* ��� $��35 ��� $��45 �.+�$   Telerik.Windows.Controls.Label  $��62��$��8,10,0,0q�$��Line :��$
��Left=�$	��Top=�$��23��*  $Telerik.Windows.Controls.RadComboBox  -     Name$ cmbLines��   AllowMultipleSelection . %   ClearSelectionButtonVisibility$ Visible=�"   ClearSelectionButtonContent$ Clear��$��	70,11,0,0q�$
��Left=�$	��Top=�$	��163��  $��62��$��8,37,0,0q�$��Dep. :��$
��Left=�$	��Top=�$��23��  -   $ cmbDeps�� �� SelectedValuePath$ Id��	 �� DisplayMemberPath$
	 Text��$ Visible=�$ Clear��$��	70,38,0,0q�$
��Left=�$	��Top=�$	��163��(   "Telerik.Windows.Controls.RadButton  -   
  Name$
 	btnchoose��$��Choose��$��60��$��30��$��	70,65,0,0q�$	��Top=�$
��Left=�*  $Telerik.Windows.Controls.RadGridView  -     Name$ bolGrid��$	��5,0q�$��1��;	  5Telerik.Windows.Controls.GridView.GridViewDataControl 	  
IsReadOnly .  	  CanUserInsertRows .   	  AutoGenerateColumns .   	  CanUserDeleteRows .   	  ShowGroupPanel .  *
  $Telerik.Windows.Controls.DataControl 
  ItemsSource ��  �� Path$ 
BolDetails� 	  Columns 1  +Telerik.Windows.Controls.GridViewDataColumn  6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ BillOfLadingNumber��   DataMemberBinding �� *BillOfLadingNumber+  $
 Line�� �� *BoxPartneringLineCode+  $ ConsigneeName�� �� *ConsigneeName+5  /Telerik.Windows.Controls.GridViewComboBoxColumn  $
 Dep.��-  'Telerik.Windows.Controls.GridViewColumn   
UniqueName$ 	depColumn��   SelectedValueMemberPath$ DepartmentId��   DisplayMemberPath$ DepartmentName�� �� *DepartmentId+ �� Mode$ TwoWay=�+  %Telerik.Windows.Controls.RadDataPager  -     Name$ radDataPager��$��5,0,5,0q�$��2��$	��Top=�   PageSize$	 100��   Source �� *Items+ �� ElementName$ voyagesGrid��  -   $
 btnSave��$
��Save��$��60��$��8,0,0,10q�$
��Left=�$��3��$��30��$��Bottom=�  -   $
 	btnCancel��$��Cancel��$��60��$��	73,0,0,10q�$
��Left=�$��3��$��30��$��Bottom=�