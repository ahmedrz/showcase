namespace PortSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BOLDetails",
                c => new
                    {
                        BOLDetailsId = c.Guid(nullable: false),
                        BillOfLadingNumber = c.String(nullable: false, maxLength: 20),
                        BoxPartneringLineCode = c.String(nullable: false, maxLength: 30),
                        BoxPartneringAgentCode = c.String(nullable: false, maxLength: 30),
                        PortCodeOfOrigin = c.String(nullable: false, maxLength: 5),
                        PortCodeOfLoading = c.String(nullable: false, maxLength: 5),
                        PortCodeOfDischarge = c.String(nullable: false, maxLength: 5),
                        PortCodeOfDestination = c.String(nullable: false, maxLength: 5),
                        DateOfLoading = c.DateTime(),
                        ManifestRegisterationNumber = c.String(maxLength: 8),
                        TradeCode = c.String(nullable: false, maxLength: 1),
                        TransShipmentMode = c.String(maxLength: 1),
                        BillOfLadingOwnerName = c.String(maxLength: 30),
                        BillOfLadingOwnerAddress = c.String(maxLength: 240),
                        CargoCode = c.String(nullable: false, maxLength: 1),
                        ConsolidatedCargoIndicator = c.String(nullable: false, maxLength: 1),
                        StorageRequestCode = c.String(maxLength: 1),
                        ContainerServiceType = c.String(maxLength: 7),
                        CountryOfOrigin = c.String(nullable: false, maxLength: 2),
                        OriginalConsigneeName = c.String(maxLength: 30),
                        OriginalConsigneeAddress = c.String(maxLength: 240),
                        OriginalVesselName = c.String(maxLength: 30),
                        OriginalVoyageNumber = c.String(maxLength: 10),
                        OriginalBolNumber = c.String(maxLength: 20),
                        OriginalShipperName = c.String(maxLength: 30),
                        OriginalShipperAddress = c.String(maxLength: 240),
                        ShipperName = c.String(nullable: false, maxLength: 30),
                        ShipperAddress = c.String(nullable: false, maxLength: 240),
                        ShipperCountryCode = c.String(nullable: false, maxLength: 2),
                        ConsigneeCode = c.String(maxLength: 5),
                        ConsigneeName = c.String(nullable: false, maxLength: 48),
                        ConsigneeAddress = c.String(nullable: false, maxLength: 240),
                        Notify1Code = c.String(maxLength: 6),
                        Notify1Name = c.String(nullable: false, maxLength: 48),
                        Notify1Address = c.String(nullable: false, maxLength: 240),
                        Notify2Code = c.String(maxLength: 6),
                        Notify2Name = c.String(maxLength: 48),
                        Notify2Address = c.String(maxLength: 240),
                        Notify3Code = c.String(maxLength: 6),
                        Notify3Name = c.String(maxLength: 48),
                        Notify3Address = c.String(maxLength: 240),
                        MarksAndNumbers = c.String(nullable: false, maxLength: 200),
                        CommodityCode = c.String(nullable: false, maxLength: 10),
                        CommodityDescription = c.String(nullable: false, maxLength: 100),
                        Packages = c.Int(nullable: false),
                        PackageType = c.String(nullable: false, maxLength: 30),
                        PackageTypeCode = c.String(nullable: false, maxLength: 3),
                        ContainerNumber = c.String(maxLength: 10),
                        CheckDigit = c.String(maxLength: 1),
                        NoOfContainers = c.Int(),
                        NoOfTeus = c.Int(),
                        TotalTareWeight = c.Double(),
                        CargoWeight = c.Double(nullable: false),
                        GrossWeight = c.Double(nullable: false),
                        CargoVolume = c.Double(),
                        TotalQuantity = c.Int(),
                        FreightTonne = c.Double(),
                        NoOfPallets = c.Int(),
                        SlacIndicator = c.String(maxLength: 1),
                        ContractCarriageCondition = c.String(maxLength: 3),
                        Remarks = c.String(maxLength: 200),
                        VoyageDetailsId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.BOLDetailsId)
                .ForeignKey("dbo.VoyageDetails", t => t.VoyageDetailsId, cascadeDelete: true)
                .Index(t => t.VoyageDetailsId);
            
            CreateTable(
                "dbo.ConsignmentDetails",
                c => new
                    {
                        ConsignmentDetailsId = c.Guid(nullable: false),
                        SerialNumber = c.Int(nullable: false),
                        MarksAndNumbers = c.String(nullable: false, maxLength: 200),
                        CargoDescription = c.String(nullable: false, maxLength: 100),
                        UsedOrNew = c.String(maxLength: 1),
                        CommodityCode = c.String(nullable: false, maxLength: 10),
                        ConsignmentPackages = c.Int(nullable: false),
                        PackageType = c.String(nullable: false, maxLength: 30),
                        PackageTypeCode = c.String(nullable: false, maxLength: 3),
                        NumberOfPallets = c.Int(nullable: false),
                        ConsignmentWeight = c.Double(nullable: false),
                        ConsignmentValume = c.Double(nullable: false),
                        DangerousGoodsIndicator = c.String(nullable: false, maxLength: 1),
                        IMOClassNumber = c.String(maxLength: 3),
                        UnNumberOfDangerousGoods = c.String(maxLength: 5),
                        FlashPoint = c.Double(),
                        UnitOfTemperatureForFlashPoint = c.String(maxLength: 1),
                        StorageRequestedForDangerousGoods = c.String(maxLength: 1),
                        RefrigerationRequired = c.String(nullable: false, maxLength: 1),
                        MinimumTemperatureOfRefrigeration = c.Double(),
                        MaximumTemperatureOfRefrigeration = c.Double(),
                        UnitOfTemperatureForRegrigeration = c.String(maxLength: 1),
                        ContainerDetailsId = c.Guid(),
                        BOLDetailsId = c.Guid(),
                    })
                .PrimaryKey(t => t.ConsignmentDetailsId)
                .ForeignKey("dbo.BOLDetails", t => t.BOLDetailsId)
                .ForeignKey("dbo.ContainerDetails", t => t.ContainerDetailsId)
                .Index(t => t.ContainerDetailsId)
                .Index(t => t.BOLDetailsId);
            
            CreateTable(
                "dbo.ContainerDetails",
                c => new
                    {
                        ContainerDetailsId = c.Guid(nullable: false),
                        ContainerNo = c.String(nullable: false, maxLength: 10),
                        CheckDigit = c.String(nullable: false, maxLength: 1),
                        ISOCode = c.String(nullable: false, maxLength: 4),
                        TareWeight = c.Double(nullable: false),
                        SealNumber = c.String(nullable: false, maxLength: 10),
                        BOLDetailsId = c.Guid(),
                    })
                .PrimaryKey(t => t.ContainerDetailsId)
                .ForeignKey("dbo.BOLDetails", t => t.BOLDetailsId)
                .Index(t => t.BOLDetailsId);
            
            CreateTable(
                "dbo.VehicleDetails",
                c => new
                    {
                        VehicleDetailsId = c.Guid(nullable: false),
                        SerialNumber = c.Int(nullable: false),
                        VehicleEquipmentIndicator = c.String(nullable: false, maxLength: 1),
                        UsedOrNew = c.String(nullable: false, maxLength: 1),
                        ChasisNumber = c.String(maxLength: 24),
                        CaseNumber = c.String(maxLength: 24),
                        Make = c.String(nullable: false, maxLength: 20),
                        Model = c.String(nullable: false, maxLength: 20),
                        EngineNumber = c.String(maxLength: 30),
                        YearBuilt = c.String(maxLength: 4),
                        Color = c.String(maxLength: 16),
                        RollingOrStatic = c.String(nullable: false, maxLength: 1),
                        DescriptionOfGood = c.String(nullable: false, maxLength: 200),
                        AdditionalAccesseries = c.String(maxLength: 100),
                        WeightInKg = c.Double(),
                        Volume = c.Double(),
                        Remarks = c.String(maxLength: 200),
                        ConsignmentDetailsId = c.Guid(),
                    })
                .PrimaryKey(t => t.VehicleDetailsId)
                .ForeignKey("dbo.ConsignmentDetails", t => t.ConsignmentDetailsId)
                .Index(t => t.ConsignmentDetailsId);
            
            CreateTable(
                "dbo.VoyageDetails",
                c => new
                    {
                        VoyageDetailsId = c.Guid(nullable: false),
                        LineCode = c.String(nullable: false, maxLength: 30),
                        VoyageAgentCode = c.String(nullable: false, maxLength: 30),
                        VesselName = c.String(nullable: false, maxLength: 30),
                        AgentVoyageNumber = c.String(nullable: false, maxLength: 10),
                        PortCodeOfDischarge = c.String(nullable: false, maxLength: 5),
                        ExpectedToArriveDate = c.DateTime(nullable: false),
                        RotationNumber = c.String(maxLength: 6),
                        MessageType = c.String(nullable: false, maxLength: 3),
                        NumberOfInstalment = c.Int(nullable: false),
                        AgentManifestSequenceNumber = c.Int(),
                        ManifestType = c.String(),
                        CarrierId = c.Guid(),
                        StatusId = c.Guid(),
                        UserId = c.Guid(),
                        UploadDate = c.DateTime(),
                        FileName = c.String(),
                        VesselId = c.Guid(),
                    })
                .PrimaryKey(t => t.VoyageDetailsId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Vessels", t => t.VesselId)
                .ForeignKey("dbo.Carriers", t => t.CarrierId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.CarrierId)
                .Index(t => t.StatusId)
                .Index(t => t.UserId)
                .Index(t => t.VesselId);
            
            CreateTable(
                "dbo.Carriers",
                c => new
                    {
                        CarrierId = c.Guid(nullable: false),
                        CarrierName = c.String(maxLength: 200),
                        Email = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 50),
                        IsActive = c.Boolean(),
                        Info = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.CarrierId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        UserName = c.String(maxLength: 10),
                        Email = c.String(maxLength: 100),
                        Password = c.String(maxLength: 10),
                        IsActive = c.Boolean(),
                        CarrierId = c.Guid(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Carriers", t => t.CarrierId)
                .Index(t => t.CarrierId);
            
            CreateTable(
                "dbo.Vessels",
                c => new
                    {
                        VesselId = c.Guid(nullable: false),
                        VesselName = c.String(nullable: false, maxLength: 30),
                        GRT = c.Double(nullable: false),
                        VesselCountry = c.String(nullable: false, maxLength: 30),
                        IMONo = c.String(),
                        CallSign = c.String(),
                        LOA = c.Double(),
                        LWL = c.Double(),
                        DraftAFT = c.Double(),
                        DraftFWD = c.Double(),
                        CarrierId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.VesselId)
                .ForeignKey("dbo.Carriers", t => t.CarrierId, cascadeDelete: true)
                .Index(t => t.CarrierId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.Guid(nullable: false),
                        StatusDesc = c.String(maxLength: 300),
                        AllowModify = c.Boolean(),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "dbo.ContainerIsoCodes",
                c => new
                    {
                        IsoTypeCode = c.String(nullable: false, maxLength: 4),
                        IsoTypeDescription = c.String(nullable: false, maxLength: 60),
                        Serial = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IsoTypeCode);
            
            CreateTable(
                "dbo.CountryCodes",
                c => new
                    {
                        CountryCodeId = c.Int(nullable: false),
                        CountryCode = c.String(nullable: false, maxLength: 2),
                        CountryName = c.String(nullable: false, maxLength: 255),
                        Serial = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CountryCodeId);
            
            CreateTable(
                "dbo.Lines",
                c => new
                    {
                        LineCode = c.String(nullable: false, maxLength: 6),
                        LineName = c.String(nullable: false, maxLength: 30),
                        Serial = c.Int(nullable: false),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.LineCode);
            
            CreateTable(
                "dbo.LocationCodes",
                c => new
                    {
                        LocationCodeId = c.Int(nullable: false),
                        CountryCode = c.String(nullable: false, maxLength: 2),
                        LocationCode = c.String(maxLength: 5),
                        FullCode = c.String(),
                        LocationName = c.String(nullable: false, maxLength: 255),
                        Function = c.String(maxLength: 16),
                        Serial = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocationCodeId);
            
            CreateTable(
                "dbo.PackageCodes",
                c => new
                    {
                        PackageCodeId = c.Int(nullable: false),
                        PackageCode = c.String(nullable: false, maxLength: 3),
                        PackageDescription = c.String(nullable: false, maxLength: 20),
                        Serial = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PackageCodeId);
            
            CreateTable(
                "dbo.UNHazardousCodes",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 4),
                        Description = c.String(nullable: false, maxLength: 150),
                        Class = c.String(nullable: false, maxLength: 4),
                        Serial = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.UNHSCodes",
                c => new
                    {
                        UNHSCodeId = c.Int(nullable: false),
                        Classification = c.String(maxLength: 2),
                        Code = c.String(maxLength: 10),
                        Description = c.String(nullable: false, maxLength: 255),
                        ParentCode = c.String(maxLength: 10),
                        Level = c.Int(nullable: false),
                        IsLeaf = c.Boolean(nullable: false),
                        Serial = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UNHSCodeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VoyageDetails", "StatusId", "dbo.Status");
            DropForeignKey("dbo.VoyageDetails", "CarrierId", "dbo.Carriers");
            DropForeignKey("dbo.VoyageDetails", "VesselId", "dbo.Vessels");
            DropForeignKey("dbo.Vessels", "CarrierId", "dbo.Carriers");
            DropForeignKey("dbo.VoyageDetails", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CarrierId", "dbo.Carriers");
            DropForeignKey("dbo.BOLDetails", "VoyageDetailsId", "dbo.VoyageDetails");
            DropForeignKey("dbo.VehicleDetails", "ConsignmentDetailsId", "dbo.ConsignmentDetails");
            DropForeignKey("dbo.ConsignmentDetails", "ContainerDetailsId", "dbo.ContainerDetails");
            DropForeignKey("dbo.ContainerDetails", "BOLDetailsId", "dbo.BOLDetails");
            DropForeignKey("dbo.ConsignmentDetails", "BOLDetailsId", "dbo.BOLDetails");
            DropIndex("dbo.Vessels", new[] { "CarrierId" });
            DropIndex("dbo.Users", new[] { "CarrierId" });
            DropIndex("dbo.VoyageDetails", new[] { "VesselId" });
            DropIndex("dbo.VoyageDetails", new[] { "UserId" });
            DropIndex("dbo.VoyageDetails", new[] { "StatusId" });
            DropIndex("dbo.VoyageDetails", new[] { "CarrierId" });
            DropIndex("dbo.VehicleDetails", new[] { "ConsignmentDetailsId" });
            DropIndex("dbo.ContainerDetails", new[] { "BOLDetailsId" });
            DropIndex("dbo.ConsignmentDetails", new[] { "BOLDetailsId" });
            DropIndex("dbo.ConsignmentDetails", new[] { "ContainerDetailsId" });
            DropIndex("dbo.BOLDetails", new[] { "VoyageDetailsId" });
            DropTable("dbo.UNHSCodes");
            DropTable("dbo.UNHazardousCodes");
            DropTable("dbo.PackageCodes");
            DropTable("dbo.LocationCodes");
            DropTable("dbo.Lines");
            DropTable("dbo.CountryCodes");
            DropTable("dbo.ContainerIsoCodes");
            DropTable("dbo.Status");
            DropTable("dbo.Vessels");
            DropTable("dbo.Users");
            DropTable("dbo.Carriers");
            DropTable("dbo.VoyageDetails");
            DropTable("dbo.VehicleDetails");
            DropTable("dbo.ContainerDetails");
            DropTable("dbo.ConsignmentDetails");
            DropTable("dbo.BOLDetails");
        }
    }
}
