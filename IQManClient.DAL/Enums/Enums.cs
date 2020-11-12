using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQManClient.DAL.Enums
{

    public enum UnitOfTemperatures
    {
        CENTIGRADE,
        FARENHEIT
    }
    public enum YesNo
    {
        YES,
        NO
    }
    public enum NewUsed
    {
        NEW,
        USED
    }
    public enum TradeCodes
    {
        IMPORT,
        TRANSSHIPMENT
    }
    public enum CargoCodes
    {
        FCLCONTAINER,
        LCLCONTAINER,
        EMPTYCONTAINER,
        BULKSOLID,
        BULKLIQUID,
        ROROUNIT,
        PASSENGER,
        LIVESTICK,
        GENERALCARGO
    }
    public enum StorageRequestCodes
    {
        DIRECTDELIVERY,
        STORAGEINSHARDS,
        STORAGEINYARDS
    }
    public enum TransshipModes
    {
        SEA,
        AIR,
        ROAD
    }
    public enum ContainerServiceTypes
    {
        FCL_FCL,
        FCL_LCL,
        LCL_LCL,
        LCL_FCL
    }
    public enum ContractCarriageConditions
    {
        COSTANDFREIGHT,
        COSTINSURANCEANDFREIGHT,
        FREEONBOARD,
        DOORTODOOR,
        OTHERS
    }
    public enum VehicleEquipmentIndicators
    {
        VEHICLE,
        EQUIPMENT,
        OTHERS
    }
    public enum RollingStatic
    {
        ROLLING,
        STATIC
    }


    public class EnumsHelper
    {
        public Dictionary<string, RollingStatic> rollingstatic;
        public Dictionary<string, YesNo> yesno;
        public Dictionary<string, NewUsed> usednew;
        public Dictionary<string, UnitOfTemperatures> temperatures;
        public Dictionary<string, TradeCodes> tradecodes;
        public Dictionary<string, CargoCodes> cargocodes;
        public Dictionary<string, StorageRequestCodes> storagerequests;
        public Dictionary<string, TransshipModes> transmodes;
        public Dictionary<string, ContainerServiceTypes> servicetypes;
        public Dictionary<string, ContractCarriageConditions> carriageconditions;
        public Dictionary<string, VehicleEquipmentIndicators> vehicleindicators;
        public EnumsHelper()
        {
            yesno = new Dictionary<string, YesNo>();
            yesno.Add("Y", YesNo.YES);
            yesno.Add("N", YesNo.NO);

            usednew = new Dictionary<string, NewUsed>();
            usednew.Add("N", NewUsed.NEW);
            usednew.Add("U", NewUsed.USED);

            temperatures = new Dictionary<string, UnitOfTemperatures>();
            temperatures.Add("C", UnitOfTemperatures.CENTIGRADE);
            temperatures.Add("F", UnitOfTemperatures.FARENHEIT);

            tradecodes = new Dictionary<string, TradeCodes>();
            tradecodes.Add("I", TradeCodes.IMPORT);
            tradecodes.Add("T", TradeCodes.TRANSSHIPMENT);

            cargocodes = new Dictionary<string, CargoCodes>();
            cargocodes.Add("F", CargoCodes.FCLCONTAINER);
            cargocodes.Add("L", CargoCodes.LCLCONTAINER);
            cargocodes.Add("M", CargoCodes.EMPTYCONTAINER);
            cargocodes.Add("B", CargoCodes.BULKSOLID);
            cargocodes.Add("Q", CargoCodes.BULKLIQUID);
            cargocodes.Add("R", CargoCodes.ROROUNIT);
            cargocodes.Add("P", CargoCodes.PASSENGER);
            cargocodes.Add("A", CargoCodes.LIVESTICK);
            cargocodes.Add("G", CargoCodes.GENERALCARGO);

            storagerequests = new Dictionary<string, StorageRequestCodes>();
            storagerequests.Add("D", StorageRequestCodes.DIRECTDELIVERY);
            storagerequests.Add("S", StorageRequestCodes.STORAGEINSHARDS);
            storagerequests.Add("Y", StorageRequestCodes.STORAGEINYARDS);

            transmodes = new Dictionary<string, TransshipModes>();
            transmodes.Add("S", TransshipModes.SEA);
            transmodes.Add("A", TransshipModes.AIR);
            transmodes.Add("R", TransshipModes.ROAD);

            servicetypes = new Dictionary<string, ContainerServiceTypes>();
            servicetypes.Add("FCL/FCL", ContainerServiceTypes.FCL_FCL);
            servicetypes.Add("FCL/LCL", ContainerServiceTypes.FCL_LCL);
            servicetypes.Add("LCL/LCL", ContainerServiceTypes.LCL_LCL);
            servicetypes.Add("LCL/FCL", ContainerServiceTypes.LCL_FCL);

            carriageconditions = new Dictionary<string, ContractCarriageConditions>();
            carriageconditions.Add("C&F", ContractCarriageConditions.COSTANDFREIGHT);
            carriageconditions.Add("CIF", ContractCarriageConditions.COSTINSURANCEANDFREIGHT);
            carriageconditions.Add("FOB", ContractCarriageConditions.FREEONBOARD);
            carriageconditions.Add("D/D", ContractCarriageConditions.DOORTODOOR);
            carriageconditions.Add("XXX", ContractCarriageConditions.OTHERS);

            vehicleindicators = new Dictionary<string, VehicleEquipmentIndicators>();
            vehicleindicators.Add("V", VehicleEquipmentIndicators.VEHICLE);
            vehicleindicators.Add("E", VehicleEquipmentIndicators.EQUIPMENT);
            vehicleindicators.Add("X", VehicleEquipmentIndicators.OTHERS);

            rollingstatic = new Dictionary<string, RollingStatic>();
            rollingstatic.Add("R", RollingStatic.ROLLING);
            rollingstatic.Add("S", RollingStatic.STATIC);

        }
    }

}
