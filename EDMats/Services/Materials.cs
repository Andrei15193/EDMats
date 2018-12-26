using System;
using System.Collections.Generic;
using System.Linq;

namespace EDMats.Services
{
    public static class Materials
    {
        private static readonly IReadOnlyDictionary<string, Material> _materialsById;

        public static MaterialType Raw { get; }

        public static MaterialCategory RawMaterialsCategory1 { get; }

        public static Material Carbon { get; }

        public static Material Vanadium { get; }

        public static Material Niobium { get; }

        public static Material Yttrium { get; }

        public static MaterialCategory RawMaterialsCategory2 { get; }

        public static Material Phosphorus { get; }

        public static Material Chromium { get; }

        public static Material Molybdenum { get; }

        public static Material Technetium { get; }

        public static MaterialCategory RawMaterialsCategory3 { get; }

        public static Material Sulphur { get; }

        public static Material Manganese { get; }

        public static Material Cadmium { get; }

        public static Material Ruthenium { get; }

        public static MaterialCategory RawMaterialsCategory4 { get; }

        public static Material Iron { get; }

        public static Material Zinc { get; }

        public static Material Tin { get; }

        public static Material Selenium { get; }

        public static MaterialCategory RawMaterialsCategory5 { get; }

        public static Material Nickel { get; }

        public static Material Germanium { get; }

        public static Material Tungsten { get; }

        public static Material Tellurium { get; }

        public static MaterialCategory RawMaterialsCategory6 { get; }

        public static Material Rhenium { get; }

        public static Material Arsenic { get; }

        public static Material Mercury { get; }

        public static Material Polonium { get; }

        public static MaterialCategory RawMaterialsCategory7 { get; }

        public static Material Lead { get; }

        public static Material Zirconium { get; }

        public static Material Boron { get; }

        public static Material Antimony { get; }

        public static MaterialType Manufactured { get; }

        public static MaterialCategory Chemical { get; }

        public static Material ChemicalStorageUnits { get; }

        public static Material ChemicalProcessors { get; }

        public static Material ChemicalDistillery { get; }

        public static Material ChemicalManipulators { get; }

        public static Material PharmaceuticalIsolators { get; }

        public static MaterialCategory Thermic { get; }

        public static Material TemperedAlloys { get; }

        public static Material HeatResistantCeramics { get; }

        public static Material PrecipitatedAlloys { get; }

        public static Material ThermicAlloys { get; }

        public static Material MilitaryGradeAlloys { get; }

        public static MaterialCategory Heat { get; }

        public static Material HeatConductionWiring { get; }

        public static Material HeatDispersionPlate { get; }

        public static Material HeatExchangers { get; }

        public static Material HeatVanes { get; }

        public static Material ProtoHeatRadiators { get; }

        public static MaterialCategory Conductive { get; }

        public static Material BasicConductors { get; }

        public static Material ConductiveComponents { get; }

        public static Material ConductiveCeramics { get; }

        public static Material ConductivePolymers { get; }

        public static Material BiotechConductors { get; }

        public static MaterialCategory MechanicalComponentsCategory { get; }

        public static Material MechanicalScrap { get; }

        public static Material MechanicalEquipment { get; }

        public static Material MechanicalComponents { get; }

        public static Material ConfigurableComponents { get; }

        public static Material ImprovisedComponents { get; }

        public static MaterialCategory Capacitors { get; }

        public static Material GridResistors { get; }

        public static Material HybridCapacitors { get; }

        public static Material ElectrochemicalArrays { get; }

        public static Material PolymerCapacitors { get; }

        public static Material MilitarySupercapacitors { get; }

        public static MaterialCategory Shielding { get; }

        public static Material WornShieldEmitters { get; }

        public static Material ShieldEmitters { get; }

        public static Material ShieldingSensors { get; }

        public static Material CompoundShielding { get; }

        public static Material ImperialShielding { get; }

        public static MaterialCategory Composite { get; }

        public static Material CompactComposites { get; }

        public static Material FilamentComposites { get; }

        public static Material HighDensityComposites { get; }

        public static Material ProprietaryComposites { get; }

        public static Material CoreDynamicsComposites { get; }

        public static MaterialCategory Crystals { get; }

        public static Material CrystalShards { get; }

        public static Material FlawedFocusCrystals { get; }

