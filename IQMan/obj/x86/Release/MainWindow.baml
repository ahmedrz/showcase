   M S B A M L   `   `   `  ���� 	  IQMan    IQMan.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     w sTelerik.Windows.Controls.EntityFramework60, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n	 jTelerik.Windows.Controls.GridView, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Ktelerik1http://schemas.telerik.com/2008/xaml/presentation     	 
  -     � Title$%  General Cargo Emanifest Creator��$	��350��$
��1000�� � WindowState$ 	Maximized=� � Icon$ ship_i0I_icon.ico��.���  � 	Resources    MenuItemContainerStyle �� 
TargetType*  $Telerik.Windows.Controls.RadMenuItem �� Property �� Value ! ConsignmentRowDetailsTemplate* 	 $Telerik.Windows.Controls.RadGridView  Name; 	 5Telerik.Windows.Controls.GridView.GridViewDataControl   CanUserInsertRows	   CanUserDeleteRows
   CanUserGroupColumns   ShowGroupPanel   ShowGroupFooters   NewRowPosition   AutoGenerateColumns*  $Telerik.Windows.Controls.DataControl   ItemsSource-  'Telerik.Windows.Controls.RadContextMenu   ContextMenu  Name   Columns1 	 +Telerik.Windows.Controls.GridViewDataColumn- 	 'Telerik.Windows.Controls.GridViewColumn   	IsVisible6 	 0Telerik.Windows.Controls.GridViewBoundColumnBase   DataMemberBinding  RowDetailsTemplate   RowDetailsVisibilityMode   RowDetailsTemplate��%6  &	        &	 s     7   &	     7  7   �� "  .���  � �� �� *Text+��  � �� �� *
SubItems+��  � {� �� *	IsEnabled+�� .R�  $ vehiclesGrid��$��10q� .  	 .  
 .   .   .  $
 None=� .   �� *VehicleDetails+   $ vehicleMenu��-   8��      .   �� *VehicleDetailsId+   �� *VehicleEquipmentIndicator+   �� *	UsedOrNew+   �� *ChasisNumber+   �� *
CaseNumber+   �� *Make+   �� *Model+   �� *EngineNumber+   �� *	YearBuilt+   �� *Color+   �� *RollingOrStatic+   �� *DescriptionOfGood+   �� *AdditionalAccesseries+   �� *
WeightInKg+   �� *Volume+   �� *	Remarks+�� .R�  $ consignmentGrid��$��10q� .  	 .  
 .   .   .  $
 None=�$ VisibleWhenSelected=�8    .   �� *ConsignmentDetails+   $ consignmentMenu��-   8��     .   �� *ConsignmentDetailsId+   �� *MarksAndNumbers+   �� *CargoDescription+   �� *	UsedOrNew+   �� *CommodityCode+   �� *ConsignmentPackages+   �� *PackageType+   �� *PackageTypeCode+   �� *NumberOfPallets+   �� *ConsignmentWeight+   �� *ConsignmentValume+   �� *DangerousGoodsIndicator+   �� *IMOClassNumber+   �� *UnNumberOfDangerousGoods+   �� *
FlashPoint+   �� * UnitOfTemperatureForFlashPoint+   �� *#!StorageRequestedForDangerousGoods+   �� *RefrigerationRequired+   �� *#!MinimumTemperatureOfRefrigeration+   �� *#!MaximumTemperatureOfRefrigeration+   �� *#!UnitOfTemperatureForRegrigeration+ � RowDefinitions �� $	��100 ��� $	��74* ��� $��35 ��� $��50 �.+�� $��0��$��3q� �� $��1* ��� $��1* ��� $��1* �.+��� $��0�� �� Orientation$ 
Horizontal=�.+�$	  Telerik.Windows.Controls.Label	  $	��140��$��3q�$��Line :��$
��Left=�$	��Top=�1
 
 +Telerik.Windows.Controls.RadAutoCompleteBox
  -    
 Name$ cmbLine�� 
  DisplayMemberPath$
 Text�� 
  AutoCompleteMode$ Suggest=� 
  TextSearchPath$ Value�� 
  SelectionMode$ Single=�$��3q�$
��Left=�$	��Top=�$	��150�� 
  DropDownWidth$	 300 � 
  
SearchText �� *Voyage.LineCode+  �� Mode$  TwoWay=�	  $	��140��$��3q�$��Voyage Agent :��$
��Left=�$	��Top=��� ! �� 	MaxLength$! 30��$��3q�$
��Left=�$	��Top=�$	��150������ *Voyage.VoyageAgentCode+	  $	��140��$��3q�$��Vessel Name :��$
��Left=�$	��Top=�
  -   $ 	txtVessel��$	 500 �$
 Name��$ Suggest=�$
 Name��$ Single=�$��3q�$
��Left=�$	��Top=�$	��150�� �� *Voyage.VesselName+$  TwoWay=��� $��1��$ 
Horizontal=�.+�	  $	��140��$��3q�$��Agent Voyage Number :��$
��Left=�$	��Top=��� $! 10��$��3q�$
��Left=�$	��Top=�$	��150������ *Voyage.AgentVoyageNumber+	  $	��140��$��3q�$��PortCode Of Discharge :��$
��Left=�$	��Top=�
  -   $ txtOriginPort��$
 Name��$ Suggest=�$ FullCode��$ Single=�$��3q�$
��Left=�$	��Top=�$	��150�� �� *Voyage.PortCodeOfDischarge+$  TwoWay=�	  $	��140��$��3q�$��Expected To ArriveDate :��$
��Left=�$	��Top=�, 
 &Telerik.Windows.Controls.RadDatePicker  $��3q�$
��Left=�$	��Top=�$	��150��0 
 *Telerik.Windows.Controls.RadDateTimePicker"   SelectedValue" �� *Voyage.ExpectedToArriveDate+�� $��2��$ 
Horizontal=�.+�	  $	��140��$��3q�$��Number Of Instalment :��$
��Left=�$	��Top=�/ 
 )Telerik.Windows.Controls.RadNumericUpDown  #   NumberDecimalDigits$# 0��$��3q�$
��Left=�$	��Top=�$	��150��+  %Telerik.Windows.Controls.RadRangeBase$   Value$ �� *Voyage.NumberOfInstalment+  -   $ bolGrid��$��3q�%   SelectionMode$% Single=� .  	 .  
 .   .   .  $
 None=� .  $ VisibleWhenSelected=�$��1��# [  �� *Voyage.BOLDetails+   -   $ GridContextMenu��#��[      .   �� *BOLDetailsId+   �� *BillOfLadingNumber+   �� *BoxPartneringLineCode+   �� *BoxPartneringAgentCode+   �� *PortCodeOfOrigin+   �� *PortCodeOfLoading+   �� *PortCodeOfDischarge+   �� *PortCodeOfDestination+   �� *DateOfLoading+   �� *	TradeCode+   �� *TransShipmentMode+   �� *BillOfLadingOwnerName+   �� *BillOfLadingOwnerAddress+   �� *	CargoCode+   �� *ConsolidatedCargoIndicator+   �� *StorageRequestCode+   �� *ContainerServiceType+   �� *CountryOfOrigin+   �� *ShipperName+   �� *ShipperAddress+   �� *ShipperCountryCode+   �� *ConsigneeCode+   �� *ConsigneeName+   �� *ConsigneeAddress+   �� *Notify1Code+   �� *Notify1Name+   �� *Notify1Address+   �� *Notify2Code+   �� *Notify2Name+   �� *Notify2Address+   �� *Notify3Code+   �� *Notify3Name+   �� *Notify3Address+   �� *MarksAndNumbers+   �� *CommodityCode+   �� *CommodityDescription+   �� *
Packages+   �� *PackageType+   �� *PackageTypeCode+   �� *ContainerNumber+   �� *
CheckDigit+   �� *NoOfContainers+   �� *
NoOfTeus+   �� *TotalTareWeight+   �� *CargoWeight+   �� *GrossWeight+   �� *CargoVolume+   �� *TotalQuantity+   �� *FreightTonne+   �� *NoOfPallets+   �� *SlacIndicator+   �� *ContractCarriageCondition+   �� *	Remarks++  %Telerik.Windows.Controls.RadDataPager  -	   &  Name$& radDataPager��$��2��'   PageSize$' 15��$��0q�(   Source( �� *Items+) �� ElementName$) bolGrid���� $ 
Horizontal=�$��3��.+�(  "Telerik.Windows.Controls.RadButton  -
   $��8q�$	��100��.��Load EDI File  -   *  Name$* btnSave��$��8q�$	��100��.��Save Changes