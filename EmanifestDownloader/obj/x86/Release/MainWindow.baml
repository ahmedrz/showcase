   M S B A M L   `   `   `  ���� e  aTelerik.Windows.Controls, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7eHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.ControlsTelerik.Windows.Controls   EmanifestDownloader$   EmanifestDownloader.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     dtelerikControlsHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.Controls     t pTelerik.Windows.Controls.DataServices60, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7w	 sTelerik.Windows.Controls.EntityFramework60, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j
 fTelerik.Windows.Controls.Data, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
     -   +   %Telerik.Windows.Controls.StyleManager    Theme-   'Telerik.Windows.Controls.ThemeConverter$  Expression_Dark $ ��Agent's Uploaded Manifests��   WindowBackground#���     ControlForeground_Normal#���  $	��350��$	��600��(  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�)  #Telerik.Windows.Controls.WindowBase   WindowState$ 	Maximized=�.���  � 	Resources   MenuItemContainerStyle �� 
TargetType*  $Telerik.Windows.Controls.RadMenuItem �� Property �� Value��%}   &	       �� "  .���  � �� �� *Text+��  � �� �� *
SubItems+��  � {� �� *	IsEnabled+ � RowDefinitions �� $��* ��� $��45 ��� $��30 �.+�(   "Telerik.Windows.Controls.RadButton  -     Name$ 
btnRefresh��$��	15,10,0,0q�$	��100��$��30��$
��Left=�$	��Top=�$��Refresh���� -   	 ��Name$	 chbAutoRefresh��
 d� 	IsChecked$

 TrueY�$��Auto Refresh��$��
346,10,0,0q�$	��113��$��30��$
��Left=�$	��Top=�/  )Telerik.Windows.Controls.RadNumericUpDown  -     Name$ txtInterval��+   %Telerik.Windows.Controls.RadRangeBase   Value$
 5000X�   NumberDecimalDigits$ 0��   Minimum$	 500Q�   Maximum$ 10000Q�$��30��$	��Top=�$��
459,10,0,0q�$
��Left=�$	��113��*	  $Telerik.Windows.Controls.RadGridView	  -    	 Name$ voyagesGrid��$��
15,45,15,0q�;
  5Telerik.Windows.Controls.GridView.GridViewDataControl 
  
IsReadOnly .  
  CanUserInsertRows .   
  AutoGenerateColumns .   
  CanUserDeleteRows .   
  ShowGroupPanel .  *  $Telerik.Windows.Controls.DataControl   ItemsSource ��  �� Path$ Voyages� 
  Columns 1  +Telerik.Windows.Controls.GridViewDataColumn  6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Vessel Name��   DataMemberBinding �� *
VesselName+  $ 	Voyage No�� �� *
VoyageNo+  $ 	Port Code�� �� *
PortCode+  $ Expected Arrival�� �� *ExpectedArrival+ �� StringFormat$ {0:dd/MM/yyyy}��  $ 	Port Code�� �� *Installment+  $ 
Downloaded�� �� *
Downloaded+-  'Telerik.Windows.Controls.RadContextMenu   ContextMenu   -     Name$ GridContextMenu��#��[ + 
 %Telerik.Windows.Controls.RadDataPager  -     Name$ radDataPager��$��	15,0,15,0q�$��1��$	��Top=�   PageSize$ 15��    Source  �� *Items+! �� ElementName$! voyagesGrid���� -   " ��Name$" pnlProgress��# �� Orientation$# 
Horizontal=�$��2��$y�Hidden=�.+�$   Telerik.Windows.Controls.Label  .��Downloading Manifest� $ � IsIndeterminate$ . $	��200��$��15��