        public static Material FocusCrystals { get; }

        public static Material RefinedFocusCrystals { get; }

        public static Material ExquisiteFocusCrystals { get; }

        public static MaterialCategory Alloys { get; }

        public static Material SalvagedAlloys { get; }

        public static Material GalvanisingAlloys { get; }

        public static Material PhaseAlloys { get; }

        public static Material ProtoLightAlloys { get; }

        public static Material ProtoRadiolicAlloys { get; }

        public static MaterialType Encoded { get; }

        public static MaterialCategory EmissionData { get; }

        public static Material ExceptionalScrambledEmissionData { get; }

        public static Material IrregularEmissionData { get; }

        public static Material UnexpectedEmissionData { get; }

        public static Material DecodedEmissionData { get; }

        public static Material AbnormalCompactEmissionsData { get; }

        public static MaterialCategory WakeScans { get; }

        public static Material AtypicalDisruptedWakeEchoes { get; }

        public static Material AnomalousFsdTelemetry { get; }

        public static Material StrangeWakeSolutions { get; }

        public static Material EccentricHyperspaceTrajectories { get; }

        public static Material DataminedWakeExceptions { get; }

        public static MaterialCategory ShieldData { get; }

        public static Material DistortedShieldCycleRecordings { get; }

        public static Material InconsistentShieldSoakAnalysis { get; }

        public static Material UntypicalShieldScans { get; }

        public static Material AberrantShieldPatternAnalysis { get; }

        public static Material PeculiarShieldFrequencyData { get; }

        public static MaterialCategory EncryptionFiles { get; }

        public static Material UnusualEncryptedFiles { get; }

        public static Material TaggedEncryptionCodes { get; }

        public static Material OpenSymmetricKeys { get; }

        public static Material AtypicalEncryptionArchives { get; }

        public static Material AdaptiveEncryptorsCapture { get; }

        public static MaterialCategory DataArchives { get; }

        public static Material AnomalousBulkScanData { get; }

        public static Material UnidentifiedScanArchives { get; }

        public static Material ClassifiedScanDatabanks { get; }

        public static Material DivergendScanData { get; }

        public static Material ClassifiedScanFragment { get; }

        public static MaterialCategory EncodedFirmware { get; }

        public static Material SpecialisedLegacyFirmware { get; }

        public static Material ModifiedConsumerFirmware { get; }

        public static Material CrackedIndustrialFirmware { get; }

        public static Material SecurityFirmwarePatch { get; }

        public static Material ModifiedEmbeddedFirmware { get; }

        public static Material FindById(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            try
            {
                return _materialsById[id];
            }
            catch (KeyNotFoundException keyNotFoundException)
            {
                throw new ArgumentException($"Material with id '{id}' does not exist.", nameof(id), keyNotFoundException);
            }
        }

