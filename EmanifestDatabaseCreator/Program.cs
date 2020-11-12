using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EmanifestParser;
using PortSystem.DAL;

namespace EmanifestDatabaseCreator
{
    class Program
    {
        static EmanifestContext db;
        static List<UNHSCodes> HSCODES;
        static List<UNHazardousCodes> HAZARDOUSCODES;
        static List<PackageCodes> PACKAGES;
        static List<ContainerIsoCodes> ISOCODES;
        static List<CountryCodes> COUNTRYCODES;
        static List<LocationCodes> LOCATIONCODES;
        static List<Lines> LINES;

        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            db = new EmanifestContext();
            HSCODES = db.UNHSCodes.Where(c => c.Classification == "H5").ToList();
            HAZARDOUSCODES = db.UNHazardousCodes.ToList();
            PACKAGES = db.PackageCodes.ToList();
            ISOCODES = db.ContainerIsoCodes.ToList();
            COUNTRYCODES = db.CountryCodes.ToList();
            LOCATIONCODES = db.LocationCodes.ToList();
            LINES = db.Lines.ToList();

            CreateLCLManifestFile();
            CreateFCLManifestFile();
            CreateFCLNoRelationManifestFile();
            CreateGeneralCargoManifestFile();
            //RunAsync().Wait();
            //GetVoyageAsync("api/test/getvoyage?vesselName=test&voyage=1&portCode=China").Wait();

            //using (EmanifestContext db = new EmanifestContext())
            //{
            //    //var test = db.VoyageDetails.Find(new Guid("08ffb8cd-ffc6-4717-9c85-4daf54101a40"));
            //    //EmanifestSerializer s = new EmanifestSerializer(ManifestFileType.FCLContainer);
            //    //var ss = s.SerializeToString(test);

            //    VoyageDetails newVoyage = new VoyageDetails()
            //    {
            //        VoyageDetailsId = Guid.NewGuid(),
            //        VesselName = "test",
            //        LineCode = "123456",
            //        VoyageAgentCode = "123456",
            //        AgentVoyageNumber = "1",
            //        PortCodeOfDischarge = "China",
            //        ExpectedToArriveDate = DateTime.Now,
            //        MessageType = "EMI",
            //        NumberOfInstalment = 1,
            //        AgentManifestSequenceNumber = 1,
            //        RotationNumber = "123456"
            //    };
            //    for (int i = 0; i < 100; i++)
            //    {
            //        BOLDetails newBol = new BOLDetails()
            //        {
            //            BOLDetailsId = Guid.NewGuid(),
            //            BillOfLadingNumber = "12345" + i,
            //            BoxPartneringLineCode = "123456",
            //            BoxPartneringAgentCode = "123456",
            //            PortCodeOfOrigin = "China",
            //            PortCodeOfLoading = "China",
            //            PortCodeOfDischarge = "China",
            //            PortCodeOfDestination = "China",
            //            DateOfLoading = DateTime.Now,
            //            ManifestRegisterationNumber = "12345678",
            //            TradeCode = "I",
            //            TransShipmentMode = "I",
            //            BillOfLadingOwnerName = "ahmed razzaq",
            //            BillOfLadingOwnerAddress = "Iraq - Baghdad",
            //            CargoCode = "F",
            //            ConsolidatedCargoIndicator = "Y",
            //            StorageRequestCode = "Y",
            //            ContainerServiceType = "FCL/FCL",
            //            CountryOfOrigin = "IQ",
            //            OriginalConsigneeName = "last name",
            //            OriginalConsigneeAddress = "last address",
            //            OriginalVesselName = "Test vessel",
            //            OriginalVoyageNumber = "123456",
            //            OriginalBolNumber = "123456789",
            //            OriginalShipperName = "Shipper",
            //            OriginalShipperAddress = "Shipper address",
            //            ShipperName = "shipper name",
            //            ShipperAddress = "new shipper address",
            //            ShipperCountryCode = "IQ",
            //            ConsigneeCode = "12345",
            //            ConsigneeName = "Consignee",
            //            ConsigneeAddress = "Consignee address",
            //            Notify1Code = "123456",
            //            Notify1Name = "Notify1Name",
            //            Notify1Address = "Notify1Address",
            //            Notify2Code = "123456",
            //            Notify2Name = "Notify2Name",
            //            Notify2Address = "Notify2Address",
            //            Notify3Code = "123456",
            //            Notify3Name = "Notify3Name",
            //            Notify3Address = "Notify3Address",
            //            MarksAndNumbers = "MarksAndNumbers",
            //            CommodityCode = "1234567899",
            //            CommodityDescription = "CommodityDescription",
            //            Packages = 1,
            //            PackageType = "PKG",
            //            PackageTypeCode = "PKG",
            //            ContainerNumber = "1234567899",
            //            CheckDigit = "1",
            //            NoOfContainers = 1,
            //            NoOfTeus = 50,
            //            TotalTareWeight = 50.5,
            //            CargoWeight = 50.5,
            //            GrossWeight = 50.5,
            //            CargoVolume = 50.5,
            //            TotalQuantity = 50,
            //            FreightTonne = 50.5,
            //            NoOfPallets = 50,
            //            SlacIndicator = "I",
            //            ContractCarriageCondition = "DOS",
            //            Remarks = "Remarks"

