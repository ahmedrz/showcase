   M S B A M L   `   `   `  ����e  aTelerik.Windows.Controls, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7eHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.ControlsTelerik.Windows.Controls  5       EmanifestDownloader$   EmanifestDownloader.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      dtelerikControlsHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.Controls     5      t pTelerik.Windows.Controls.DataServices60, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7w	 sTelerik.Windows.Controls.EntityFramework60, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j
 fTelerik.Windows.Controls.Data, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
     5      -   5        +   %Telerik.Windows.Controls.StyleManager    Theme-   'Telerik.Windows.Controls.ThemeConverter$  Expression_Dark 5      $ ��Agent's Uploaded Manifests��5         WindowBackground#���   5        ControlForeground_Normal#���  5	      $	��350��6I   $	��600��6V   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�6b   )  #Telerik.Windows.Controls.WindowBase   WindowState$ 	Maximized=�6�   .��5
      �  � 	Resources 5   
     MenuItemContainerStyle �� 
TargetType*  $Telerik.Windows.Controls.RadMenuItem �� Property �� Value��%�   &	       �� 5      "  63   .�5      ��  � �� 6+   �� *Text+�� 5       � �� 60   �� *
SubItems+�� 5       � {� 6.   �� *	IsEnabled+5      5       � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��45 �6   �� 5      $��30 �6   .+�5   
   (   "Telerik.Windows.Controls.RadButton  -   5          Name$ 
btnRefresh��5   �   $��	15,10,0,0q�65   $	��100��6H   $��30��6T   $
��Left=�6`   $	��Top=�6{   $��Refresh��6�   6�   �� 5   
   -   5        	 ��Name$	 chbAutoRefresh��5   $   
 d� 	IsChecked$

 TrueY�6   $��Auto Refresh��6<   $��
346,10,0,0q�6S   $	��113��6g   $��30��6s   $
��Left=�6   $	��Top=�6�   6�   /  )Telerik.Windows.Controls.RadNumericUpDown  5   
   -   5          Name$ txtInterval��5   f   +   %Telerik.Windows.Controls.RadRangeBase   Value$
 5000X�6#      NumberDecimalDigits$ 0��60      Minimum$	 500Q�6H      Maximum$ 10000Q�6V   $��30��6{   $	��Top=�6�   $��
459,10,0,0q�6�   $
��Left=�6�   $	��113��6�   6�   *	  $Telerik.Windows.Controls.RadGridView	  5   
   -   5         	 Name$ voyagesGrid��5   2   $��
15,45,15,0q�6   ;
  5Telerik.Windows.Controls.GridView.GridViewDataControl 
  
IsReadOnly .  
  CanUserInsertRows .   
  AutoGenerateColumns .   
  CanUserDeleteRows .   
  ShowGroupPanel .  *  $Telerik.Windows.Controls.DataControl   ItemsSource 5       ��  �� Path$ Voyages� 
  Columns 5!      1  +Telerik.Windows.Controls.GridViewDataColumn  5"      6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Vessel Name��6-      DataMemberBinding 6B   �� *
VesselName+  5#      $ 	Voyage No��6-    6@   �� *
VoyageNo+  5$      $ 	Port Code��6-    6@   �� *
PortCode+  5%      $ Expected Arrival��6-    6G   �� *ExpectedArrival+ �� StringFormat$ {0:dd/MM/yyyy}��  5&      $ 	Port Code��6-    6@   �� *Installment+  5'      $ 
Downloaded��6-    6A   �� *
Downloaded+-  'Telerik.Windows.Controls.RadContextMenu   ContextMenu 5)        5*      -   5          Name$ GridContextMenu��5*   )   #��[ 5+   )   5-   )   5/      + 
 %Telerik.Windows.Controls.RadDataPager  50   
   -   5          Name$ radDataPager��50   2   $��	15,0,15,0q�6   $��1��6H   $	��Top=�51         PageSize$ 15��53          Source  52      �� *Items+! �� ElementName$! voyagesGrid��53      �� 54   
   -   5        " ��Name$" pnlProgress��54      # �� Orientation$# 
Horizontal=�6*   $��2��6C   $y�Hidden=�6P   .+�55      $   Telerik.Windows.Controls.Label  .��6   Downloading Manifest62   � 56      $ � IsIndeterminate$ . $	��200��61   $��15��6=   6K   57      58      59      