        static Materials()
        {
            {
                var categories = new List<MaterialCategory>(7);
                Raw = new MaterialType("Raw", categories);
                {
                    var materials = new List<Material>(4);
                    categories.Add(
                        RawMaterialsCategory1 = new MaterialCategory("Raw Material Category 1", Raw, materials)
                    );
                    materials.Add(
                        Carbon = new Material("carbon", "Carbon", MaterialGrade.VeryCommon, RawMaterialsCategory1, 300)
                    );
                    materials.Add(
                        Vanadium = new Material("vanadium", "Vanadium", MaterialGrade.Common, RawMaterialsCategory1, 250)
                    );
                    materials.Add(
                        Niobium = new Material("niobium", "Niobium", MaterialGrade.Standard, RawMaterialsCategory1, 200)
                    );
                    materials.Add(
                        Yttrium = new Material("yttrium", "Yttrium", MaterialGrade.Rare, RawMaterialsCategory1, 150)
                    );
                }
                {
                    var materials = new List<Material>(4);
                    categories.Add(
                        RawMaterialsCategory2 = new MaterialCategory("Raw Material Category 2", Raw, materials)
                    );
                    materials.Add(
                        Phosphorus = new Material("phosphorus", "Phosphorus", MaterialGrade.VeryCommon, RawMaterialsCategory2, 300)
                    );
                    materials.Add(
                        Chromium = new Material("chromium", "Chromium", MaterialGrade.Common, RawMaterialsCategory2, 250)
                    );
                    materials.Add(
                        Molybdenum = new Material("molybdenum", "Molybdenum", MaterialGrade.Standard, RawMaterialsCategory2, 200)
                    );
                    materials.Add(
                        Technetium = new Material("technetium", "Technetium", MaterialGrade.Rare, RawMaterialsCategory2, 150)
                    );
                }
                {
                    var materials = new List<Material>(4);
                    categories.Add(
                        RawMaterialsCategory3 = new MaterialCategory("Raw Material Category 3", Raw, materials)
                    );
                    materials.Add(
                        Sulphur = new Material("sulphur", "Sulphur", MaterialGrade.VeryCommon, RawMaterialsCategory3, 300)
                    );
                    materials.Add(
                        Manganese = new Material("manganese", "Manganese", MaterialGrade.Common, RawMaterialsCategory3, 250)
                    );
                    materials.Add(
                        Cadmium = new Material("cadmium", "Cadmium", MaterialGrade.Standard, RawMaterialsCategory3, 200)
                    );
                    materials.Add(
                        Ruthenium = new Material("ruthenium", "Ruthenium", MaterialGrade.Rare, RawMaterialsCategory3, 150)
                    );
                }
                {
                    var materials = new List<Material>(4);
                    categories.Add(
                        RawMaterialsCategory4 = new MaterialCategory("Raw Material Category 4", Raw, materials)
                    );
                    materials.Add(
                        Iron = new Material("iron", "Iron", MaterialGrade.VeryCommon, RawMaterialsCategory4, 300)
                    );
                    materials.Add(
                        Zinc = new Material("zinc", "Zinc", MaterialGrade.Common, RawMaterialsCategory4, 250)
                    );
                    materials.Add(
                        Tin = new Material("tin", "Tin", MaterialGrade.Standard, RawMaterialsCategory4, 200)
                    );
                    materials.Add(
                        Selenium = new Material("selenium", "Selenium", MaterialGrade.Rare, RawMaterialsCategory4, 150)
                    );
                }
                {
                    var materials = new List<Material>(4);
                    categories.Add(
                        RawMaterialsCategory5 = new MaterialCategory("Raw Material Category 5", Raw, materials)
                    );
                    materials.Add(
                        Nickel = new Material("nickel", "Nickel", MaterialGrade.VeryCommon, RawMaterialsCategory5, 300)
                    );
                    materials.Add(
                        Germanium = new Material("germanium", "Germanium", MaterialGrade.Common, RawMaterialsCategory5, 250)
                    );
                    materials.Add(
                        Tungsten = new Material("tungsten", "Tungsten", MaterialGrade.Standard, RawMaterialsCategory5, 200)
                    );
                    materials.Add(
                        Tellurium = new Material("tellurium", "Tellurium", MaterialGrade.Rare, RawMaterialsCategory5, 150)
                    );
                }
                {
                    var materials = new List<Material>(4);
                    categories.Add(
                        RawMaterialsCategory6 = new MaterialCategory("Raw Material Category 6", Raw, materials)
                    );
                    materials.Add(
                        Rhenium = new Material("rhenium", "Rhenium", MaterialGrade.VeryCommon, RawMaterialsCategory6, 300)
                    );
                    materials.Add(
                        Arsenic = new Material("arsenic", "Arsenic", MaterialGrade.Common, RawMaterialsCategory6, 250)
                    );
                    materials.Add(
                        Mercury = new Material("mercury", "Mercury", MaterialGrade.Standard, RawMaterialsCategory6, 200)
                    );
                    materials.Add(
                        Polonium = new Material("polonium", "Polonium", MaterialGrade.Rare, RawMaterialsCategory6, 150)
                    );
                }
                {
                    var materials = new List<Material>(4);
                    categories.Add(
                        RawMaterialsCategory7 = new MaterialCategory("Raw Material Category 7", Raw, materials)
                    );
                    materials.Add(
                        Lead = new Material("lead", "Lead", MaterialGrade.VeryCommon, RawMaterialsCategory7, 300)
                    );
                    materials.Add(
                        Zirconium = new Material("zirconium", "Zirconium", MaterialGrade.Common, RawMaterialsCategory7, 250)
                    );
                    materials.Add(
                        Boron = new Material("boron", "Boron", MaterialGrade.Standard, RawMaterialsCategory7, 200)
                    );
                    materials.Add(
                        Antimony = new Material("antimony", "Antimony", MaterialGrade.Rare, RawMaterialsCategory7, 150)
                    );
                }
            }

            {
                var categories = new List<MaterialCategory>(10);
                Manufactured = new MaterialType("Manufactured", categories);
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        Chemical = new MaterialCategory("Chemical", Manufactured, materials)
                    );
                    materials.Add(
                        ChemicalStorageUnits = new Material("chemicalstorageunits", "Chemical Storage Units", MaterialGrade.VeryCommon, Chemical, 300)
                    );
                    materials.Add(
                        ChemicalProcessors = new Material("chemicalprocessors", "Chemical Processors", MaterialGrade.Common, Chemical, 250)
                    );
                    materials.Add(
                        ChemicalDistillery = new Material("chemicaldistillery", "Chemical Distillery", MaterialGrade.Standard, Chemical, 200)
                    );
                    materials.Add(
                        ChemicalManipulators = new Material("chemicalmanipulators", "Chemical Manipulators", MaterialGrade.Rare, Chemical, 150)
                    );
                    materials.Add(
                        PharmaceuticalIsolators = new Material("pharmaceuticalisolators", "Pharmaceutical Isolators", MaterialGrade.VeryRare, Chemical, 100)
                    );
                }
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        Thermic = new MaterialCategory("Thermic", Manufactured, materials)
                    );
                    materials.Add(
                        TemperedAlloys = new Material("temperedalloys", "Tempered Alloys", MaterialGrade.VeryCommon, Thermic, 300)
                    );
                    materials.Add(
                        HeatResistantCeramics = new Material("heatresistantceramics", "Heat Resistant Ceramics", MaterialGrade.Common, Thermic, 250)
                    );
                    materials.Add(
                        PrecipitatedAlloys = new Material("precipitatedalloys", "Precipitated Alloys", MaterialGrade.Standard, Thermic, 200)
                    );
                    materials.Add(
                        ThermicAlloys = new Material("thermicalloys", "Thermic Alloys", MaterialGrade.Rare, Thermic, 150)
                    );
                    materials.Add(
                        MilitaryGradeAlloys = new Material("militarygradealloys", "Military Grade Alloys", MaterialGrade.VeryRare, Thermic, 100)
                    );
                }
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        Heat = new MaterialCategory("Heat", Manufactured, materials)
                    );
                    materials.Add(
                        HeatConductionWiring = new Material("heatconductionwiring", "Heat Conduction Wiring", MaterialGrade.VeryCommon, Heat, 300)
                    );
                    materials.Add(
                        HeatDispersionPlate = new Material("heatdispersionplate", "Heat Dispersion Plate", MaterialGrade.Common, Heat, 250)
                    );
                    materials.Add(
                        HeatExchangers = new Material("heatexchangers", "Heat Exchangers", MaterialGrade.Standard, Heat, 200)
                    );
                    materials.Add(
                        HeatVanes = new Material("heatvanes", "Heat Vanes", MaterialGrade.Rare, Heat, 150)
                    );
                    materials.Add(
                        ProtoHeatRadiators = new Material("protoheatradiators", "Proto Heat Radiators", MaterialGrade.VeryRare, Heat, 100)
                    );
                }
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        Conductive = new MaterialCategory("Conductive", Manufactured, materials)
                    );
                    materials.Add(
                        BasicConductors = new Material("basicconductors", "Basic Conductors", MaterialGrade.VeryCommon, Conductive, 300)
                    );
                    materials.Add(
                        ConductiveComponents = new Material("conductivecomponents", "Conductive Components", MaterialGrade.Common, Conductive, 250)
                    );
                    materials.Add(
                        ConductiveCeramics = new Material("conductiveceramics", "Conductive Ceramics", MaterialGrade.Standard, Conductive, 200)
                    );
                    materials.Add(
                        ConductivePolymers = new Material("conductivepolymers", "Conductive Polymers", MaterialGrade.Rare, Conductive, 150)
                    );
                    materials.Add(
                        BiotechConductors = new Material("biotechconductors", "Biotech Conductors", MaterialGrade.VeryRare, Conductive, 100)
                    );
                }
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        MechanicalComponentsCategory = new MaterialCategory("Mechanical Components", Manufactured, materials)
                    );
                    materials.Add(
                        MechanicalScrap = new Material("mechanicalscrap", "Mechanical Scrap", MaterialGrade.VeryCommon, MechanicalComponentsCategory, 300)
                    );
                    materials.Add(
                        MechanicalEquipment = new Material("mechanicalequipment", "Mechanical Equipment", MaterialGrade.Common, MechanicalComponentsCategory, 250)
                    );
                    materials.Add(
                        MechanicalComponents = new Material("mechanicalcomponents", "Mechanical Components", MaterialGrade.Standard, MechanicalComponentsCategory, 200)
                    );
                    materials.Add(
                        ConfigurableComponents = new Material("configurablecomponents", "Configurable Components", MaterialGrade.Rare, MechanicalComponentsCategory, 150)
                    );
                    materials.Add(
                        ImprovisedComponents = new Material("improvisedcomponents", "Improvised Components", MaterialGrade.VeryRare, MechanicalComponentsCategory, 100)
                    );
                }
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        Capacitors = new MaterialCategory("Capacitors", Manufactured, materials)
                    );
                    materials.Add(
                        GridResistors = new Material("gridresistors", "Grid Resistors", MaterialGrade.VeryCommon, Capacitors, 300)
                    );
                    materials.Add(
                        HybridCapacitors = new Material("hybridcapacitors", "Hybrid Capacitors", MaterialGrade.Common, Capacitors, 250)
                    );
                    materials.Add(
                        ElectrochemicalArrays = new Material("electrochemicalarrays", "Electrochemical Arrays", MaterialGrade.Standard, Capacitors, 200)
                    );
                    materials.Add(
                        PolymerCapacitors = new Material("polymercapacitors", "Polymer Capacitors", MaterialGrade.Rare, Capacitors, 150)
                    );
                    materials.Add(
                        MilitarySupercapacitors = new Material("militarysupercapacitors", "Military Supercapacitors", MaterialGrade.VeryRare, Capacitors, 100)
                    );
                }
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        Shielding = new MaterialCategory("Shielding", Manufactured, materials)
                    );
                    materials.Add(
                        WornShieldEmitters = new Material("wornshieldemitters", "Worn Shield Emitters", MaterialGrade.VeryCommon, Shielding, 300)
                    );
                    materials.Add(
                        ShieldEmitters = new Material("shieldemitters", "Shield Emitters", MaterialGrade.Common, Shielding, 250)
                    );
                    materials.Add(
                        ShieldingSensors = new Material("shieldingsensors", "Shielding Sensors", MaterialGrade.Standard, Shielding, 200)
                    );
                    materials.Add(
                        CompoundShielding = new Material("compoundshielding", "Compound Shielding", MaterialGrade.Rare, Shielding, 150)
                    );
                    materials.Add(
                        ImperialShielding = new Material("imperialshielding", "Imperial Shielding", MaterialGrade.VeryRare, Shielding, 100)
                    );
                }
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        Composite = new MaterialCategory("Composite", Manufactured, materials)
                    );
                    materials.Add(
                        CompactComposites = new Material("compactcomposites", "Compact Composites", MaterialGrade.VeryCommon, Composite, 300)
                    );
                    materials.Add(
                        FilamentComposites = new Material("filamentcomposites", "Filament Composites", MaterialGrade.Common, Composite, 250)
                    );
                    materials.Add(
                        HighDensityComposites = new Material("highdensitycomposites", "High Density Composites", MaterialGrade.Standard, Composite, 200)
                    );
                    materials.Add(
                        ProprietaryComposites = new Material("proprietarycomposites", "Proprietary Composites", MaterialGrade.Rare, Composite, 150)
                    );
                    materials.Add(
                        CoreDynamicsComposites = new Material("coredynamicscomposites", "Core Dynamics Composites", MaterialGrade.VeryRare, Composite, 100)
                    );
                }
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        Crystals = new MaterialCategory("Crystals", Manufactured, materials)
                    );
                    materials.Add(
                        CrystalShards = new Material("crystalshards", "Crystal Shards", MaterialGrade.VeryCommon, Crystals, 300)
                    );
                    materials.Add(
                        FlawedFocusCrystals = new Material("uncutfocuscrystals", "Flawed Focus Crystals", MaterialGrade.Common, Crystals, 250)
                    );
                    materials.Add(
                        FocusCrystals = new Material("focuscrystals", "Focus Crystals", MaterialGrade.Standard, Crystals, 200)
                    );
                    materials.Add(
                        RefinedFocusCrystals = new Material("refinedfocuscrystals", "Refined Focus Crystals", MaterialGrade.Rare, Crystals, 150)
                    );
                    materials.Add(
                        ExquisiteFocusCrystals = new Material("exquisitefocuscrystals", "Exquisite Focus Crystals", MaterialGrade.VeryRare, Crystals, 100)
                    );
                }
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        Alloys = new MaterialCategory("Alloys", Manufactured, materials)
                    );
                    materials.Add(
                        SalvagedAlloys = new Material("salvagedalloys", "Salvaged Alloys", MaterialGrade.VeryCommon, Alloys, 300)
                    );
                    materials.Add(
                        GalvanisingAlloys = new Material("galvanisingalloys", "Galvanising Alloys", MaterialGrade.Common, Alloys, 250)
                    );
                    materials.Add(
                        PhaseAlloys = new Material("phasealloys", "Phase Alloys", MaterialGrade.Standard, Alloys, 200)
                    );
                    materials.Add(
                        ProtoLightAlloys = new Material("protolightalloys", "Proto Light Alloys", MaterialGrade.Rare, Alloys, 150)
                    );
                    materials.Add(
                        ProtoRadiolicAlloys = new Material("protoradiolicalloys", "Proto Radiolic Alloys", MaterialGrade.VeryRare, Alloys, 100)
                    );
                }
            }

            {
                var categories = new List<MaterialCategory>(6);
                Encoded = new MaterialType("Encoded", categories);
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        EmissionData = new MaterialCategory("Emission Data", Encoded, materials)
                    );
                    materials.Add(
                        ExceptionalScrambledEmissionData = new Material("scrambledemissiondata", "Exceptional Scrambled Emission Data", MaterialGrade.VeryCommon, EmissionData, 300)
                    );
                    materials.Add(
                        IrregularEmissionData = new Material("archivedemissiondata", "Irregular Emission Data", MaterialGrade.Common, EmissionData, 250)
                    );
                    materials.Add(
                        UnexpectedEmissionData = new Material("emissiondata", "Unexpected Emission Data", MaterialGrade.Standard, EmissionData, 200)
                    );
                    materials.Add(
                        DecodedEmissionData = new Material("decodedemissiondata", "Decoded Emission Data", MaterialGrade.Rare, EmissionData, 150)
                    );
                    materials.Add(
                        AbnormalCompactEmissionsData = new Material("abnormalcompactemissionsdata", "Abnormal Compact Emissions Data", MaterialGrade.VeryRare, EmissionData, 100)
                    );
                }
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        WakeScans = new MaterialCategory("Wake Scans", Encoded, materials)
                    );
                    materials.Add(
                        AtypicalDisruptedWakeEchoes = new Material("disruptedwakeechoes", "Atypical Disrupted Wake Echoes", MaterialGrade.VeryCommon, WakeScans, 300)
                    );
                    materials.Add(
                        AnomalousFsdTelemetry = new Material("fsdtelemetry", "Anomalous FSD Telemetry", MaterialGrade.Common, WakeScans, 250)
                    );
                    materials.Add(
                        StrangeWakeSolutions = new Material("strangewakesolutions", "Strange Wake Solutions", MaterialGrade.Standard, WakeScans, 200)
                    );
                    materials.Add(
                        EccentricHyperspaceTrajectories = new Material("hyperspacetrajectories", "Eccentric Hyperspace Trajectories", MaterialGrade.Rare, WakeScans, 150)
                    );
                    materials.Add(
                        DataminedWakeExceptions = new Material("dataminedwake", "Datamined Wake Exceptions", MaterialGrade.VeryRare, WakeScans, 100)
                    );
                }
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        ShieldData = new MaterialCategory("Shield Data", Encoded, materials)
                    );
                    materials.Add(
                        DistortedShieldCycleRecordings = new Material("shieldcyclerecordings", "Distorted Shield Cycle Recordings", MaterialGrade.VeryCommon, ShieldData, 300)
                    );
                    materials.Add(
                        InconsistentShieldSoakAnalysis = new Material("shieldsoakanalysis", "Inconsistent Shield Soak Analysis", MaterialGrade.Common, ShieldData, 250)
                    );
                    materials.Add(
                        UntypicalShieldScans = new Material("shielddensityreports", "Untypical Shield Scans", MaterialGrade.Standard, ShieldData, 200)
                    );
                    materials.Add(
                        AberrantShieldPatternAnalysis = new Material("shieldpatternanalysis", "Aberrant Shield Pattern Analysis", MaterialGrade.Rare, ShieldData, 150)
                    );
                    materials.Add(
                        PeculiarShieldFrequencyData = new Material("shieldfrequencydata", "Peculiar Shield Frequency Data", MaterialGrade.VeryRare, ShieldData, 100)
                    );
                }
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        EncryptionFiles = new MaterialCategory("Encryption Files", Encoded, materials)
                    );
                    materials.Add(
                        UnusualEncryptedFiles = new Material("unusualencryptedfiles", "Unusual Encrypted Files", MaterialGrade.VeryCommon, EncryptionFiles, 300)
                    );
                    materials.Add(
                        TaggedEncryptionCodes = new Material("taggedencryptioncodes", "Tagged Encryption Codes", MaterialGrade.Common, EncryptionFiles, 250)
                    );
                    materials.Add(
                        OpenSymmetricKeys = new Material("opensymmetrickeys", "Open Symmetric Keys", MaterialGrade.Standard, EncryptionFiles, 200)
                    );
                    materials.Add(
                        AtypicalEncryptionArchives = new Material("atypicalencryptionarchives", "Atypical Encryption Archives", MaterialGrade.Rare, EncryptionFiles, 150)
                    );
                    materials.Add(
                        AdaptiveEncryptorsCapture = new Material("adaptiveencryptorscapture", "Adaptive Encryptors Capture", MaterialGrade.VeryRare, EncryptionFiles, 100)
                    );
                }
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        DataArchives = new MaterialCategory("Data Archives", Encoded, materials)
                    );
                    materials.Add(
                        AnomalousBulkScanData = new Material("bulkscandata", "Anomalous Bulk Scan Data", MaterialGrade.VeryCommon, DataArchives, 300)
                    );
                    materials.Add(
                        UnidentifiedScanArchives = new Material("scanarchives", "Unidentified Scan Archives", MaterialGrade.Common, DataArchives, 250)
                    );
                    materials.Add(
                        ClassifiedScanDatabanks = new Material("scandatabanks", "Classified Scan Databanks", MaterialGrade.Standard, DataArchives, 200)
                    );
                    materials.Add(
                        DivergendScanData = new Material("divergendscandata", "Divergend Scan Data", MaterialGrade.Rare, DataArchives, 150)
                    );
                    materials.Add(
                        ClassifiedScanFragment = new Material("classifiedscanfragment", "Classified Scan Fragment", MaterialGrade.VeryRare, DataArchives, 100)
                    );
                }
                {
                    var materials = new List<Material>(5);
                    categories.Add(
                        EncodedFirmware = new MaterialCategory("Encoded Firmware", Encoded, materials)
                    );
                    materials.Add(
                        SpecialisedLegacyFirmware = new Material("specialisedlegacyfirmware", "Specialised Legacy Firmware", MaterialGrade.VeryCommon, EncodedFirmware, 300)
                    );
                    materials.Add(
                        ModifiedConsumerFirmware = new Material("consumerfirmware", "Modified Consumer Firmware", MaterialGrade.Common, EncodedFirmware, 250)
                    );
                    materials.Add(
                        CrackedIndustrialFirmware = new Material("industrialfirmware", "Cracked Industrial Firmware", MaterialGrade.Standard, EncodedFirmware, 200)
                    );
                    materials.Add(
                        SecurityFirmwarePatch = new Material("securityfirmwarepatch", "Security Firmware Patch", MaterialGrade.Rare, EncodedFirmware, 150)
                    );
                    materials.Add(
                        ModifiedEmbeddedFirmware = new Material("embeddedfirmware", "Modified Embedded Firmware", MaterialGrade.VeryRare, EncodedFirmware, 100)
                    );
                }
            }

            _materialsById = Raw
                .Categories
                .Concat(Manufactured.Categories)
                .Concat(Encoded.Categories)
                .SelectMany(category => category.Materials)
                .ToDictionary(material => material.Id, StringComparer.OrdinalIgnoreCase);
        }
    }
}