            //        };
            //        //ContainerDetails newContainer = new ContainerDetails()
            //        //{
            //        //    ContainerDetailsId = Guid.NewGuid(),
            //        //    ContainerNo = "12345" + i,
            //        //    CheckDigit = "I",
            //        //    ISOCode = "22G1",
            //        //    TareWeight = 100.50,
            //        //    SealNumber = "1234567890",

            //        //};
            //        ConsignmentDetails newConsignment = new ConsignmentDetails()
            //        {
            //            ConsignmentDetailsId = Guid.NewGuid(),
            //            SerialNumber = 123456,
            //            MarksAndNumbers = "MarksAndNumbers",
            //            CargoDescription = "CargoDescription",
            //            UsedOrNew = "U",
            //            CommodityCode = "12345" + i,
            //            ConsignmentPackages = 5,
            //            PackageType = "PackageType",
            //            PackageTypeCode = "PKG",
            //            NumberOfPallets = 10,
            //            ConsignmentWeight = 100.50,
            //            ConsignmentValume = 100.50,
            //            DangerousGoodsIndicator = "Y",
            //            IMOClassNumber = "IMO",
            //            UnNumberOfDangerousGoods = "12345",
            //            FlashPoint = 50,
            //            UnitOfTemperatureForFlashPoint = "C",
            //            StorageRequestedForDangerousGoods = "V",
            //            RefrigerationRequired = "Y",
            //            MinimumTemperatureOfRefrigeration = -50,
            //            MaximumTemperatureOfRefrigeration = 50,
            //            UnitOfTemperatureForRegrigeration = "C"

            //        };
            //        VehicleDetails newVehicle = new VehicleDetails()
            //        {
            //            VehicleDetailsId = Guid.NewGuid(),
            //            VehicleEquipmentIndicator = "V",
            //            UsedOrNew = "U",
            //            ChasisNumber = "1234" + i,
            //            CaseNumber = "123456789",
            //            Make = "Toyota",
            //            Model = "Corola",
            //            EngineNumber = "123456789",
            //            YearBuilt = "2019",
            //            Color = "White",
            //            RollingOrStatic = "R",
            //            DescriptionOfGood = "DescriptionOfGood",
            //            AdditionalAccesseries = "AdditionalAccesseries",
            //            WeightInKg = 100.50,
            //            Volume = 100.50,
            //            Remarks = "Remarks"
            //        };
            //        newConsignment.VehicleDetails.Add(newVehicle);
            //        //newBol.ConsignmentDetails.Add(newConsignment);
            //        //newContainer.ConsignmentDetails.Add(newConsignment);
            //        newBol.ConsignmentDetails.Add(newConsignment);
            //        ContainerDetails newContainer1 = new ContainerDetails()
            //        {
            //            ContainerDetailsId = Guid.NewGuid(),
            //            ContainerNo = "1234" + i,
            //            CheckDigit = "I",
            //            ISOCode = "22G1",
            //            TareWeight = 100.50,
            //            SealNumber = "1234567891",

