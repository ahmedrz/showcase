   M S B A M L   `   `   `  ����	  IQMan    IQMan.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      w sTelerik.Windows.Controls.EntityFramework60, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n	 jTelerik.Windows.Controls.GridView, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2019.2.510.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Ktelerik1http://schemas.telerik.com/2008/xaml/presentation     	 
  5      -   5          � Title$%  General Cargo Emanifest Creator��5      $	��350��69   $
��1000��6F    � WindowState$ 	Maximized=�6S    � Icon$ ship_i0I_icon.ico��6k   .��5      �  � 	Resources 5	   
      MenuItemContainerStyle �� 
TargetType*  $Telerik.Windows.Controls.RadMenuItem �� Property �� Value ! ConsignmentRowDetailsTemplate* 	 $Telerik.Windows.Controls.RadGridView  Name; 	 5Telerik.Windows.Controls.GridView.GridViewDataControl   CanUserInsertRows	   CanUserDeleteRows
   CanUserGroupColumns   ShowGroupPanel   ShowGroupFooters   NewRowPosition   AutoGenerateColumns*  $Telerik.Windows.Controls.DataControl   ItemsSource-  'Telerik.Windows.Controls.RadContextMenu   ContextMenu  Name   Columns1 	 +Telerik.Windows.Controls.GridViewDataColumn- 	 'Telerik.Windows.Controls.GridViewColumn   	IsVisible6 	 0Telerik.Windows.Controls.GridViewBoundColumnBase   DataMemberBinding  RowDetailsTemplate   RowDetailsVisibilityMode   RowDetailsTemplate��%r
  &	        &	 �     7   &	 �    7  7   �� 5
      "  63   .�5      ��  � �� 6+   �� *Text+�� 5       � �� 60   �� *
SubItems+�� 5       � {� 6.   �� *	IsEnabled+5      �� 5      .R�5        $ vehiclesGrid��6&   $��10q�5   &    .  	 .  
 .   .   .  $
 None=�5       .   5      �� *VehicleDetails+ 5        5      $ vehicleMenu��61   -   5        8��  5   (    5         5!       .   6G   �� *VehicleDetailsId+  5"       65   �� *VehicleEquipmentIndicator+  5#       65   �� *	UsedOrNew+  5$       65   �� *ChasisNumber+  5%       65   �� *
CaseNumber+  5&       65   �� *Make+  5'       65   �� *Model+  5(       65   �� *EngineNumber+  5)       65   �� *	YearBuilt+  5*       65   �� *Color+  5+       65   �� *RollingOrStatic+  5,       65   �� *DescriptionOfGood+  5-       65   �� *AdditionalAccesseries+  5.       65   �� *
WeightInKg+  5/       65   �� *Volume+  50       65   �� *	Remarks+52      53      �� 55      .R�56        $ consignmentGrid��6&   $��10q�57   &    .  	 .  
 .   .   .  $
 None=�5=      $ VisibleWhenSelected=�5>   &   8    .   5B      �� *ConsignmentDetails+ 5D        5E      $ consignmentMenu��61   -   5        8�� 5H   (    5J        5K       .   6G   �� *ConsignmentDetailsId+  5L       65   �� *MarksAndNumbers+  5M       65   �� *CargoDescription+  5N       65   �� *	UsedOrNew+  5O       65   �� *CommodityCode+  5P       65   �� *ConsignmentPackages+  5Q       65   �� *PackageType+  5R       65   �� *PackageTypeCode+  5S       65   �� *NumberOfPallets+  5T       65   �� *ConsignmentWeight+  5U       65   �� *ConsignmentValume+  5V       65   �� *DangerousGoodsIndicator+  5W       65   �� *IMOClassNumber+  5X       65   �� *UnNumberOfDangerousGoods+  5Y       65   �� *
FlashPoint+  5Z       65   �� * UnitOfTemperatureForFlashPoint+  5[       65   �� *#!StorageRequestedForDangerousGoods+  5\       65   �� *RefrigerationRequired+  5]       65   �� *#!MinimumTemperatureOfRefrigeration+  5^       65   �� *#!MaximumTemperatureOfRefrigeration+  5_       65   �� *#!UnitOfTemperatureForRegrigeration+5b      5c      5d       � RowDefinitions 5e   
   �� 5f      $	��100 �6   �� 5g      $	��74* �6   �� 5h      $��35 �6   �� 5i      $��50 �6   .+�5k   
   � $��0��6   $��3q�6    5l      �� 5m      $��1* �6    �� 5n      $��1* �6    �� 5o      $��1* �6    .+�5q      �� $��0��6    �� Orientation$ 
Horizontal=�6&   .+�5r      $	  Telerik.Windows.Controls.Label	  $	��140��6    $��3q�6,   $��Line :��67   $
��Left=�6H   $	��Top=�6d   1
 
 +Telerik.Windows.Controls.RadAutoCompleteBox
  5s      -   5         
 Name$ cmbLine��5s   -    
  DisplayMemberPath$
 Text��6>    
  AutoCompleteMode$ Suggest=�6W    
  TextSearchPath$ Value��6r    
  SelectionMode$ Single=�6�   $��3q�6�   $
��Left=�6�   $	��Top=�6�   $	��150��6   
  DropDownWidth$	 300 �6!   
  
SearchText 6�   �� *Voyage.LineCode+  �� Mode$  TwoWay=�6!  	  5t      $	��140��6    $��3q�6,   $��Voyage Agent :��67   $
��Left=�6P   $	��Top=�6l   �� 5u      ! �� 	MaxLength$! 30��6C   $��3q�6R   $
��Left=�6]   $	��Top=�6y   $	��150��6�   ��6   �� *Voyage.VoyageAgentCode+6�   	  5v      $	��140��6    $��3q�6,   $��Vessel Name :��67   $
��Left=�6O   $	��Top=�6k   
  5w      -   5        $ 	txtVessel��5w   A   $	 500 �6-   $
 Name��6�   $ Suggest=�6�   $
 Name��6�   $ Single=�6�   $��3q�6  $
��Left=�6)  $	��Top=�6E  $	��150��6]   6�   �� *Voyage.VesselName+$  TwoWay=�6]  5x      �� 5y      $��1��6   $ 
Horizontal=�6&   .+�5z      	  $	��140��6    $��3q�6,   $��Agent Voyage Number :��67   $
��Left=�6W   $	��Top=�6s   �� 5{      $! 10��6D   $��3q�6S   $
��Left=�6^   $	��Top=�6z   $	��150��6�   ��6   �� *Voyage.AgentVoyageNumber+6�   	  5|      $	��140��6    $��3q�6,   $��PortCode Of Discharge :��67   $
��Left=�6Y   $	��Top=�6u   
  5~      -   5        $ txtOriginPort��5~   -   $
 Name��6D   $ Suggest=�6]   $ FullCode��6x   $ Single=�6�   $��3q�6  $
��Left=�6*  $	��Top=�6F  $	��150��6^   6�   �� *Voyage.PortCodeOfDischarge+$  TwoWay=�6^  	  5      $	��140��6    $��3q�6,   $��Expected To ArriveDate :��67   $
��Left=�6Z   $	��Top=�6v   , 
 &Telerik.Windows.Controls.RadDatePicker  5�      $��3q�6^   $
��Left=�6i   $	��Top=�6�   $	��150��6�   0 
 *Telerik.Windows.Controls.RadDateTimePicker"   SelectedValue" 6(   �� *Voyage.ExpectedToArriveDate+6�   5�      �� 5�      $��2��6   $ 
Horizontal=�6&   .+�5�      	  $	��140��6    $��3q�6,   $��Number Of Instalment :��67   $
��Left=�6X   $	��Top=�6t   / 
 )Telerik.Windows.Controls.RadNumericUpDown  5�      #   NumberDecimalDigits$# 0��6W   $��3q�6o   $
��Left=�6z   $	��Top=�6�   $	��150��6�   +  %Telerik.Windows.Controls.RadRangeBase$   Value$ 6+   �� *Voyage.NumberOfInstalment+6�   5�      5�        5�   
   -   5        $ bolGrid��5�      $��3q�6/   %   SelectionMode$% Single=�5�       .  	 .  
 .   .   .  $
 None=�5�       .  $ VisibleWhenSelected=�5�      $��1��5�      # [ 5�       5�   :   �� *Voyage.BOLDetails+ 5�        5�      -   5        $ GridContextMenu��5�   )   #��[  5�   *   5�   )    5�        5�       .   6?   �� *BOLDetailsId+  5�       6-   �� *BillOfLadingNumber+  5�       6-   �� *BoxPartneringLineCode+  5�       6-   �� *BoxPartneringAgentCode+  5�       6-   �� *PortCodeOfOrigin+  5�       6-   �� *PortCodeOfLoading+  5�       6-   �� *PortCodeOfDischarge+  5�       6-   �� *PortCodeOfDestination+  5�       6-   �� *DateOfLoading+  5�       6-   �� *	TradeCode+  5�       6-   �� *TransShipmentMode+  5�       6-   �� *BillOfLadingOwnerName+  5�       6-   �� *BillOfLadingOwnerAddress+  5�       6-   �� *	CargoCode+  5�       6-   �� *ConsolidatedCargoIndicator+  5�       6-   �� *StorageRequestCode+  5�       6-   �� *ContainerServiceType+  5�       6-   �� *CountryOfOrigin+  5�       6-   �� *ShipperName+  5�       6-   �� *ShipperAddress+  5�       6-   �� *ShipperCountryCode+  5�       6-   �� *ConsigneeCode+  5�       6-   �� *ConsigneeName+  5�       6-   �� *ConsigneeAddress+  5�       6-   �� *Notify1Code+  5�       6-   �� *Notify1Name+  5�       6-   �� *Notify1Address+  5�       6-   �� *Notify2Code+  5�       6-   �� *Notify2Name+  5�       6-   �� *Notify2Address+  5�       6-   �� *Notify3Code+  5�       6-   �� *Notify3Name+  5�       6-   �� *Notify3Address+  5�       6-   �� *MarksAndNumbers+  5�       6-   �� *CommodityCode+  5�       6-   �� *CommodityDescription+  5�       6-   �� *
Packages+  5�       6-   �� *PackageType+  5�       6-   �� *PackageTypeCode+  5�       6-   �� *ContainerNumber+  5�       6-   �� *
CheckDigit+  5�       6-   �� *NoOfContainers+  5�       6-   �� *
NoOfTeus+  5�       6-   �� *TotalTareWeight+  5�       6-   �� *CargoWeight+  5�       6-   �� *GrossWeight+  5�       6-   �� *CargoVolume+  5�       6-   �� *TotalQuantity+  5�       6-   �� *FreightTonne+  5�       6-   �� *NoOfPallets+  5�       6-   �� *SlacIndicator+  5�       6-   �� *ContractCarriageCondition+  5�       6-   �� *	Remarks+5�      +  %Telerik.Windows.Controls.RadDataPager  5�   
   -	   5        &  Name$& radDataPager��5�      $��2��65   '   PageSize$' 15��5�      $��0q�6)   (   Source( 5�      �� *Items+) �� ElementName$) bolGrid��5�   )   �� 5�   
   $ 
Horizontal=�6   $��3��6.   .+�5�      (  "Telerik.Windows.Controls.RadButton  -
   5        $��8q�5�       $	��100��6+   .��6Q   Load EDI File6`     5�      -   5        *  Name$* btnSave��5�       $��8q�61   $	��100��6<   .��6^   Save Changes6l   5�      5�      5�      