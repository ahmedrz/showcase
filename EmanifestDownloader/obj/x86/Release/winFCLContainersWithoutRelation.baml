   M S B A M L   `   `   `  ���� e  aTelerik.Windows.Controls, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7eHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.ControlsTelerik.Windows.Controls   EmanifestDownloader9   3EmanifestDownloader.winFCLContainersWithoutRelation   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     t pTelerik.Windows.Controls.DataServices60, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7w	 sTelerik.Windows.Controls.EntityFramework60, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j
 fTelerik.Windows.Controls.Data, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
     ltelerikControlsHclr-namespace:Telerik.Windows.Controls;assembly=Telerik.Windows.Controls    	 
     +   %Telerik.Windows.Controls.StyleManager    Theme-   'Telerik.Windows.Controls.ThemeConverter$  Expression_Dark $'��!FCLContainer (Relation Not Known)��   WindowBackground#���     ControlForeground_Normal#���  $	��350��$
��1000��)  #Telerik.Windows.Controls.WindowBase   WindowState$ 	Maximized=�.���  � 	Resources   MenuItemContainerStyle �� 
TargetType*  $Telerik.Windows.Controls.RadMenuItem �� Property �� Value ! ConsignmentRowDetailsTemplate*  $Telerik.Windows.Controls.RadGridView;  5Telerik.Windows.Controls.GridView.GridViewDataControl   
IsReadOnly   CanUserInsertRows   CanUserDeleteRows	   CanUserGroupColumns
   ShowGroupPanel   ShowGroupFooters   NewRowPosition   AutoGenerateColumns*  $Telerik.Windows.Controls.DataControl   ItemsSource-  'Telerik.Windows.Controls.RadContextMenu   ContextMenu  Name   Columns1	  +Telerik.Windows.Controls.GridViewDataColumn-
  'Telerik.Windows.Controls.GridViewColumn 
  	IsVisible6  0Telerik.Windows.Controls.GridViewBoundColumnBase   DataMemberBinding5  /Telerik.Windows.Controls.GridViewComboBoxColumn   Header 
  
UniqueName   SelectedValueMemberPath   DisplayMemberPath   ItemsSource �� RelativeSource �� Mode �� AncestorType(  "Telerik.Windows.Controls.RadWindow  RowDetailsTemplate,  &Telerik.Windows.Controls.RadTabControl)  #Telerik.Windows.Controls.RadTabItem   RowDetailsVisibilityMode   RowDetailsTemplate��%{  &	       &	 s     7  &	 �    7  7  7  �� "  .���  � �� �� *Text+��  � �� �� *
SubItems+��  � {� �� *	IsEnabled+�� .R�  $��vehiclesGrid��$��10q� .  .   .  	 .  
 .   .  $
 None=� .   �� *VehicleDetails+   $ vehiclesMenu��-   8��   	   .   �� *VehicleDetailsId+  $ VehicleEquipmentIndicator��$ VehicleEquipmentIndicator��$ Value��$
 Text�� �� *VehicleEquipmentIndicator+ �� **(Sources.VehicleEquipmentIndicatorsSource+ �� $ FindAncestor=�"    $ 	UsedOrNew��$ 	UsedOrNew��$ Value��$
 Text�� �� *	UsedOrNew+ �� *Sources.UsedNewSource+ �� $ FindAncestor=�"  	   �� *ChasisNumber+	   �� *
CaseNumber+	   �� *Make+	   �� *Model+	   �� *EngineNumber+	   �� *	YearBuilt+	   �� *Color+  $ RollingOrStatic��$ RollingOrStatic��$ Value��$
 Text�� �� *RollingOrStatic+ �� *Sources.RollingStaticSource+ �� $ FindAncestor=�"  	   �� *DescriptionOfGood+	   �� *AdditionalAccesseries+	   �� *
WeightInKg+	   �� *Volume+	   �� *	Remarks+�� .R�  @�  $��Consignments Details��.��  $��consignmentGrid��$��10q� .  .   .  	 .  
 .   .  $
 None=�$ VisibleWhenSelected=�8    .   �� *ConsignmentDetails+   $ consignmentMenu��-   8��  	   .   �� *ConsignmentDetailsId+	   �� *MarksAndNumbers+	   �� *CargoDescription+  $ 	UsedOrNew��$ 	UsedOrNew��$ Value��$
 Text�� �� *	UsedOrNew+ �� *Sources.UsedNewSource+ �� $ FindAncestor=�"  	   �� *CommodityCode+	   �� *ConsignmentPackages+	   �� *PackageType+	   �� *PackageTypeCode+	   �� *NumberOfPallets+	   �� *ConsignmentWeight+	   �� *ConsignmentValume+  $ DangerousGoodsIndicator��$ DangerousGoodsIndicator��$ Value��$
 Text�� �� *DangerousGoodsIndicator+ �� *Sources.YesNoSource+ �� $ FindAncestor=�"  	   �� *IMOClassNumber+	   �� *UnNumberOfDangerousGoods+	   �� *
FlashPoint+  $$ UnitOfTemperatureForFlashPoint��$$ UnitOfTemperatureForFlashPoint��$ Value��$
 Text�� �� * UnitOfTemperatureForFlashPoint+ �� *Sources.TemperaturesSource+ �� $ FindAncestor=�"    $' !StorageRequestedForDangerousGoods��$' !StorageRequestedForDangerousGoods��$ Value��$
 Text�� �� *#!StorageRequestedForDangerousGoods+ �� *#!Sources.StorageRequestCodesSource+ �� $ FindAncestor=�"    $ RefrigerationRequired��$ RefrigerationRequired��$ Value��$
 Text�� �� *RefrigerationRequired+ �� *Sources.YesNoSource+ �� $ FindAncestor=�"  	   �� *#!MinimumTemperatureOfRefrigeration+	   �� *#!MaximumTemperatureOfRefrigeration+  $' !UnitOfTemperatureForRegrigeration��$' !UnitOfTemperatureForRegrigeration��$ Value��$
 Text�� �� *#!UnitOfTemperatureForRegrigeration+ �� *Sources.TemperaturesSource+ �� $ FindAncestor=�"    $��Containers Details��.��  $��containersGrid�� .  .   .  	 .  
 .   .  $
 None=�$ VisibleWhenSelected=�$��10q� .   �� *ContainerDetails+   $ containerMenu��-   8��  	   .   �� *ContainerDetailsId+	   �� *ContainerNo+	   �� *
CheckDigit+	   �� *	ISOCode+	   �� *
TareWeight+	   �� *
SealNumber+ � RowDefinitions �� $	��100 ��� $��* ��� $��35 �.+�� $��0��$��3q� �� $��1* ��� $��1* ��� $��1* �.+��� $��0�� �� Orientation$ 
Horizontal=�.+�$   Telerik.Windows.Controls.Label  $	��140��$��3q�$��Line :��$
��Left=�$	��Top=���   �� 
IsReadOnly  . ! �� 	MaxLength$! 30��$��3q�$
��Left=�$	��Top=�$	��150������ *Voyage.LineCode+  $	��140��$��3q�$��Voyage Agent :��$
��Left=�$	��Top=���   . $! 30��$��3q�$
��Left=�$	��Top=�$	��150������ *Voyage.VoyageAgentCode+  $	��140��$��3q�$��Vessel Name :��$
��Left=�$	��Top=���   . $��3q�$
��Left=�$	��Top=�$	��150������ *Voyage.VesselName+�� $��1��$ 
Horizontal=�.+�  $	��140��$��3q�$��Agent Voyage Number :��$
��Left=�$	��Top=���   . $! 10��$��3q�$
��Left=�$	��Top=�$	��150������ *Voyage.AgentVoyageNumber+  $	��140��$��3q�$��PortCode Of Discharge :��$
��Left=�$	��Top=���   . $��3q�$
��Left=�$	��Top=�$	��150������ *Voyage.PortCodeOfDischarge+  $	��140��$��3q�$��Expected To ArriveDate :��$
��Left=�$	��Top=�,  &Telerik.Windows.Controls.RadDatePicker  0  *Telerik.Windows.Controls.RadDateTimePicker"   
IsReadOnly" . $��3q�$
��Left=�$	��Top=�$	��150��#   SelectedValue# �� *Voyage.ExpectedToArriveDate+�� $��2��$ 
Horizontal=�.+�  $	��140��$��3q�$��Number Of Instalment :��$
��Left=�$	��Top=�/  )Telerik.Windows.Controls.RadNumericUpDown  $   
IsReadOnly$ . %   NumberDecimalDigits$% 0��$��3q�$
��Left=�$	��Top=�$	��150��+   %Telerik.Windows.Controls.RadRangeBase&   Value& �� *Voyage.NumberOfInstalment+  -   '  Name$' bolGrid�� . $��3q�(   SelectionMode$( Multiple=� .  $ VisibleWhenSelected=� .   .  	 .  
 .   .  $
 None=�$��1��# [  �� *Voyage.BOLDetails+   -   $ GridContextMenu��#��[  	   .   �� *BOLDetailsId+	   �� *BillOfLadingNumber+	   �� *BoxPartneringLineCode+	   �� *BoxPartneringAgentCode+	   �� *PortCodeOfOrigin+	   �� *PortCodeOfLoading+	   �� *PortCodeOfDischarge+	   �� *PortCodeOfDestination+	   �� *DateOfLoading+) �� StringFormat$) {0:dd/MM/yyyy}��  $ 	Tade Code��$ 	TradeCode��$ Value��$
 Text�� �� *	TradeCode+  $ TransShipmentMode��$ TransShipmentMode��$ Value��$
 Text�� �� *TransShipmentMode+	   �� *BillOfLadingOwnerName+	   �� *BillOfLadingOwnerAddress+  $ 	CargoCode��$ 	CargoCode��$ Value��$
 Text�� �� *	CargoCode+  $  ConsolidatedCargoIndicator��$  ConsolidatedCargoIndicator��$ Value��$
 Text�� �� *ConsolidatedCargoIndicator+  $ StorageRequestCode��$ StorageRequestCode��$ Value��$
 Text�� �� *StorageRequestCode+	   �� *ContainerServiceType+	   �� *CountryOfOrigin+	   �� *ShipperName+	   �� *ShipperAddress+	   �� *ShipperCountryCode+	   �� *ConsigneeCode+	   �� *ConsigneeName+	   �� *ConsigneeAddress+	   �� *Notify1Code+	   �� *Notify1Name+	   �� *Notify1Address+	   �� *Notify2Code+	   �� *Notify2Name+	   �� *Notify2Address+	   �� *Notify3Code+	   �� *Notify3Name+	   �� *Notify3Address+	   �� *MarksAndNumbers+	   �� *CommodityCode+	   �� *CommodityDescription+	   �� *
Packages+	   �� *PackageType+	   �� *PackageTypeCode+	   �� *ContainerNumber+	   �� *
CheckDigit+	   �� *NoOfContainers+	   �� *
NoOfTeus+	   �� *TotalTareWeight+	   �� *CargoWeight+	   �� *GrossWeight+	   �� *CargoVolume+	   �� *TotalQuantity+	   �� *FreightTonne+	   �� *NoOfPallets+  $ SlacIndicator��$ SlacIndicator��$ Value��$
 Text�� �� *SlacIndicator+  $ ContractCarriageCondition��$ ContractCarriageCondition��$ Value��$
 Text�� �� *ContractCarriageCondition+	   �� *	Remarks++ 
 %Telerik.Windows.Controls.RadDataPager  -   *  Name$* radDataPager��$��2��+   PageSize$+ 15��$��0q�,   Source, �� *Items+- �� ElementName$- bolGrid��