            //        };

            //        newBol.ContainerDetails.Add(newContainer1);
            //        //newBol.VehicleDetails.Add(newVehicle);
            //        newVoyage.BOLDetails.Add(newBol);
            //    }





            //    ////voyage
            //    //VoyageSerializer serializer = new VoyageSerializer();
            //    //var result = serializer.Serialize(newVoyage);

            //    //VoyageParser parser = new VoyageParser();
            //    //var paresed = VoyageParser.Parse(result);

            //    ////vehicle
            //    //VehicleSerializer vehicleSerializer = new VehicleSerializer();
            //    //var vehicleSerialized = vehicleSerializer.Serialize(newVehicle);
            //    //VehicleDetails parsedVehicle = VehicleParser.Parse(vehicleSerialized);

            //    ////container
            //    //ContainerSerializer containerSerlializer = new ContainerSerializer();
            //    //var serializedContainer = containerSerlializer.Serialize(newContainer);
            //    //var parsedContainer = ContainerParser.Parse(serializedContainer);

            //    ////consignment
            //    //ConsignmentSerializer consignmentSerializer = new ConsignmentSerializer();
            //    //var serializedConsignment = consignmentSerializer.Serialize(newConsignment);
            //    //var parsedConsignment = ConsignmentParser.Parse(serializedConsignment);




            //    ////bol
            //    //BolSerializer bolSerializer = new BolSerializer();
            //    //var serializedBol = bolSerializer.Serialize(newBol);
            //    //var parsedBol = BolParser.Parse(serializedBol);

            //    //full serializer
            //    EmanifestSerializer manifestSerializer = new EmanifestSerializer(ManifestFileType.FCLContainerRelationNotknown);
            //    var serializerManifest = manifestSerializer.SerializeToString(newVoyage);
            //    manifestSerializer.SerializeTofile("FCLContainersNoRelation.txt", newVoyage);
            //    EmanifestParser.EmanifestParser manifestParser = new EmanifestParser.EmanifestParser(File.Open("text.txt", FileMode.Open));
            //    var parsedVoyage = manifestParser.ParseString(serializerManifest);

            //    parsedVoyage = manifestParser.ParseStream();

            //    db.VoyageDetails.Add(newVoyage);
            //    db.BulkSaveChanges();

            //}

        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://ahmedrz-002-site5.htempurl.com/");
            client.Timeout = TimeSpan.FromMinutes(5);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        static async Task<List<VoyageDetails>> GetVoyageAsync(string path)
        {
            List<VoyageDetails> voyages = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                voyages = await response.Content.ReadAsAsync<List<VoyageDetails>>();
            }
            return voyages;
        }

