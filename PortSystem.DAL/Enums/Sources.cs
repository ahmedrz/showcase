using System.Collections.Generic;
using System.Linq;

namespace PortSystem.DAL.Enums
{
    public class Sources
    {
        private EnumsHelper enums;
        public List<ComboItemSource> IMOClassSource { get; set; }
        public List<ComboItemSource> YesNoSource { get; set; }
        public List<ComboItemSource> UsedNewSource { get; set; }
        public List<ComboItemSource> TemperaturesSource { get; set; }
        public List<ComboItemSource> TradeCodesSource { get; set; }
        public List<ComboItemSource> CargoCodesSource { get; set; }
        public List<ComboItemSource> StorageRequestCodesSource { get; set; }
        public List<ComboItemSource> TransshipModesSource { get; set; }
        public List<ComboItemSource> ContainerServiceTypesSource { get; set; }
        public List<ComboItemSource> ContractCarriageConditionsSource { get; set; }
        public List<ComboItemSource> VehicleEquipmentIndicatorsSource { get; set; }
        public List<ComboItemSource> RollingStaticSource { get; set; }
        public Sources()
        {
            enums = new EnumsHelper();
            YesNoSource = enums.yesno.Select(i => new ComboItemSource { Value = i.Key, Text = $"{i.Key} - {i.Value.ToString()}" }).ToList();
            UsedNewSource = enums.usednew.Select(i => new ComboItemSource { Value = i.Key, Text = $"{i.Key} - {i.Value.ToString()}" }).ToList();
            TemperaturesSource = enums.temperatures.Select(i => new ComboItemSource { Value = i.Key, Text = $"{i.Key} - {i.Value.ToString()}" }).ToList();
            TradeCodesSource = enums.tradecodes.Select(i => new ComboItemSource { Value = i.Key, Text = $"{i.Key} - {i.Value.ToString()}" }).ToList();
            CargoCodesSource = enums.cargocodes.Select(i => new ComboItemSource { Value = i.Key, Text = $"{i.Key} - {i.Value.ToString()}" }).ToList();
            StorageRequestCodesSource = enums.storagerequests.Select(i => new ComboItemSource { Value = i.Key, Text = $"{i.Key} - {i.Value.ToString()}" }).ToList();
            TransshipModesSource = enums.transmodes.Select(i => new ComboItemSource { Value = i.Key, Text = $"{i.Key} - {i.Value.ToString()}" }).ToList();
            ContainerServiceTypesSource = enums.servicetypes.Select(i => new ComboItemSource { Value = i.Key, Text = $"{i.Value.ToString()}" }).ToList();
            ContractCarriageConditionsSource = enums.carriageconditions.Select(i => new ComboItemSource { Value = i.Key, Text = $"{i.Key} - {i.Value.ToString()}" }).ToList();
            VehicleEquipmentIndicatorsSource = enums.vehicleindicators.Select(i => new ComboItemSource { Value = i.Key, Text = $"{i.Key} - {i.Value.ToString()}" }).ToList();
            RollingStaticSource = enums.rollingstatic.Select(i => new ComboItemSource { Value = i.Key, Text = $"{i.Key} - {i.Value.ToString()}" }).ToList();
            FillImoClasses();
        }
        private void FillImoClasses()
        {
            IMOClassSource = new List<ComboItemSource>();
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "1",
                Text = "Explosives"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "1.1",
                Text = "Substances and articles which have a mass explosion hazard."
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "1.2",
                Text = "Substances and articles which have a projection hazard but not a mass explosion hazard."
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "1.3",
                Text = @"Substances and articles which have a fire hazard and either a minor blast hazard or a minor
projection hazard or both, but not a mass explosion hazard."
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "1.4",
                Text = "Substances and articles which present no significant hazard."
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "1.5",
                Text = "Very insensitive substances which have a mass explosion hazard."
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "1.6",
                Text = "Extremely insensitive articles which do not have a mass explosion hazard."
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "2",
                Text = "Gases: Compressed, Liquefied or Dissolved under Pressure"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "2.1",
                Text = "Flammable gases"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "2.2",
                Text = "Non-Flammable gases"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "2.3",
                Text = "Toxic gases"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "3",
                Text = "Flammable Liquids"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "4",
                Text = "Flammable Solids or Substances"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "4.1",
                Text = "Flammable solids"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "4.2",
                Text = "Substances liable to spontaneous combustion"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "4.3",
                Text = "Substances which, in contact with water, emit flammable gases."
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "5",
                Text = "Oxidizing Substances (agents) and Organic Peroxides"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "5.1",
                Text = "Oxidizing substances (agents) by yielding oxygen increase the risk and intensity of fire"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "5.2",
                Text = "Organic peroxides - most will burn rapidly and are sensitive to impact or friction"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "6",
                Text = "Toxic and infectious Substances"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "6.1",
                Text = "Toxic substances"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "6.2",
                Text = "Infectious substances"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "7",
                Text = "Radioactive Substances"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "8",
                Text = "Corrosives"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "9",
                Text = "Miscellaneous dangerous substances and articles"
            });
            IMOClassSource.Add(new ComboItemSource
            {
                Value = "MHB",
                Text = "Materials hazardous only in bulk"
            });
        }
    }

    public class ComboItemSource
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
