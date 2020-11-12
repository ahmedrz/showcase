using EManifestServices.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class VoyageValidationContext
    {
        EmanifestContext _db;
        public List<LocationCodes> DbLocations { get; set; }
        public List<CountryCodes> DbCountries { get; set; }
        public List<ContainerIsoCodes> DbContainerCodes { get; set; }
        public List<UNHazardousCodes> DbHazardousCodes { get; set; }
        public List<UNHSCodes> DbHsCodes { get; set; }
        public List<PackageCodes> DbPackageCodes { get; set; }
        public List<Lines> DbLines { get; set; }
        public VoyageDetails Voyage { get; set; }

        public VoyageValidationContext()
        {
            _db = new EmanifestContext();
            DbLocations = new List<LocationCodes>();
            DbCountries = new List<CountryCodes>();
            DbContainerCodes = new List<ContainerIsoCodes>();
            DbHazardousCodes = new List<UNHazardousCodes>();
            DbHsCodes = new List<UNHSCodes>();
            DbPackageCodes = new List<PackageCodes>();
            DbLines = new List<Lines>();
        }
        private void InitializeCache()
        {
            try
            {
                if (Voyage == null)
                {
                    return;
                }
                //lines
                var voyageLineNames = Voyage.BOLDetails.Select(b => b.BoxPartneringLineCode).Distinct().ToList();
                var voyagePrimaryLinesName = Voyage.LineCode;
                voyageLineNames.Add(voyagePrimaryLinesName);
                //find the lines that is not fetched from db

                var nonFetchedLineName = voyageLineNames.Distinct()
                    .Where(line => !DbLines.Any(dbline => dbline.LineName == line))
                    .ToList();
                if (nonFetchedLineName.Any())
                {
                    var newLines = _db.Lines.Where(l => nonFetchedLineName.Contains(l.LineName)).ToList();
                    DbLines.AddRange(newLines);
                }



                //location codes
                var voyageLocationCodes = Voyage.BOLDetails.Select(b => b.PortCodeOfOrigin).Distinct().ToList();
                voyageLocationCodes.AddRange(Voyage.BOLDetails.Select(b => b.PortCodeOfLoading).Distinct().ToList());
                voyageLocationCodes.AddRange(Voyage.BOLDetails.Select(b => b.PortCodeOfDischarge).Distinct().ToList());
                voyageLocationCodes.AddRange(Voyage.BOLDetails.Select(b => b.PortCodeOfDestination).Distinct().ToList());
                voyageLocationCodes.Add(Voyage.PortCodeOfDischarge);

                var nonFetchedLocations = voyageLocationCodes.Distinct()
                  .Where(code => !DbLocations.Any(dbcode => dbcode.FullCode == code))
                  .ToList();
                if (nonFetchedLocations.Any())
                {
                    var newLocations = _db.LocationCodes.Where(l => nonFetchedLocations.Contains(l.FullCode)).ToList();
                    DbLocations.AddRange(newLocations);
                }

                //country codes
                var voyageCountryCodes = Voyage.BOLDetails.Select(b => b.CountryOfOrigin).Distinct().ToList();
                voyageCountryCodes.AddRange(Voyage.BOLDetails.Select(b => b.ShipperCountryCode).Distinct().ToList());


                var nonFetchedCountries = voyageCountryCodes.Distinct()
                  .Where(code => !DbCountries.Any(dbcode => dbcode.CountryCode == code))
                  .ToList();
                if (nonFetchedCountries.Any())
                {
                    var newCountries = _db.CountryCodes.Where(c => nonFetchedCountries.Contains(c.CountryCode)).ToList();
                    DbCountries.AddRange(newCountries);
                }

                //container codes
                var voyageContainerCodes = Voyage.BOLDetails.SelectMany(b => b.ContainerDetails).Select(c => c.ISOCode).Distinct().ToList();

                var nonFetchedContainerCodes = voyageContainerCodes.Distinct()
                  .Where(code => !DbContainerCodes.Any(dbcode => dbcode.IsoTypeCode == code))
                  .ToList();
                if (nonFetchedContainerCodes.Any())
                {
                    var newContainerCodes = _db.ContainerIsoCodes.Where(c => nonFetchedContainerCodes.Contains(c.IsoTypeCode)).ToList();
                    DbContainerCodes.AddRange(newContainerCodes);
                }

                //unhazardous codes
                var voyageHazardousCodes = Voyage.BOLDetails.SelectMany(b => b.ContainerDetails.SelectMany(c => c.ConsignmentDetails)).Select(c => c.UnNumberOfDangerousGoods).Distinct().ToList();
                voyageHazardousCodes.AddRange(Voyage.BOLDetails.SelectMany(b => b.ConsignmentDetails).Select(c => c.UnNumberOfDangerousGoods).Distinct().ToList());

                var nonFetchedUnhazardousCodes = voyageHazardousCodes.Distinct()
                  .Where(code => !DbHazardousCodes.Any(dbcode => dbcode.Code == code))
                  .ToList();
                if (nonFetchedUnhazardousCodes.Any())
                {
                    var newHazardousCodes = _db.UNHazardousCodes.Where(c => nonFetchedUnhazardousCodes.Contains(c.Code)).ToList();
                    DbHazardousCodes.AddRange(newHazardousCodes);
                }

                //hs codes
                var voyageBolHsCode = Voyage.BOLDetails.Select(b => b.CommodityCode).Distinct().ToList();
                voyageBolHsCode.AddRange(Voyage.BOLDetails.SelectMany(b => b.ConsignmentDetails.Select(c => c.CommodityCode)).Distinct().ToList());
                voyageBolHsCode.AddRange(Voyage.BOLDetails.SelectMany(b => b.ContainerDetails.SelectMany(c => c.ConsignmentDetails.Select(con => con.CommodityCode))).Distinct().ToList());


                var nonFetchedHsCodes = voyageBolHsCode.Distinct()
                  .Where(code => !DbHsCodes.Any(dbcode => dbcode.Code == code))
                  .ToList();
                if (nonFetchedHsCodes.Any())
                {
                    var newHsCodes = _db.UNHSCodes.Where(c => c.Classification == "H5" && nonFetchedHsCodes.Contains(c.Code)).ToList();
                    DbHsCodes.AddRange(newHsCodes);
                }

                //package codes
                var voyagePackageCodes = Voyage.BOLDetails.Select(b => b.PackageTypeCode).Distinct().ToList();
                voyagePackageCodes.AddRange(Voyage.BOLDetails.SelectMany(b => b.ConsignmentDetails.Select(c => c.PackageTypeCode)).Distinct().ToList());
                voyagePackageCodes.AddRange(Voyage.BOLDetails.SelectMany(b => b.ContainerDetails.SelectMany(c => c.ConsignmentDetails.Select(con => con.PackageTypeCode))).Distinct().ToList());


                var nonFetchedPackageCodes = voyagePackageCodes.Distinct()
                  .Where(code => !DbPackageCodes.Any(dbcode => dbcode.PackageCode == code))
                  .ToList();
                if (nonFetchedPackageCodes.Any())
                {
                    var newpackageCodes = _db.PackageCodes.Where(c => nonFetchedPackageCodes.Contains(c.PackageCode)).ToList();
                    DbPackageCodes.AddRange(newpackageCodes);
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        private static VoyageValidationContext _validationContext;
        public static VoyageValidationContext CreateContext(VoyageDetails voyage)
        {
            var newContext = _validationContext ?? new VoyageValidationContext();
            newContext.Voyage = voyage;
            newContext.InitializeCache();
            return newContext;
        }
    }
}