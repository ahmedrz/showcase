   M S B A M L   `   `   `  ����e  aTelerik.Windows.Controls, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7eHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.ControlsTelerik.Windows.Controls  5       EmanifestDownloader+   %EmanifestDownloader.winDownloadVoyage   5      t pTelerik.Windows.Controls.DataServices60, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7w sTelerik.Windows.Controls.EntityFramework60, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation          R	 NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\
 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 	 
  5   	   btelerikControlsHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.Controls 	 
  5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	 
   5   	   @d2http://schemas.microsoft.com/expression/blend/2008 	 
   5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	 
   5	   	   -   5        +   %Telerik.Windows.Controls.StyleManager    Theme-   'Telerik.Windows.Controls.ThemeConverter$  Expression_Dark 5      $��Download Voyage��5
         WindowBackground#���   5        ControlForeground_Normal#���  5      $	��350��6I   $	��600��6V   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�6b   )  #Telerik.Windows.Controls.WindowBase   WindowState$ 	Maximized=�6�   .��5      �  � RowDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��* �6   �� 5      $��35 �6   �� 5      $��45 �6   .+�5   
   $   Telerik.Windows.Controls.Label  $��62��6   $��8,10,0,0q�6#   $��Line :��65   $
��Left=�6F   $	��Top=�6b   $��23��6z   *  $Telerik.Windows.Controls.RadComboBox  5   
   -   5          Name$ cmbLines��5   �      AllowMultipleSelection . %   ClearSelectionButtonVisibility$ Visible=�6<   "   ClearSelectionButtonContent$ Clear��6e   $��	70,11,0,0q�6�   $
��Left=�6�   $	��Top=�6�   $	��163��6�   6�     5   
   $��62��6   $��8,37,0,0q�6#   $��Dep. :��65   $
��Left=�6F   $	��Top=�6b   $��23��6z     5   
   -   5        $ cmbDeps��5   �    �� SelectedValuePath$ Id��6   	 �� DisplayMemberPath$
	 Text��65   $ Visible=�6N   $ Clear��6w   $��	70,38,0,0q�6�   $
��Left=�6�   $	��Top=�6�   $	��163��6�   6  (   "Telerik.Windows.Controls.RadButton  5   
   -   5        
  Name$
 	btnchoose��5      $��Choose��6/   $��60��6@   $��30��6K   $��	70,65,0,0q�6W   $	��Top=�6j   $
��Left=�6�   6�   *  $Telerik.Windows.Controls.RadGridView  5   
   -   5          Name$ bolGrid��5   8   $	��5,0q�6   $��1��6+   ;	  5Telerik.Windows.Controls.GridView.GridViewDataControl 	  
IsReadOnly .  	  CanUserInsertRows .   	  AutoGenerateColumns .   	  CanUserDeleteRows .   	  ShowGroupPanel .  *
  $Telerik.Windows.Controls.DataControl 
  ItemsSource 5#      ��  �� Path$ 
BolDetails� 	  Columns 5$      1  +Telerik.Windows.Controls.GridViewDataColumn  5%      6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ BillOfLadingNumber��6-      DataMemberBinding 6I   �� *BillOfLadingNumber+  5&      $
 Line��6-    6;   �� *BoxPartneringLineCode+  5'      $ ConsigneeName��6-    6D   �� *ConsigneeName+5  /Telerik.Windows.Controls.GridViewComboBoxColumn  5(      $
 Dep.��61   -  'Telerik.Windows.Controls.GridViewColumn   
UniqueName$ 	depColumn��5*         SelectedValueMemberPath$ DepartmentId��5+         DisplayMemberPath$ DepartmentName��5,       5)      �� *DepartmentId+ �� Mode$ TwoWay=�5,      5.      +  %Telerik.Windows.Controls.RadDataPager  5/   
   -   5          Name$ radDataPager��5/   0   $��5,0,5,0q�6   $��2��6F   $	��Top=�50         PageSize$	 100��52         Source 51      �� *Items+ �� ElementName$ voyagesGrid��52        53   
   -   5        $
 btnSave��53      $
��Save��6-   $��60��6<   $��8,0,0,10q�6G   $
��Left=�6Y   $��3��6t   $��30��6�   $��Bottom=�6�   6�     54   
   -   5        $
 	btnCancel��54      $��Cancel��6/   $��60��6@   $��	73,0,0,10q�6K   $
��Left=�6^   $��3��6y   $��30��6�   $��Bottom=�6�   6�   55      56      