        public static void CreateGeneralCargoManifestFile()
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                //var test = db.VoyageDetails.Find(new Guid("08ffb8cd-ffc6-4717-9c85-4daf54101a40"));
                //EmanifestSerializer s = new EmanifestSerializer(ManifestFileType.FCLContainer);
                //var ss = s.SerializeToString(test);
                var manifestType = ManifestFileType.GeneralCargo.ToString();
                VoyageDetails newVoyage = new VoyageDetails()
                {
                    VoyageDetailsId = Guid.NewGuid(),
                    VesselName = "test",
                    LineCode = GetRandomLines(),
                    VoyageAgentCode = "Agent",
                    AgentVoyageNumber = "1",
                    PortCodeOfDischarge = "IQUQR",
                    ExpectedToArriveDate = DateTime.Now,
                    MessageType = "MFI",
                    NumberOfInstalment = 1,
                    ManifestType = manifestType
                };
                for (int i = 0; i < 2000; i++)
                {
                    BOLDetails newBol = new BOLDetails()
                    {
                        BOLDetailsId = Guid.NewGuid(),
                        BillOfLadingNumber = i.ToString("D8"),
                        BoxPartneringLineCode = GetRandomLines(),
                        BoxPartneringAgentCode = "Agent",
                        PortCodeOfOrigin = GetRandomLocationCode(),
                        PortCodeOfLoading = GetRandomLocationCode(),
                        PortCodeOfDischarge = GetRandomLocationCode(),
                        PortCodeOfDestination = GetRandomLocationCode(),
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
                        ShipperCountryCode = GetRandomCountryCode(),
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
                        CommodityCode = GetRandomHsCode(),
                        CommodityDescription = "CommodityDescription",
                        Packages = 1,
                        PackageType = "PACKAGES",
                        PackageTypeCode = GetRandomPackageCode(),
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
                    ConsignmentDetails newConsignment = new ConsignmentDetails()
                    {
                        ConsignmentDetailsId = Guid.NewGuid(),
                        SerialNumber = 1,
                        MarksAndNumbers = "MarksAndNumbers",
                        CargoDescription = "CargoDescription",
                        UsedOrNew = "U",
                        CommodityCode = GetRandomHsCode(),
                        ConsignmentPackages = 5,
                        PackageType = "PackageType",
                        PackageTypeCode = "PKG",
                        NumberOfPallets = 10,
                        ConsignmentWeight = 100.50,
                        ConsignmentValume = 100.50,
                        DangerousGoodsIndicator = "Y",
                        IMOClassNumber = "1.3",
                        UnNumberOfDangerousGoods = GetRandomHazardousCode(),
                        FlashPoint = 50,
                        UnitOfTemperatureForFlashPoint = "C",
                        StorageRequestedForDangerousGoods = "D",
                        RefrigerationRequired = "Y",
                        MinimumTemperatureOfRefrigeration = -50,
                        MaximumTemperatureOfRefrigeration = 50,
                        UnitOfTemperatureForRegrigeration = "C"

                    };
                    VehicleDetails newVehicle = new VehicleDetails()
                    {
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
                    newBol.ConsignmentDetails.Add(newConsignment);
                    newVoyage.BOLDetails.Add(newBol);
                }


                //full serializer
                EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails> manifestSerializer = new EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(ManifestFileType.GeneralCargo);
                var serializerManifest = manifestSerializer.SerializeToString(newVoyage);
                manifestSerializer.SerializeTofile("GeneralCargo.txt", newVoyage);
                EmanifestParser.EmanifestParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails> manifestParser = new EmanifestParser.EmanifestParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(File.Open("GeneralCargo.txt", FileMode.Open));
                var parsedVoyage = manifestParser.ParseString(serializerManifest);

                parsedVoyage = manifestParser.ParseStream();

                //db.VoyageDetails.Add(newVoyage);
                //db.BulkSaveChanges();

            }
        }
        public static void CreateFCLNoRelationManifestFile()
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                //var test = db.VoyageDetails.Find(new Guid("08ffb8cd-ffc6-4717-9c85-4daf54101a40"));
                //EmanifestSerializer s = new EmanifestSerializer(ManifestFileType.FCLContainer);
                //var ss = s.SerializeToString(test);
                var manifestType = ManifestFileType.FCLContainerRelationNotknown.ToString();
                VoyageDetails newVoyage = new VoyageDetails()
                {
                    VoyageDetailsId = Guid.NewGuid(),
                    VesselName = "test",
                    LineCode = GetRandomLines(),
                    VoyageAgentCode = "Agent",
                    AgentVoyageNumber = "1",
                    PortCodeOfDischarge = "IQUQR",
                    ExpectedToArriveDate = DateTime.Now,
                    MessageType = "MFI",
                    NumberOfInstalment = 1,
                    ManifestType = manifestType
                };
                for (int i = 0; i < 2000; i++)
                {
                    BOLDetails newBol = new BOLDetails()
                    {
                        BOLDetailsId = Guid.NewGuid(),
                        BillOfLadingNumber = i.ToString("D8"),
                        BoxPartneringLineCode = GetRandomLines(),
                        BoxPartneringAgentCode = "Agent",
                        PortCodeOfOrigin = GetRandomLocationCode(),
                        PortCodeOfLoading = GetRandomLocationCode(),
                        PortCodeOfDischarge = GetRandomLocationCode(),
                        PortCodeOfDestination = GetRandomLocationCode(),
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
                        ShipperCountryCode = GetRandomCountryCode(),
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
                        CommodityCode = GetRandomHsCode(),
                        CommodityDescription = "CommodityDescription",
                        Packages = 1,
                        PackageType = "PACKAGES",
                        PackageTypeCode = GetRandomPackageCode(),
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
                    ConsignmentDetails newConsignment = new ConsignmentDetails()
                    {
                        ConsignmentDetailsId = Guid.NewGuid(),
                        SerialNumber = 1,
                        MarksAndNumbers = "MarksAndNumbers",
                        CargoDescription = "CargoDescription",
                        UsedOrNew = "U",
                        CommodityCode = GetRandomHsCode(),
                        ConsignmentPackages = 5,
                        PackageType = "PackageType",
                        PackageTypeCode = "PKG",
                        NumberOfPallets = 10,
                        ConsignmentWeight = 100.50,
                        ConsignmentValume = 100.50,
                        DangerousGoodsIndicator = "Y",
                        IMOClassNumber = "1.3",
                        UnNumberOfDangerousGoods = GetRandomHazardousCode(),
                        FlashPoint = 50,
                        UnitOfTemperatureForFlashPoint = "C",
                        StorageRequestedForDangerousGoods = "D",
                        RefrigerationRequired = "Y",
                        MinimumTemperatureOfRefrigeration = -50,
                        MaximumTemperatureOfRefrigeration = 50,
                        UnitOfTemperatureForRegrigeration = "C"

                    };
                    ConsignmentDetails newConsignment1 = new ConsignmentDetails()
                    {
                        ConsignmentDetailsId = Guid.NewGuid(),
                        SerialNumber = 1,
                        MarksAndNumbers = "MarksAndNumbers",
                        CargoDescription = "CargoDescription",
                        UsedOrNew = "U",
                        CommodityCode = GetRandomHsCode(),
                        ConsignmentPackages = 5,
                        PackageType = "PackageType",
                        PackageTypeCode = "PKG",
                        NumberOfPallets = 10,
                        ConsignmentWeight = 100.50,
                        ConsignmentValume = 100.50,
                        DangerousGoodsIndicator = "Y",
                        IMOClassNumber = "1.3",
                        UnNumberOfDangerousGoods = GetRandomHazardousCode(),
                        FlashPoint = 50,
                        UnitOfTemperatureForFlashPoint = "C",
                        StorageRequestedForDangerousGoods = "D",
                        RefrigerationRequired = "Y",
                        MinimumTemperatureOfRefrigeration = -50,
                        MaximumTemperatureOfRefrigeration = 50,
                        UnitOfTemperatureForRegrigeration = "C"

                    };
                    VehicleDetails newVehicle = new VehicleDetails()
                    {
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

                    ContainerDetails newContainer = new ContainerDetails()
                    {
                        ContainerDetailsId = Guid.NewGuid(),
                        ContainerNo = "1234" + i,
                        CheckDigit = "I",
                        ISOCode = GetRandomIsoCode(),
                        TareWeight = 100.50,
                        SealNumber = "1234567891",

                    };
                    newConsignment.VehicleDetails.Add(newVehicle);
                    newBol.ConsignmentDetails.Add(newConsignment);
                    newBol.ConsignmentDetails.Add(newConsignment1);
                    newBol.ContainerDetails.Add(newContainer);
                    newVoyage.BOLDetails.Add(newBol);
                }


                //full serializer
                EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails> manifestSerializer = new EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(ManifestFileType.FCLContainerRelationNotknown);
                var serializerManifest = manifestSerializer.SerializeToString(newVoyage);
                manifestSerializer.SerializeTofile("FCLNoRelation.txt", newVoyage);
                EmanifestParser.EmanifestParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails> manifestParser = new EmanifestParser.EmanifestParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(File.Open("FCLNoRelation.txt", FileMode.Open));
                var parsedVoyage = manifestParser.ParseString(serializerManifest);

                parsedVoyage = manifestParser.ParseStream();

                //db.VoyageDetails.Add(newVoyage);
                //db.BulkSaveChanges();

            }
        }
        public static void CreateFCLManifestFile()
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                //var test = db.VoyageDetails.Find(new Guid("08ffb8cd-ffc6-4717-9c85-4daf54101a40"));
                //EmanifestSerializer s = new EmanifestSerializer(ManifestFileType.FCLContainer);
                //var ss = s.SerializeToString(test);
                var manifestType = ManifestFileType.FCLContainer.ToString();
                VoyageDetails newVoyage = new VoyageDetails()
                {
                    VoyageDetailsId = Guid.NewGuid(),
                    VesselName = "test",
                    LineCode = GetRandomLines(),
                    VoyageAgentCode = "Agent",
                    AgentVoyageNumber = "1",
                    PortCodeOfDischarge = "IQUQR",
                    ExpectedToArriveDate = DateTime.Now,
                    MessageType = "MFI",
                    NumberOfInstalment = 1,
                    ManifestType = manifestType
                };
                for (int i = 0; i < 2000; i++)
                {
                    BOLDetails newBol = new BOLDetails()
                    {
                        BOLDetailsId = Guid.NewGuid(),
                        BillOfLadingNumber = i.ToString("D8"),
                        BoxPartneringLineCode = GetRandomLines(),
                        BoxPartneringAgentCode = "Agent",
                        PortCodeOfOrigin = GetRandomLocationCode(),
                        PortCodeOfLoading = GetRandomLocationCode(),
                        PortCodeOfDischarge = GetRandomLocationCode(),
                        PortCodeOfDestination = GetRandomLocationCode(),
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
                        ShipperCountryCode = GetRandomCountryCode(),
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
                        CommodityCode = GetRandomHsCode(),
                        CommodityDescription = "CommodityDescription",
                        Packages = 1,
                        PackageType = "PACKAGES",
                        PackageTypeCode = GetRandomPackageCode(),
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
                    ConsignmentDetails newConsignment = new ConsignmentDetails()
                    {
                        ConsignmentDetailsId = Guid.NewGuid(),
                        SerialNumber = 1,
                        MarksAndNumbers = "MarksAndNumbers",
                        CargoDescription = "CargoDescription",
                        UsedOrNew = "U",
                        CommodityCode = GetRandomHsCode(),
                        ConsignmentPackages = 5,
                        PackageType = "PackageType",
                        PackageTypeCode = "PKG",
                        NumberOfPallets = 10,
                        ConsignmentWeight = 100.50,
                        ConsignmentValume = 100.50,
                        DangerousGoodsIndicator = "Y",
                        IMOClassNumber = "1.3",
                        UnNumberOfDangerousGoods = GetRandomHazardousCode(),
                        FlashPoint = 50,
                        UnitOfTemperatureForFlashPoint = "C",
                        StorageRequestedForDangerousGoods = "D",
                        RefrigerationRequired = "Y",
                        MinimumTemperatureOfRefrigeration = -50,
                        MaximumTemperatureOfRefrigeration = 50,
                        UnitOfTemperatureForRegrigeration = "C"

                    };
                    ConsignmentDetails newConsignment1 = new ConsignmentDetails()
                    {
                        ConsignmentDetailsId = Guid.NewGuid(),
                        SerialNumber = 1,
                        MarksAndNumbers = "MarksAndNumbers",
                        CargoDescription = "CargoDescription",
                        UsedOrNew = "U",
                        CommodityCode = GetRandomHsCode(),
                        ConsignmentPackages = 5,
                        PackageType = "PackageType",
                        PackageTypeCode = "PKG",
                        NumberOfPallets = 10,
                        ConsignmentWeight = 100.50,
                        ConsignmentValume = 100.50,
                        DangerousGoodsIndicator = "Y",
                        IMOClassNumber = "1.3",
                        UnNumberOfDangerousGoods = GetRandomHazardousCode(),
                        FlashPoint = 50,
                        UnitOfTemperatureForFlashPoint = "C",
                        StorageRequestedForDangerousGoods = "D",
                        RefrigerationRequired = "Y",
                        MinimumTemperatureOfRefrigeration = -50,
                        MaximumTemperatureOfRefrigeration = 50,
                        UnitOfTemperatureForRegrigeration = "C"

                    };
                    VehicleDetails newVehicle = new VehicleDetails()
                    {
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

                    ContainerDetails newContainer = new ContainerDetails()
                    {
                        ContainerDetailsId = Guid.NewGuid(),
                        ContainerNo = "1234" + i,
                        CheckDigit = "I",
                        ISOCode = GetRandomIsoCode(),
                        TareWeight = 100.50,
                        SealNumber = "1234567891",
                        ContainerOwnerType = "COC"

                    };
                    newConsignment.VehicleDetails.Add(newVehicle);
                    newContainer.ConsignmentDetails.Add(newConsignment);
                    newContainer.ConsignmentDetails.Add(newConsignment1);
                    newBol.ContainerDetails.Add(newContainer);
                    newVoyage.BOLDetails.Add(newBol);
                }


                //full serializer
                EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails> manifestSerializer = new EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(ManifestFileType.FCLContainer);
                var serializerManifest = manifestSerializer.SerializeToString(newVoyage);
                manifestSerializer.SerializeTofile("FCL.txt", newVoyage);
                EmanifestParser.EmanifestParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails> manifestParser = new EmanifestParser.EmanifestParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(File.Open("FCL.txt", FileMode.Open));
                var parsedVoyage = manifestParser.ParseString(serializerManifest);

                parsedVoyage = manifestParser.ParseStream();

                //db.VoyageDetails.Add(newVoyage);
                //db.BulkSaveChanges();

            }
        }
        private static string GetRandomHsCode()
        {
            Random r = new Random();
            var index = r.Next(0, HSCODES.Count);
            return HSCODES[index].Code;
        }
        private static string GetRandomHazardousCode()
        {
            Random r = new Random();
            var index = r.Next(0, HAZARDOUSCODES.Count);
            return HAZARDOUSCODES[index].Code;
        }
        private static string GetRandomPackageCode()
        {
            Random r = new Random();
            var index = r.Next(0, PACKAGES.Count);
            return PACKAGES[index].PackageCode;
        }
        private static string GetRandomIsoCode()
        {
            Random r = new Random();
            var index = r.Next(0, ISOCODES.Count);
            return ISOCODES[index].IsoTypeCode;
        }
        private static string GetRandomCountryCode()
        {
            Random r = new Random();
            var index = r.Next(0, COUNTRYCODES.Count);
            return COUNTRYCODES[index].CountryCode;
        }
        private static string GetRandomLocationCode()
        {
            Random r = new Random();
            var index = r.Next(0, LOCATIONCODES.Count);
            return LOCATIONCODES[index].FullCode;
        }
        private static string GetRandomLines()
        {
            Random r = new Random();
            var index = r.Next(0, LINES.Count);
            return LINES[index].LineCode;
        }
        public static void CreateLCLManifestFile()
        {

            using (EmanifestContext db = new EmanifestContext())
            {
                //var test = db.VoyageDetails.Find(new Guid("08ffb8cd-ffc6-4717-9c85-4daf54101a40"));
                //EmanifestSerializer s = new EmanifestSerializer(ManifestFileType.FCLContainer);
                //var ss = s.SerializeToString(test);
                var manifestType = ManifestFileType.LCLContainer.ToString();
                VoyageDetails newVoyage = new VoyageDetails()
                {
                    VoyageDetailsId = Guid.NewGuid(),
                    VesselName = "test",
                    LineCode = GetRandomLines(),
                    VoyageAgentCode = "Agent",
                    AgentVoyageNumber = "1",
                    PortCodeOfDischarge = "IQUQR",
                    ExpectedToArriveDate = DateTime.Now,
                    MessageType = "MFI",
                    NumberOfInstalment = 1,
                    ManifestType = manifestType
                };
                for (int i = 0; i < 2000; i++)
                {
                    BOLDetails newBol = new BOLDetails()
                    {
                        BOLDetailsId = Guid.NewGuid(),
                        BillOfLadingNumber = i.ToString("D8"),
                        BoxPartneringLineCode = GetRandomLines(),
                        BoxPartneringAgentCode = "Agent",
                        PortCodeOfOrigin = GetRandomLocationCode(),
                        PortCodeOfLoading = GetRandomLocationCode(),
                        PortCodeOfDischarge = GetRandomLocationCode(),
                        PortCodeOfDestination = GetRandomLocationCode(),
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
                        ShipperCountryCode = GetRandomCountryCode(),
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
                        CommodityCode = GetRandomHsCode(),
                        CommodityDescription = "CommodityDescription",
                        Packages = 1,
                        PackageType = "PACKAGES",
                        PackageTypeCode = GetRandomPackageCode(),
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
                    ConsignmentDetails newConsignment = new ConsignmentDetails()
                    {
                        ConsignmentDetailsId = Guid.NewGuid(),
                        SerialNumber = 1,
                        MarksAndNumbers = "MarksAndNumbers",
                        CargoDescription = "CargoDescription",
                        UsedOrNew = "U",
                        CommodityCode = GetRandomHsCode(),
                        ConsignmentPackages = 5,
                        PackageType = "PackageType",
                        PackageTypeCode = "PKG",
                        NumberOfPallets = 10,
                        ConsignmentWeight = 100.50,
                        ConsignmentValume = 100.50,
                        DangerousGoodsIndicator = "Y",
                        IMOClassNumber = "1.3",
                        UnNumberOfDangerousGoods = GetRandomHazardousCode(),
                        FlashPoint = 50,
                        UnitOfTemperatureForFlashPoint = "C",
                        StorageRequestedForDangerousGoods = "D",
                        RefrigerationRequired = "Y",
                        MinimumTemperatureOfRefrigeration = -50,
                        MaximumTemperatureOfRefrigeration = 50,
                        UnitOfTemperatureForRegrigeration = "C"

                    };
                    ConsignmentDetails newConsignment1 = new ConsignmentDetails()
                    {
                        ConsignmentDetailsId = Guid.NewGuid(),
                        SerialNumber = 1,
                        MarksAndNumbers = "MarksAndNumbers",
                        CargoDescription = "CargoDescription",
                        UsedOrNew = "U",
                        CommodityCode = GetRandomHsCode(),
                        ConsignmentPackages = 5,
                        PackageType = "PackageType",
                        PackageTypeCode = GetRandomPackageCode(),
                        NumberOfPallets = 10,
                        ConsignmentWeight = 100.50,
                        ConsignmentValume = 100.50,
                        DangerousGoodsIndicator = "Y",
                        IMOClassNumber = "1.3",
                        UnNumberOfDangerousGoods = GetRandomHazardousCode(),
                        FlashPoint = 50,
                        UnitOfTemperatureForFlashPoint = "C",
                        StorageRequestedForDangerousGoods = "D",
                        RefrigerationRequired = "Y",
                        MinimumTemperatureOfRefrigeration = -50,
                        MaximumTemperatureOfRefrigeration = 50,
                        UnitOfTemperatureForRegrigeration = "C"

                    };

                    ContainerDetails newContainer = new ContainerDetails()
                    {
                        ContainerDetailsId = Guid.NewGuid(),
                        ContainerNo = "1234" + i,
                        CheckDigit = "5",
                        ISOCode = GetRandomIsoCode(),
                        TareWeight = 100.50,
                        SealNumber = "1234567891",

                    };
                    newContainer.ConsignmentDetails.Add(newConsignment);
                    newContainer.ConsignmentDetails.Add(newConsignment1);
                    newBol.ContainerDetails.Add(newContainer);
                    newVoyage.BOLDetails.Add(newBol);
                }


                //full serializer
                EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails> manifestSerializer = new EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(ManifestFileType.LCLContainer);
                var serializerManifest = manifestSerializer.SerializeToString(newVoyage);
                manifestSerializer.SerializeTofile("LCL.txt", newVoyage);
                EmanifestParser.EmanifestParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails> manifestParser = new EmanifestParser.EmanifestParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(File.Open("LCL.txt", FileMode.Open));
                var parsedVoyage = manifestParser.ParseString(serializerManifest);

                parsedVoyage = manifestParser.ParseStream();

                //db.VoyageDetails.Add(newVoyage);
                //db.BulkSaveChanges();

            }
        }


    }
}
