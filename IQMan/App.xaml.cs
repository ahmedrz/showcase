using EmanifestParser;
using IQManClient.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace IQMan
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            //SeedGeneralCargoData();
            //SeedFCLContainerData();
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }


        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = string.Format("An unhandled exception occurred: {0}", e.Exception.Message);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            // OR whatever you want like logging etc. MessageBox it's just example
            // for quick debugging etc.
            e.Handled = true;
        }

        private static void SeedGeneralCargoData()
        {

            using (EmanifestContext db = new EmanifestContext())
            {
                List<VoyageDetails> voyagesToAdd = new List<VoyageDetails>();
                List<BOLDetails> bolToAdd = new List<BOLDetails>();
                List<ContainerDetails> containerToAdd = new List<ContainerDetails>();
                List<ConsignmentDetails> consignmentsToAdd = new List<ConsignmentDetails>();
                List<VehicleDetails> vehiclesToAdd = new List<VehicleDetails>();
                for (int h = 0; h < 1000; h++)
                {
                    var manifestType = ManifestFileType.GeneralCargo.ToString();
                    VoyageDetails newVoyage = new VoyageDetails()
                    {
                        VoyageDetailsId = Guid.NewGuid(),
                        VesselName = "test",
                        LineCode = "123456",
                        VoyageAgentCode = "123456",
                        AgentVoyageNumber = "1",
                        PortCodeOfDischarge = "IQUQR",
                        ExpectedToArriveDate = DateTime.Now,
                        MessageType = "MFI",
                        NumberOfInstalment = h,
                        ManifestType = manifestType
                    };
                    for (int i = 0; i < 1000; i++)
                    {
                        BOLDetails newBol = new BOLDetails()
                        {
                            VoyageDetailsId = newVoyage.VoyageDetailsId,
                            BOLDetailsId = Guid.NewGuid(),
                            BillOfLadingNumber = i.ToString(),
                            BoxPartneringLineCode = "123456",
                            BoxPartneringAgentCode = "123456",
                            PortCodeOfOrigin = "IQUQR",
                            PortCodeOfLoading = "IQUQR",
                            PortCodeOfDischarge = "IQUQR",
                            PortCodeOfDestination = "IQUQR",
                            DateOfLoading = DateTime.Now,
                            TradeCode = "I",
                            TransShipmentMode = "S",
                            BillOfLadingOwnerName = "ahmed razzaq",
                            BillOfLadingOwnerAddress = "Iraq - Baghdad",
                            CargoCode = "F",
                            ConsolidatedCargoIndicator = "Y",
                            StorageRequestCode = "Y",
                            ContainerServiceType = "FCL/FCL",
                            CountryOfOrigin = "IQ",
                            ShipperName = "shipper name",
                            ShipperAddress = "new shipper address",
                            ShipperCountryCode = "IQ",
                            ConsigneeCode = "12345",
                            ConsigneeName = "Consignee",
                            ConsigneeAddress = "Consignee address",
                            Notify1Code = "123456",
                            Notify1Name = "Notify1Name",
                            Notify1Address = "Notify1Address",
                            Notify2Code = "123456",
                            Notify2Name = "Notify2Name",
                            Notify2Address = "Notify2Address",
                            Notify3Code = "123456",
                            Notify3Name = "Notify3Name",
                            Notify3Address = "Notify3Address",
                            MarksAndNumbers = "MarksAndNumbers",
                            CommodityCode = "123456789",
                            CommodityDescription = "CommodityDescription",
                            Packages = 1,
                            PackageType = "PACKAGES",
                            PackageTypeCode = "PKG",
                            ContainerNumber = "123456789",
                            CheckDigit = "1",
                            NoOfContainers = 1,
                            NoOfTeus = 50,
                            TotalTareWeight = 50.5,
                            CargoWeight = 50.5,
                            GrossWeight = 50.5,
                            CargoVolume = 50.5,
                            TotalQuantity = 50,
                            FreightTonne = 50.5,
                            NoOfPallets = 50,
                            SlacIndicator = "Y",
                            ContractCarriageCondition = "C&F",
                            Remarks = "Remarks"

                        };
                        bolToAdd.Add(newBol);
                        //ContainerDetails newContainer = new ContainerDetails()
                        //{
                        //    ContainerDetailsId = Guid.NewGuid(),
                        //    ContainerNo = "12345" + i,
                        //    CheckDigit = "I",
                        //    ISOCode = "22G1",
                        //    TareWeight = 100.50,
                        //    SealNumber = "1234567890",

                        //};
                        ConsignmentDetails newConsignment = new ConsignmentDetails()
                        {
                            BOLDetailsId = newBol.BOLDetailsId,
                            ConsignmentDetailsId = Guid.NewGuid(),
                            SerialNumber = 1,
                            MarksAndNumbers = "MarksAndNumbers",
                            CargoDescription = "CargoDescription",
                            UsedOrNew = "U",
                            CommodityCode = "1",
                            ConsignmentPackages = 5,
                            PackageType = "PACKAGES",
                            PackageTypeCode = "PKG",
                            NumberOfPallets = 10,
                            ConsignmentWeight = 100.50,
                            ConsignmentValume = 100.50,
                            DangerousGoodsIndicator = "Y",
                            IMOClassNumber = "IMO",
                            UnNumberOfDangerousGoods = "12345",
                            FlashPoint = 50,
                            UnitOfTemperatureForFlashPoint = "C",
                            StorageRequestedForDangerousGoods = "D",
                            RefrigerationRequired = "Y",
                            MinimumTemperatureOfRefrigeration = -50,
                            MaximumTemperatureOfRefrigeration = 50,
                            UnitOfTemperatureForRegrigeration = "C"

                        };
                        consignmentsToAdd.Add(newConsignment);
                        VehicleDetails newVehicle = new VehicleDetails()
                        {
                            ConsignmentDetailsId = newConsignment.ConsignmentDetailsId,
                            VehicleDetailsId = Guid.NewGuid(),
                            SerialNumber = 1,
                            VehicleEquipmentIndicator = "V",
                            UsedOrNew = "U",
                            ChasisNumber = "v",
                            CaseNumber = "123456789",
                            Make = "Toyota",
                            Model = "Corola",
                            EngineNumber = "123456789",
                            YearBuilt = "2019",
                            Color = "White",
                            RollingOrStatic = "R",
                            DescriptionOfGood = "DescriptionOfGood",
                            AdditionalAccesseries = "AdditionalAccesseries",
                            WeightInKg = 100.50,
                            Volume = 100.50,
                            Remarks = "Remarks"
                        };
                        vehiclesToAdd.Add(newVehicle);
                        //newConsignment.VehicleDetails.Add(newVehicle);
                        //newBol.ConsignmentDetails.Add(newConsignment);
                        //newVoyage.BOLDetails.Add(newBol);
                    }
                    voyagesToAdd.Add(newVoyage);
                }
                db.BulkInsert(voyagesToAdd);
                db.BulkInsert(bolToAdd);
                db.BulkInsert(consignmentsToAdd);
                db.BulkInsert(vehiclesToAdd);

            }

        }
        private static void SeedFCLContainerData()
        {

            using (EmanifestContext db = new EmanifestContext())
            {
                for (int h = 0; h < 1000; h++)
                {
                    var manifestType = ManifestFileType.FCLContainer.ToString();
                    VoyageDetails newVoyage = new VoyageDetails()
                    {
                        VoyageDetailsId = Guid.NewGuid(),
                        VesselName = "test",
                        LineCode = "123456",
                        VoyageAgentCode = "123456",
                        AgentVoyageNumber = "1",
                        PortCodeOfDischarge = "IQUQR",
                        ExpectedToArriveDate = DateTime.Now,
                        MessageType = "MFI",
                        NumberOfInstalment = h,
                        ManifestType = manifestType
                    };
                    for (int i = 0; i < 1000; i++)
                    {
                        BOLDetails newBol = new BOLDetails()
                        {
                            VoyageDetailsId = newVoyage.VoyageDetailsId,
                            BOLDetailsId = Guid.NewGuid(),
                            BillOfLadingNumber = "12345" + i,
                            BoxPartneringLineCode = "123456",
                            BoxPartneringAgentCode = "123456",
                            PortCodeOfOrigin = "IQUQR",
                            PortCodeOfLoading = "IQUQR",
                            PortCodeOfDischarge = "IQUQR",
                            PortCodeOfDestination = "IQUQR",
                            DateOfLoading = DateTime.Now,
                            ManifestRegisterationNumber = "12345678",
                            TradeCode = "I",
                            TransShipmentMode = "S",
                            BillOfLadingOwnerName = "ahmed razzaq",
                            BillOfLadingOwnerAddress = "Iraq - Baghdad",
                            CargoCode = "F",
                            ConsolidatedCargoIndicator = "Y",
                            StorageRequestCode = "Y",
                            ContainerServiceType = "FCL/FCL",
                            CountryOfOrigin = "IQ",
                            ShipperName = "shipper name",
                            ShipperAddress = "new shipper address",
                            ShipperCountryCode = "IQ",
                            ConsigneeCode = "12345",
                            ConsigneeName = "Consignee",
                            ConsigneeAddress = "Consignee address",
                            Notify1Code = "123456",
                            Notify1Name = "Notify1Name",
                            Notify1Address = "Notify1Address",
                            Notify2Code = "123456",
                            Notify2Name = "Notify2Name",
                            Notify2Address = "Notify2Address",
                            Notify3Code = "123456",
                            Notify3Name = "Notify3Name",
                            Notify3Address = "Notify3Address",
                            MarksAndNumbers = "MarksAndNumbers",
                            CommodityCode = "1234567899",
                            CommodityDescription = "CommodityDescription",
                            Packages = 1,
                            PackageType = "PKG",
                            PackageTypeCode = "PACKAGES",
                            ContainerNumber = "1234567899",
                            CheckDigit = "1",
                            NoOfContainers = 1,
                            NoOfTeus = 50,
                            TotalTareWeight = 50.5,
                            CargoWeight = 50.5,
                            GrossWeight = 50.5,
                            CargoVolume = 50.5,
                            TotalQuantity = 50,
                            FreightTonne = 50.5,
                            NoOfPallets = 50,
                            SlacIndicator = "Y",
                            ContractCarriageCondition = "C&F",
                            Remarks = "Remarks"

                        };
                        //ContainerDetails newContainer = new ContainerDetails()
                        //{
                        //    ContainerDetailsId = Guid.NewGuid(),
                        //    ContainerNo = "12345" + i,
                        //    CheckDigit = "I",
                        //    ISOCode = "22G1",
                        //    TareWeight = 100.50,
                        //    SealNumber = "1234567890",

                        //};
                        ContainerDetails newContainer = new ContainerDetails()
                        {
                            ContainerDetailsId = Guid.NewGuid(),
                            ContainerNo = "1234" + i,
                            CheckDigit = "I",
                            ISOCode = "22G1",
                            TareWeight = 100.50,
                            SealNumber = "1234567891",

                        };
                        ConsignmentDetails newConsignment = new ConsignmentDetails()
                        {
                            ContainerDetailsId = newContainer.ContainerDetailsId,
                            ConsignmentDetailsId = Guid.NewGuid(),
                            SerialNumber = 1,
                            MarksAndNumbers = "MarksAndNumbers",
                            CargoDescription = "CargoDescription",
                            UsedOrNew = "U",
                            CommodityCode = "12345" + i,
                            ConsignmentPackages = 5,
                            PackageType = "PackageType",
                            PackageTypeCode = "PKG",
                            NumberOfPallets = 10,
                            ConsignmentWeight = 100.50,
                            ConsignmentValume = 100.50,
                            DangerousGoodsIndicator = "Y",
                            IMOClassNumber = "IMO",
                            UnNumberOfDangerousGoods = "12345",
                            FlashPoint = 50,
                            UnitOfTemperatureForFlashPoint = "C",
                            StorageRequestedForDangerousGoods = "V",
                            RefrigerationRequired = "Y",
                            MinimumTemperatureOfRefrigeration = -50,
                            MaximumTemperatureOfRefrigeration = 50,
                            UnitOfTemperatureForRegrigeration = "C"

                        };
                        ConsignmentDetails newConsignment1 = new ConsignmentDetails()
                        {
                            ContainerDetailsId = newContainer.ContainerDetailsId,
                            ConsignmentDetailsId = Guid.NewGuid(),
                            SerialNumber = 1,
                            MarksAndNumbers = "MarksAndNumbers",
                            CargoDescription = "CargoDescription",
                            UsedOrNew = "U",
                            CommodityCode = "12345" + i,
                            ConsignmentPackages = 5,
                            PackageType = "PackageType",
                            PackageTypeCode = "PKG",
                            NumberOfPallets = 10,
                            ConsignmentWeight = 100.50,
                            ConsignmentValume = 100.50,
                            DangerousGoodsIndicator = "Y",
                            IMOClassNumber = "IMO",
                            UnNumberOfDangerousGoods = "12345",
                            FlashPoint = 50,
                            UnitOfTemperatureForFlashPoint = "C",
                            StorageRequestedForDangerousGoods = "V",
                            RefrigerationRequired = "Y",
                            MinimumTemperatureOfRefrigeration = -50,
                            MaximumTemperatureOfRefrigeration = 50,
                            UnitOfTemperatureForRegrigeration = "C"

                        };
                        VehicleDetails newVehicle = new VehicleDetails()
                        {
                            ConsignmentDetailsId = newConsignment.ContainerDetailsId,
                            VehicleDetailsId = Guid.NewGuid(),
                            SerialNumber = 1,
                            VehicleEquipmentIndicator = "V",
                            UsedOrNew = "U",
                            ChasisNumber = "1234" + i,
                            CaseNumber = "123456789",
                            Make = "Toyota",
                            Model = "Corola",
                            EngineNumber = "123456789",
                            YearBuilt = "2019",
                            Color = "White",
                            RollingOrStatic = "R",
                            DescriptionOfGood = "DescriptionOfGood",
                            AdditionalAccesseries = "AdditionalAccesseries",
                            WeightInKg = 100.50,
                            Volume = 100.50,
                            Remarks = "Remarks"
                        };


                        newConsignment.VehicleDetails.Add(newVehicle);
                        newContainer.ConsignmentDetails.Add(newConsignment);
                        newContainer.ConsignmentDetails.Add(newConsignment1);
                        newBol.ContainerDetails.Add(newContainer);
                        newVoyage.BOLDetails.Add(newBol);
                    }
                    db.VoyageDetails.Add(newVoyage);
                }
                db.BulkSaveChanges();

            }

        }
        private void EncryptConfigSection(string sectionKey)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSection section = config.GetSection(sectionKey);
            if (section != null)
            {
                if (!section.SectionInformation.IsProtected)
                {
                    if (!section.ElementInformation.IsLocked)
                    {
                        section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                        section.SectionInformation.ForceSave = true;
                        config.Save(ConfigurationSaveMode.Full);
                    }
                }
            }
        }
        protected override void OnStartup(StartupEventArgs eventArgs)
        {
            EncryptConfigSection("appSettings");
        }

    }
}
