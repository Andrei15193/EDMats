using System;
using System.Collections.Generic;
using System.Linq;
using EDMats.Data.Materials;

namespace EDMats.Data.Engineering
{
    public class Module
    {
        public Module(ModuleType type, string id, string name, IReadOnlyCollection<Blueprint> blueprints, IReadOnlyCollection<ExperimentalEffect> experimentalEffects)
        {
            Type = type;
            Id = id;
            Name = name;
            Blueprints = blueprints;
            ExperimentalEffects = experimentalEffects;
        }

        public ModuleType Type { get; }

        public string Id { get; }

        public string Name { get; }

        public IReadOnlyCollection<Blueprint> Blueprints { get; }

        public IReadOnlyCollection<ExperimentalEffect> ExperimentalEffects { get; }

        private static readonly IReadOnlyDictionary<string, Module> _modulesById;

        public static IReadOnlyCollection<Module> All { get; }

        public static ModuleType CoreInternals { get; }

        public static Module Armour { get; }

        public static Module FrameShiftDrive { get; }

        public static Module LifeSupport { get; }

        public static Module PowerDistributor { get; }

        public static Module PowerPlant { get; }

        public static Module Sensors { get; }

        public static Module Thrusters { get; }

        public static ModuleType Hardpoints { get; }

        public static Module BeamLaser { get; }

        public static Module BurstLaser { get; }

        public static Module Cannon { get; }

        public static Module FragmentCannon { get; }

        public static Module MineLauncher { get; }

        public static Module MissileRack { get; }

        public static Module MultiCannon { get; }

        public static Module PlasmaAccelerator { get; }

        public static Module PulseLaser { get; }

        public static Module RailGun { get; }

        public static Module SeekerMissileRack { get; }

        public static Module TorpedoPylon { get; }

        public static ModuleType OptionalInternals { get; }

        public static Module AutoFieldMaintenanceUnit { get; }

        public static Module CollectorLimpetController { get; }

        public static Module DetailedSurfaceScanner { get; }

        public static Module FrameShiftDriveInterdictor { get; }

        public static Module FuelScoop { get; }

        public static Module FuelTransferLimpetController { get; }

        public static Module HatchBreakerLimpetController { get; }

        public static Module HullReinforcementPackage { get; }

        public static Module ProspectorLimpetController { get; }

        public static Module Refinery { get; }

        public static Module ShieldCellBank { get; }

        public static Module ShieldGenerator { get; }

        public static ModuleType UtilityMounts { get; }

        public static Module ChaffLauncher { get; }

        public static Module ElectronicCountermeasure { get; }

        public static Module FrameShiftWakeScanner { get; }

        public static Module HeatSinkLauncher { get; }

        public static Module KillWarrantScanner { get; }

        public static Module ManifestScanner { get; }

        public static Module PointDefence { get; }

        public static Module ShieldBooster { get; }

        public static Module FindById(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            try
            {
                return _modulesById[id];
            }
            catch (KeyNotFoundException keyNotFoundException)
            {
                throw new ArgumentException($"Module with id '{id}' does not exist.", nameof(id), keyNotFoundException);
            }
        }

        static Module()
        {
            {
                var modules = new Module[7];
                CoreInternals = new ModuleType("ci", "Core Internals", modules);
                {
                    var blueprints = new Blueprint[5];
                    var experimentalEffects = new ExperimentalEffect[4];
                    modules[0] = Armour = new Module(CoreInternals, "a", "Armour", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        Armour,
                        "br",
                        "Blast Resistant",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.SalvagedAlloys, 1), new MaterialQuantity(Material.Vanadium, 1), new MaterialQuantity(Material.Zirconium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.GalvanisingAlloys, 1), new MaterialQuantity(Material.Mercury, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.Ruthenium, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        Armour,
                        "hd",
                        "Heavy Duty",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Carbon, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        Armour,
                        "kr",
                        "Kinetic Resistant",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.SalvagedAlloys, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.GalvanisingAlloys, 1), new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.PhaseAlloys, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        Armour,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.ProprietaryComposites, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.MilitaryGradeAlloys, 1), new MaterialQuantity(Material.Tin, 1) })
                        }
                    );
                    blueprints[4] = new Blueprint(
                        Armour,
                        "tr",
                        "Thermal Resistant",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.HeatConductionWiring, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.HeatExchangers, 1), new MaterialQuantity(Material.SalvagedAlloys, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.GalvanisingAlloys, 1), new MaterialQuantity(Material.HeatVanes, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoHeatRadiators, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(Armour, "ap", "Angled Plating", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.HighDensityComposites, 3), new MaterialQuantity(Material.Zirconium, 3) });
                    experimentalEffects[1] = new ExperimentalEffect(Armour, "dp", "Deep Plating", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.MechanicalEquipment, 3), new MaterialQuantity(Material.Molybdenum, 2) });
                    experimentalEffects[2] = new ExperimentalEffect(Armour, "lp", "Layered Plating", new[] { new MaterialQuantity(Material.HeatConductionWiring, 5), new MaterialQuantity(Material.HighDensityComposites, 3), new MaterialQuantity(Material.Niobium, 1) });
                    experimentalEffects[3] = new ExperimentalEffect(Armour, "rp", "Reflective Plating", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.HeatDispersionPlate, 3), new MaterialQuantity(Material.ThermicAlloys, 2) });
                }
                {
                    var blueprints = new Blueprint[3];
                    var experimentalEffects = new ExperimentalEffect[5];
                    modules[1] = FrameShiftDrive = new Module(CoreInternals, "fsd", "Frame Shift Drive", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        FrameShiftDrive,
                        "fbs",
                        "Faster Boot Sequence",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.GridResistors, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.GridResistors, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.GridResistors, 1), new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.Selenium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Cadmium, 1), new MaterialQuantity(Material.HeatExchangers, 1), new MaterialQuantity(Material.HybridCapacitors, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.HeatVanes, 1), new MaterialQuantity(Material.Tellurium, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        FrameShiftDrive,
                        "ir",
                        "Increased Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.AtypicalDisruptedWakeEchoes, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.AtypicalDisruptedWakeEchoes, 1), new MaterialQuantity(Material.ChemicalProcessors, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ChemicalProcessors, 1), new MaterialQuantity(Material.Phosphorus, 1), new MaterialQuantity(Material.StrangeWakeSolutions, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ChemicalDistillery, 1), new MaterialQuantity(Material.EccentricHyperspaceTrajectories, 1), new MaterialQuantity(Material.Manganese, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Arsenic, 1), new MaterialQuantity(Material.ChemicalManipulators, 1), new MaterialQuantity(Material.DataminedWakeExceptions, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        FrameShiftDrive,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ImperialShielding, 1), new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(FrameShiftDrive, "dc", "Deep Charge", new[] { new MaterialQuantity(Material.AtypicalDisruptedWakeEchoes, 5), new MaterialQuantity(Material.GalvanisingAlloys, 3), new MaterialQuantity(Material.EccentricHyperspaceTrajectories, 1) });
                    experimentalEffects[1] = new ExperimentalEffect(FrameShiftDrive, "db", "Double Braced", new[] { new MaterialQuantity(Material.AtypicalDisruptedWakeEchoes, 5), new MaterialQuantity(Material.GalvanisingAlloys, 3), new MaterialQuantity(Material.ConfigurableComponents, 1) });
                    experimentalEffects[2] = new ExperimentalEffect(FrameShiftDrive, "mm", "Mass Manager", new[] { new MaterialQuantity(Material.AtypicalDisruptedWakeEchoes, 5), new MaterialQuantity(Material.GalvanisingAlloys, 3), new MaterialQuantity(Material.EccentricHyperspaceTrajectories, 1) });
                    experimentalEffects[3] = new ExperimentalEffect(FrameShiftDrive, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.AtypicalDisruptedWakeEchoes, 5), new MaterialQuantity(Material.GalvanisingAlloys, 3), new MaterialQuantity(Material.ProtoLightAlloys, 1) });
                    experimentalEffects[4] = new ExperimentalEffect(FrameShiftDrive, "ts", "Thermal Spread", new[] { new MaterialQuantity(Material.AtypicalDisruptedWakeEchoes, 5), new MaterialQuantity(Material.GalvanisingAlloys, 3), new MaterialQuantity(Material.GridResistors, 3), new MaterialQuantity(Material.HeatVanes, 1) });
                }
                {
                    var blueprints = new Blueprint[3];
                    modules[2] = LifeSupport = new Module(CoreInternals, "ls", "Life Support", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        LifeSupport,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        LifeSupport,
                        "r",
                        "Reinforced",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        LifeSupport,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[6];
                    var experimentalEffects = new ExperimentalEffect[5];
                    modules[3] = PowerDistributor = new Module(CoreInternals, "pdstr", "Power Distributor", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        PowerDistributor,
                        "ce",
                        "Charge Enhanced",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ChemicalProcessors, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ChemicalDistillery, 1), new MaterialQuantity(Material.GridResistors, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ChemicalManipulators, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1), new MaterialQuantity(Material.HybridCapacitors, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ChemicalManipulators, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1), new MaterialQuantity(Material.ExquisiteFocusCrystals, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        PowerDistributor,
                        "ef",
                        "Engine Focused",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.AnomalousBulkScanData, 1), new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.PolymerCapacitors, 1), new MaterialQuantity(Material.Selenium, 1), new MaterialQuantity(Material.UnidentifiedScanArchives, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Cadmium, 1), new MaterialQuantity(Material.ClassifiedScanDatabanks, 1), new MaterialQuantity(Material.MilitarySupercapacitors, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        PowerDistributor,
                        "hcc",
                        "High Charge Capacity",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.Selenium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CrackedIndustrialFirmware, 1), new MaterialQuantity(Material.MilitarySupercapacitors, 1), new MaterialQuantity(Material.ProprietaryComposites, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        PowerDistributor,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                    blueprints[4] = new Blueprint(
                        PowerDistributor,
                        "ss",
                        "System Focused",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.AnomalousBulkScanData, 1), new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.PolymerCapacitors, 1), new MaterialQuantity(Material.Selenium, 1), new MaterialQuantity(Material.UnidentifiedScanArchives, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Cadmium, 1), new MaterialQuantity(Material.ClassifiedScanDatabanks, 1), new MaterialQuantity(Material.MilitarySupercapacitors, 1) })
                        }
                    );
                    blueprints[5] = new Blueprint(
                        PowerDistributor,
                        "wf",
                        "Weapon Focused",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.AnomalousBulkScanData, 1), new MaterialQuantity(Material.HybridCapacitors, 1), new MaterialQuantity(Material.Selenium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Cadmium, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.UnidentifiedScanArchives, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ClassifiedScanDatabanks, 1), new MaterialQuantity(Material.PolymerCapacitors, 1), new MaterialQuantity(Material.Tellurium, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(PowerDistributor, "cc", "Cluster Capacitors", new[] { new MaterialQuantity(Material.Phosphorus, 5), new MaterialQuantity(Material.HeatResistantCeramics, 3), new MaterialQuantity(Material.Cadmium, 1) });
                    experimentalEffects[1] = new ExperimentalEffect(PowerDistributor, "db", "Double Braced", new[] { new MaterialQuantity(Material.Phosphorus, 5), new MaterialQuantity(Material.HeatResistantCeramics, 3), new MaterialQuantity(Material.ProprietaryComposites, 1) });
                    experimentalEffects[2] = new ExperimentalEffect(PowerDistributor, "fc", "Flow Control", new[] { new MaterialQuantity(Material.Phosphorus, 5), new MaterialQuantity(Material.HeatResistantCeramics, 3), new MaterialQuantity(Material.ConductivePolymers, 1) });
                    experimentalEffects[3] = new ExperimentalEffect(PowerDistributor, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.Phosphorus, 5), new MaterialQuantity(Material.HeatResistantCeramics, 3), new MaterialQuantity(Material.ProtoLightAlloys, 1) });
                    experimentalEffects[4] = new ExperimentalEffect(PowerDistributor, "sc", "Super Conduits", new[] { new MaterialQuantity(Material.Phosphorus, 5), new MaterialQuantity(Material.HeatResistantCeramics, 3), new MaterialQuantity(Material.SecurityFirmwarePatch, 1) });
                }
                {
                    var blueprints = new Blueprint[3];
                    var experimentalEffects = new ExperimentalEffect[4];
                    modules[4] = PowerPlant = new Module(CoreInternals, "pp", "Power Plant", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        PowerPlant,
                        "a",
                        "Armoured",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        PowerPlant,
                        "le",
                        "Low Emissions",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Iron, 1), new MaterialQuantity(Material.IrregularEmissionData, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.HeatExchangers, 1), new MaterialQuantity(Material.Iron, 1), new MaterialQuantity(Material.IrregularEmissionData, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.HeatVanes, 1), new MaterialQuantity(Material.UnexpectedEmissionData, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.DecodedEmissionData, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.ProtoHeatRadiators, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        PowerPlant,
                        "o",
                        "Overcharged",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.HeatConductionWiring, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.HeatConductionWiring, 1), new MaterialQuantity(Material.Selenium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Cadmium, 1), new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.HeatDispersionPlate, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ChemicalManipulators, 1), new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Tellurium, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(PowerPlant, "db", "Double Braced", new[] { new MaterialQuantity(Material.GridResistors, 5), new MaterialQuantity(Material.Vanadium, 3), new MaterialQuantity(Material.ProprietaryComposites, 1) });
                    experimentalEffects[1] = new ExperimentalEffect(PowerPlant, "m", "Monstered", new[] { new MaterialQuantity(Material.GridResistors, 5), new MaterialQuantity(Material.Vanadium, 3), new MaterialQuantity(Material.PolymerCapacitors, 1) });
                    experimentalEffects[2] = new ExperimentalEffect(PowerPlant, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.GridResistors, 5), new MaterialQuantity(Material.Vanadium, 3), new MaterialQuantity(Material.ProtoLightAlloys, 1) });
                    experimentalEffects[3] = new ExperimentalEffect(PowerPlant, "ts", "Thermal Spread", new[] { new MaterialQuantity(Material.GridResistors, 5), new MaterialQuantity(Material.Vanadium, 3), new MaterialQuantity(Material.HeatVanes, 1) });
                }
                {
                    var blueprints = new Blueprint[3];
                    modules[5] = Sensors = new Module(CoreInternals, "s", "Sensors", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        Sensors,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        Sensors,
                        "lr",
                        "Long Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HybridCapacitors, 1), new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.HybridCapacitors, 1), new MaterialQuantity(Material.Iron, 1), new MaterialQuantity(Material.UnexpectedEmissionData, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.DecodedEmissionData, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.Germanium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.AbnormalCompactEmissionsData, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.PolymerCapacitors, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        Sensors,
                        "wa",
                        "Wide Angle",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ClassifiedScanDatabanks, 1), new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.DivergendScanData, 1), new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.Niobium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ClassifiedScanFragment, 1), new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.Tin, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[3];
                    var experimentalEffects = new ExperimentalEffect[5];
                    modules[6] = Thrusters = new Module(CoreInternals, "t", "Thrusters", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        Thrusters,
                        "c",
                        "Clean",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1), new MaterialQuantity(Material.UnexpectedEmissionData, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.DecodedEmissionData, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.AbnormalCompactEmissionsData, 1), new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Tin, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        Thrusters,
                        "d",
                        "Dirty",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Selenium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Cadmium, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1), new MaterialQuantity(Material.PharmaceuticalIsolators, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        Thrusters,
                        "r",
                        "Reinforced",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Carbon, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatConductionWiring, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.HeatConductionWiring, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.HighDensityComposites, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HeatExchangers, 1), new MaterialQuantity(Material.ImperialShielding, 1), new MaterialQuantity(Material.ProprietaryComposites, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(Thrusters, "db", "Double Braced", new[] { new MaterialQuantity(Material.Iron, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.ProprietaryComposites, 1) });
                    experimentalEffects[1] = new ExperimentalEffect(Thrusters, "ddrvs", "Drag Drives", new[] { new MaterialQuantity(Material.Iron, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.SecurityFirmwarePatch, 1) });
                    experimentalEffects[2] = new ExperimentalEffect(Thrusters, "ddstrs", "Drive Distributors", new[] { new MaterialQuantity(Material.Iron, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.SecurityFirmwarePatch, 1) });
                    experimentalEffects[3] = new ExperimentalEffect(Thrusters, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.Iron, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.ProtoLightAlloys, 1) });
                    experimentalEffects[4] = new ExperimentalEffect(Thrusters, "ts", "Thermal Spread", new[] { new MaterialQuantity(Material.Iron, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.HeatVanes, 1) });
                }
            }

            {
                var modules = new Module[12];
                Hardpoints = new ModuleType("h", "Hardpoints", modules);
                {
                    var blueprints = new Blueprint[6];
                    var experimentalEffects = new ExperimentalEffect[9];
                    modules[0] = BeamLaser = new Module(Hardpoints, "bml", "Beam Laser", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        BeamLaser,
                        "e",
                        "Efficient",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.ExceptionalScrambledEmissionData, 1), new MaterialQuantity(Material.HeatExchangers, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.HeatVanes, 1), new MaterialQuantity(Material.IrregularEmissionData, 1), new MaterialQuantity(Material.Selenium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Cadmium, 1), new MaterialQuantity(Material.ProtoHeatRadiators, 1), new MaterialQuantity(Material.UnexpectedEmissionData, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        BeamLaser,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        BeamLaser,
                        "lr",
                        "Long Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.BiotechConductors, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        BeamLaser,
                        "o",
                        "Overcharged",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.PolymerCapacitors, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1), new MaterialQuantity(Material.Zirconium, 1) })
                        }
                    );
                    blueprints[4] = new Blueprint(
                        BeamLaser,
                        "sr",
                        "Short Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.BiotechConductors, 1), new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1) })
                        }
                    );
                    blueprints[5] = new Blueprint(
                        BeamLaser,
                        "s",
                        "Sturdy",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(BeamLaser, "cs", "Concordant Sequence", new[] { new MaterialQuantity(Material.FocusCrystals, 5), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 3), new MaterialQuantity(Material.Zirconium, 1) });
                    experimentalEffects[1] = new ExperimentalEffect(BeamLaser, "db", "Double Braced", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Vanadium, 3) });
                    experimentalEffects[2] = new ExperimentalEffect(BeamLaser, "fc", "Flow Control", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1) });
                    experimentalEffects[3] = new ExperimentalEffect(BeamLaser, "o", "Oversized", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.MechanicalComponents, 3), new MaterialQuantity(Material.Ruthenium, 1) });
                    experimentalEffects[4] = new ExperimentalEffect(BeamLaser, "rs", "Regeneration Sequence", new[] { new MaterialQuantity(Material.ShieldingSensors, 4), new MaterialQuantity(Material.RefinedFocusCrystals, 3), new MaterialQuantity(Material.PeculiarShieldFrequencyData, 1) });
                    experimentalEffects[5] = new ExperimentalEffect(BeamLaser, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.Carbon, 5), new MaterialQuantity(Material.SalvagedAlloys, 5), new MaterialQuantity(Material.Tin, 1) });
                    experimentalEffects[6] = new ExperimentalEffect(BeamLaser, "tc", "Thermal Conduit", new[] { new MaterialQuantity(Material.HeatDispersionPlate, 5), new MaterialQuantity(Material.Sulphur, 5), new MaterialQuantity(Material.TemperedAlloys, 5) });
                    experimentalEffects[7] = new ExperimentalEffect(BeamLaser, "ts", "Thermal Shock", new[] { new MaterialQuantity(Material.FlawedFocusCrystals, 5), new MaterialQuantity(Material.ConductiveComponents, 3), new MaterialQuantity(Material.HeatResistantCeramics, 3), new MaterialQuantity(Material.Tungsten, 3) });
                    experimentalEffects[8] = new ExperimentalEffect(BeamLaser, "tv", "Thermal Vent", new[] { new MaterialQuantity(Material.FlawedFocusCrystals, 5), new MaterialQuantity(Material.ConductivePolymers, 3), new MaterialQuantity(Material.PrecipitatedAlloys, 3) });
                }
                {
                    var blueprints = new Blueprint[8];
                    var experimentalEffects = new ExperimentalEffect[10];
                    modules[1] = BurstLaser = new Module(Hardpoints, "bstl", "Burst Laser", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        BurstLaser,
                        "e",
                        "Efficient",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.ExceptionalScrambledEmissionData, 1), new MaterialQuantity(Material.HeatExchangers, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.HeatVanes, 1), new MaterialQuantity(Material.IrregularEmissionData, 1), new MaterialQuantity(Material.Selenium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Cadmium, 1), new MaterialQuantity(Material.ProtoHeatRadiators, 1), new MaterialQuantity(Material.UnexpectedEmissionData, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        BurstLaser,
                        "f",
                        "Focused",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.PolymerCapacitors, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.MilitarySupercapacitors, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.RefinedFocusCrystals, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        BurstLaser,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        BurstLaser,
                        "lr",
                        "Long Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.BiotechConductors, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) })
                        }
                    );
                    blueprints[4] = new Blueprint(
                        BurstLaser,
                        "o",
                        "Overcharged",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.PolymerCapacitors, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1), new MaterialQuantity(Material.Zirconium, 1) })
                        }
                    );
                    blueprints[5] = new Blueprint(
                        BurstLaser,
                        "rf",
                        "Rapid Fire",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[6] = new Blueprint(
                        BurstLaser,
                        "sr",
                        "Short Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.BiotechConductors, 1), new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1) })
                        }
                    );
                    blueprints[7] = new Blueprint(
                        BurstLaser,
                        "s",
                        "Sturdy",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(BurstLaser, "cs", "Concordant Sequence", new[] { new MaterialQuantity(Material.FocusCrystals, 5), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 3), new MaterialQuantity(Material.Zirconium, 1) });
                    experimentalEffects[1] = new ExperimentalEffect(BurstLaser, "db", "Double Braced", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Vanadium, 3) });
                    experimentalEffects[2] = new ExperimentalEffect(BurstLaser, "fc", "Flow Control", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1) });
                    experimentalEffects[3] = new ExperimentalEffect(BurstLaser, "ii", "Inertial Impact", new[] { new MaterialQuantity(Material.AtypicalDisruptedWakeEchoes, 5), new MaterialQuantity(Material.DistortedShieldCycleRecordings, 5), new MaterialQuantity(Material.FlawedFocusCrystals, 5) });
                    experimentalEffects[4] = new ExperimentalEffect(BurstLaser, "ms", "Multi-Servos", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.FocusCrystals, 4), new MaterialQuantity(Material.ConductivePolymers, 2), new MaterialQuantity(Material.ConfigurableComponents, 2) });
                    experimentalEffects[5] = new ExperimentalEffect(BurstLaser, "o", "Oversized", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.MechanicalComponents, 3), new MaterialQuantity(Material.Ruthenium, 1) });
                    experimentalEffects[6] = new ExperimentalEffect(BurstLaser, "ps", "Phasing Sequence", new[] { new MaterialQuantity(Material.FocusCrystals, 5), new MaterialQuantity(Material.AberrantShieldPatternAnalysis, 3), new MaterialQuantity(Material.ConfigurableComponents, 3), new MaterialQuantity(Material.Niobium, 3) });
                    experimentalEffects[7] = new ExperimentalEffect(BurstLaser, "ss", "Scramble Spectrum", new[] { new MaterialQuantity(Material.CrystalShards, 5), new MaterialQuantity(Material.ExceptionalScrambledEmissionData, 5), new MaterialQuantity(Material.UntypicalShieldScans, 3) });
                    experimentalEffects[8] = new ExperimentalEffect(BurstLaser, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.Carbon, 5), new MaterialQuantity(Material.SalvagedAlloys, 5), new MaterialQuantity(Material.Tin, 1) });
                    experimentalEffects[9] = new ExperimentalEffect(BurstLaser, "ts", "Thermal Shock", new[] { new MaterialQuantity(Material.FlawedFocusCrystals, 5), new MaterialQuantity(Material.ConductiveComponents, 3), new MaterialQuantity(Material.HeatResistantCeramics, 3), new MaterialQuantity(Material.Tungsten, 3) });
                }
                {
                    var blueprints = new Blueprint[8];
                    var experimentalEffects = new ExperimentalEffect[11];
                    modules[2] = Cannon = new Module(Hardpoints, "c", "Cannon", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        Cannon,
                        "e",
                        "Efficient",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.ExceptionalScrambledEmissionData, 1), new MaterialQuantity(Material.HeatExchangers, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.HeatVanes, 1), new MaterialQuantity(Material.IrregularEmissionData, 1), new MaterialQuantity(Material.Selenium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Cadmium, 1), new MaterialQuantity(Material.ProtoHeatRadiators, 1), new MaterialQuantity(Material.UnexpectedEmissionData, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        Cannon,
                        "hc",
                        "High Capacity",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Tin, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.MilitarySupercapacitors, 1), new MaterialQuantity(Material.ProprietaryComposites, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        Cannon,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        Cannon,
                        "lr",
                        "Long Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.BiotechConductors, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) })
                        }
                    );
                    blueprints[4] = new Blueprint(
                        Cannon,
                        "o",
                        "Overcharged",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.PolymerCapacitors, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1), new MaterialQuantity(Material.Zirconium, 1) })
                        }
                    );
                    blueprints[5] = new Blueprint(
                        Cannon,
                        "rf",
                        "Rapid Fire",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[6] = new Blueprint(
                        Cannon,
                        "sr",
                        "Short Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.BiotechConductors, 1), new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1) })
                        }
                    );
                    blueprints[7] = new Blueprint(
                        Cannon,
                        "s",
                        "Sturdy",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(Cannon, "al", "Auto Loader", new[] { new MaterialQuantity(Material.MechanicalEquipment, 4), new MaterialQuantity(Material.HighDensityComposites, 3), new MaterialQuantity(Material.MechanicalComponents, 3) });
                    experimentalEffects[1] = new ExperimentalEffect(Cannon, "dl", "Dispersal Field", new[] { new MaterialQuantity(Material.ConductiveComponents, 5), new MaterialQuantity(Material.HybridCapacitors, 5), new MaterialQuantity(Material.IrregularEmissionData, 5), new MaterialQuantity(Material.WornShieldEmitters, 5) });
                    experimentalEffects[2] = new ExperimentalEffect(Cannon, "db", "Double Braced", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Vanadium, 3) });
                    experimentalEffects[3] = new ExperimentalEffect(Cannon, "fc", "Flow Control", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1) });
                    experimentalEffects[4] = new ExperimentalEffect(Cannon, "fs", "Force Shell", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Zinc, 5), new MaterialQuantity(Material.HeatConductionWiring, 3), new MaterialQuantity(Material.PhaseAlloys, 3) });
                    experimentalEffects[5] = new ExperimentalEffect(Cannon, "hys", "High Yield Shell", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Nickel, 5), new MaterialQuantity(Material.ChemicalManipulators, 3), new MaterialQuantity(Material.ProtoLightAlloys, 3) });
                    experimentalEffects[6] = new ExperimentalEffect(Cannon, "ms", "Multi-servos", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.FocusCrystals, 4), new MaterialQuantity(Material.ConductivePolymers, 2), new MaterialQuantity(Material.ConfigurableComponents, 2) });
                    experimentalEffects[7] = new ExperimentalEffect(Cannon, "o", "Oversized", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.MechanicalComponents, 3), new MaterialQuantity(Material.Ruthenium, 1) });
                    experimentalEffects[8] = new ExperimentalEffect(Cannon, "sr", "Smart Rounds", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.ClassifiedScanDatabanks, 3), new MaterialQuantity(Material.DecodedEmissionData, 3), new MaterialQuantity(Material.SecurityFirmwarePatch, 3) });
                    experimentalEffects[9] = new ExperimentalEffect(Cannon, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.Carbon, 5), new MaterialQuantity(Material.SalvagedAlloys, 5), new MaterialQuantity(Material.Tin, 1) });
                    experimentalEffects[10] = new ExperimentalEffect(Cannon, "tc", "Thermal Cascade", new[] { new MaterialQuantity(Material.HeatConductionWiring, 5), new MaterialQuantity(Material.Phosphorus, 5), new MaterialQuantity(Material.HybridCapacitors, 4), new MaterialQuantity(Material.HighDensityComposites, 3) });
                }
                {
                    var blueprints = new Blueprint[7];
                    var experimentalEffects = new ExperimentalEffect[10];
                    modules[3] = FragmentCannon = new Module(Hardpoints, "fc", "Fragment Cannon", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        FragmentCannon,
                        "ds",
                        "Double Shot",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Carbon, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.MechanicalEquipment, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1), new MaterialQuantity(Material.MechanicalEquipment, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.SecurityFirmwarePatch, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        FragmentCannon,
                        "e",
                        "Efficient",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.ExceptionalScrambledEmissionData, 1), new MaterialQuantity(Material.HeatExchangers, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.HeatVanes, 1), new MaterialQuantity(Material.IrregularEmissionData, 1), new MaterialQuantity(Material.Selenium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Cadmium, 1), new MaterialQuantity(Material.ProtoHeatRadiators, 1), new MaterialQuantity(Material.UnexpectedEmissionData, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        FragmentCannon,
                        "hc",
                        "High Capacity",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.Tin, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.MilitarySupercapacitors, 1), new MaterialQuantity(Material.ProprietaryComposites, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        FragmentCannon,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[4] = new Blueprint(
                        FragmentCannon,
                        "o",
                        "Overcharged",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.PolymerCapacitors, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1), new MaterialQuantity(Material.Zirconium, 1) })
                        }
                    );
                    blueprints[5] = new Blueprint(
                        FragmentCannon,
                        "rf",
                        "Rapid Fire",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[6] = new Blueprint(
                        FragmentCannon,
                        "s",
                        "Sturdy",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(FragmentCannon, "cs", "Corrosive Shell", new[] { new MaterialQuantity(Material.ChemicalStorageUnits, 5), new MaterialQuantity(Material.PrecipitatedAlloys, 4), new MaterialQuantity(Material.Arsenic, 3) });
                    experimentalEffects[1] = new ExperimentalEffect(FragmentCannon, "ds", "Dazzle Shell", new[] { new MaterialQuantity(Material.HybridCapacitors, 5), new MaterialQuantity(Material.MechanicalComponents, 5), new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Manganese, 4) });
                    experimentalEffects[2] = new ExperimentalEffect(FragmentCannon, "db", "Double Braced", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Vanadium, 3) });
                    experimentalEffects[3] = new ExperimentalEffect(FragmentCannon, "dm", "Drag Munitions", new[] { new MaterialQuantity(Material.Carbon, 5), new MaterialQuantity(Material.GridResistors, 5), new MaterialQuantity(Material.Molybdenum, 2) });
                    experimentalEffects[4] = new ExperimentalEffect(FragmentCannon, "fc", "Flow Control", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1) });
                    experimentalEffects[5] = new ExperimentalEffect(FragmentCannon, "ir", "Incendiary Rounds", new[] { new MaterialQuantity(Material.HeatConductionWiring, 5), new MaterialQuantity(Material.Phosphorus, 5), new MaterialQuantity(Material.Sulphur, 5), new MaterialQuantity(Material.PhaseAlloys, 3) });
                    experimentalEffects[6] = new ExperimentalEffect(FragmentCannon, "ms", "Multi-servos", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.FocusCrystals, 4), new MaterialQuantity(Material.ConductivePolymers, 2), new MaterialQuantity(Material.ConfigurableComponents, 2) });
                    experimentalEffects[7] = new ExperimentalEffect(FragmentCannon, "o", "Oversized", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.MechanicalComponents, 3), new MaterialQuantity(Material.Ruthenium, 1) });
                    experimentalEffects[8] = new ExperimentalEffect(FragmentCannon, "ss", "Screening Shell", new[] { new MaterialQuantity(Material.DistortedShieldCycleRecordings, 5), new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.ModifiedConsumerFirmware, 5), new MaterialQuantity(Material.Niobium, 3) });
                    experimentalEffects[9] = new ExperimentalEffect(FragmentCannon, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.Carbon, 5), new MaterialQuantity(Material.SalvagedAlloys, 5), new MaterialQuantity(Material.Tin, 1) });
                }
                {
                    var blueprints = new Blueprint[4];
                    var experimentalEffects = new ExperimentalEffect[10];
                    modules[4] = MineLauncher = new Module(Hardpoints, "ml", "Mine Launcher", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        MineLauncher,
                        "hc",
                        "High Capacity",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.Tin, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.MilitarySupercapacitors, 1), new MaterialQuantity(Material.ProprietaryComposites, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        MineLauncher,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        MineLauncher,
                        "rf",
                        "Rapid Fire",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        MineLauncher,
                        "s",
                        "Sturdy",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(MineLauncher, "db", "Double Braced", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Vanadium, 3) });
                    experimentalEffects[1] = new ExperimentalEffect(MineLauncher, "em", "Emissive Munitions", new[] { new MaterialQuantity(Material.MechanicalEquipment, 5), new MaterialQuantity(Material.HeatExchangers, 3), new MaterialQuantity(Material.Manganese, 3), new MaterialQuantity(Material.UnexpectedEmissionData, 3) });
                    experimentalEffects[2] = new ExperimentalEffect(MineLauncher, "fc", "Flow Control", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1) });
                    experimentalEffects[3] = new ExperimentalEffect(MineLauncher, "id", "Ion Disruption", new[] { new MaterialQuantity(Material.Phosphorus, 5), new MaterialQuantity(Material.Sulphur, 5), new MaterialQuantity(Material.ChemicalDistillery, 3), new MaterialQuantity(Material.ElectrochemicalArrays, 3) });
                    experimentalEffects[4] = new ExperimentalEffect(MineLauncher, "om", "Overload Munitions", new[] { new MaterialQuantity(Material.FilamentComposites, 5), new MaterialQuantity(Material.TaggedEncryptionCodes, 4), new MaterialQuantity(Material.Germanium, 3), new MaterialQuantity(Material.AberrantShieldPatternAnalysis, 2) });
                    experimentalEffects[5] = new ExperimentalEffect(MineLauncher, "o", "Oversized", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.MechanicalComponents, 3), new MaterialQuantity(Material.Ruthenium, 1) });
                    experimentalEffects[6] = new ExperimentalEffect(MineLauncher, "rcnstr", "Radiant Canister", new[] { new MaterialQuantity(Material.HeatDispersionPlate, 4), new MaterialQuantity(Material.PhaseAlloys, 3), new MaterialQuantity(Material.Polonium, 1) });
                    experimentalEffects[7] = new ExperimentalEffect(MineLauncher, "rcsd", "Reverberating Cascade", new[] { new MaterialQuantity(Material.Chromium, 4), new MaterialQuantity(Material.FilamentComposites, 4), new MaterialQuantity(Material.ClassifiedScanDatabanks, 3), new MaterialQuantity(Material.ConfigurableComponents, 2) });
                    experimentalEffects[8] = new ExperimentalEffect(MineLauncher, "slc", "Shift-lock Canister", new[] { new MaterialQuantity(Material.SalvagedAlloys, 5), new MaterialQuantity(Material.TemperedAlloys, 5), new MaterialQuantity(Material.StrangeWakeSolutions, 3) });
                    experimentalEffects[9] = new ExperimentalEffect(MineLauncher, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.Carbon, 5), new MaterialQuantity(Material.SalvagedAlloys, 5), new MaterialQuantity(Material.Tin, 1) });
                }
                {
                    var blueprints = new Blueprint[4];
                    var experimentalEffects = new ExperimentalEffect[10];
                    modules[5] = MissileRack = new Module(Hardpoints, "mr", "Missile Rack", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        MissileRack,
                        "hc",
                        "High Capacity",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.Tin, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.MilitarySupercapacitors, 1), new MaterialQuantity(Material.ProprietaryComposites, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        MissileRack,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        MissileRack,
                        "rf",
                        "Rapid Fire",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        MissileRack,
                        "s",
                        "Sturdy",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(MissileRack, "db", "Double Braced", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Vanadium, 3) });
                    experimentalEffects[1] = new ExperimentalEffect(MissileRack, "em", "Emissive Munitions", new[] { new MaterialQuantity(Material.MechanicalEquipment, 5), new MaterialQuantity(Material.HeatExchangers, 3), new MaterialQuantity(Material.Manganese, 3), new MaterialQuantity(Material.UnexpectedEmissionData, 3) });
                    experimentalEffects[2] = new ExperimentalEffect(MissileRack, "fc", "Flow Control", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1) });
                    experimentalEffects[3] = new ExperimentalEffect(MissileRack, "fsdi", "FSD Interrupt", new[] { new MaterialQuantity(Material.AnomalousFsdTelemetry, 5), new MaterialQuantity(Material.MechanicalEquipment, 5), new MaterialQuantity(Material.ConfigurableComponents, 3), new MaterialQuantity(Material.StrangeWakeSolutions, 3) });
                    experimentalEffects[6] = new ExperimentalEffect(MissileRack, "ms", "Multi-servos", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.FocusCrystals, 4), new MaterialQuantity(Material.ConductivePolymers, 2), new MaterialQuantity(Material.ConfigurableComponents, 2) });
                    experimentalEffects[4] = new ExperimentalEffect(MissileRack, "om", "Overload Munitions", new[] { new MaterialQuantity(Material.FilamentComposites, 5), new MaterialQuantity(Material.TaggedEncryptionCodes, 4), new MaterialQuantity(Material.Germanium, 3), new MaterialQuantity(Material.AberrantShieldPatternAnalysis, 2) });
                    experimentalEffects[5] = new ExperimentalEffect(MissileRack, "o", "Oversized", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.MechanicalComponents, 3), new MaterialQuantity(Material.Ruthenium, 1) });
                    experimentalEffects[7] = new ExperimentalEffect(MissileRack, "pm", "Penetrator Munitions", new[] { new MaterialQuantity(Material.GalvanisingAlloys, 5), new MaterialQuantity(Material.ElectrochemicalArrays, 3), new MaterialQuantity(Material.Zirconium, 3) });
                    experimentalEffects[9] = new ExperimentalEffect(MissileRack, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.Carbon, 5), new MaterialQuantity(Material.SalvagedAlloys, 5), new MaterialQuantity(Material.Tin, 1) });
                    experimentalEffects[8] = new ExperimentalEffect(MissileRack, "tc", "Thermal Cascade", new[] { new MaterialQuantity(Material.HeatConductionWiring, 5), new MaterialQuantity(Material.Phosphorus, 5), new MaterialQuantity(Material.HybridCapacitors, 4), new MaterialQuantity(Material.HighDensityComposites, 3) });
                }
                {
                    var blueprints = new Blueprint[8];
                    var experimentalEffects = new ExperimentalEffect[11];
                    modules[6] = MultiCannon = new Module(Hardpoints, "mc", "Multi Cannon", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        MultiCannon,
                        "e",
                        "Efficient",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.ExceptionalScrambledEmissionData, 1), new MaterialQuantity(Material.HeatExchangers, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.HeatVanes, 1), new MaterialQuantity(Material.IrregularEmissionData, 1), new MaterialQuantity(Material.Selenium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Cadmium, 1), new MaterialQuantity(Material.ProtoHeatRadiators, 1), new MaterialQuantity(Material.UnexpectedEmissionData, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        MultiCannon,
                        "hc",
                        "High Capacity",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Tin, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.MilitarySupercapacitors, 1), new MaterialQuantity(Material.ProprietaryComposites, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        MultiCannon,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        MultiCannon,
                        "lr",
                        "Long Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.BiotechConductors, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) })
                        }
                    );
                    blueprints[4] = new Blueprint(
                        MultiCannon,
                        "o",
                        "Overcharged",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.PolymerCapacitors, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1), new MaterialQuantity(Material.Zirconium, 1) })
                        }
                    );
                    blueprints[5] = new Blueprint(
                        MultiCannon,
                        "rf",
                        "Rapid Fire",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[6] = new Blueprint(
                        MultiCannon,
                        "sr",
                        "Short Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.BiotechConductors, 1), new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1) })
                        }
                    );
                    blueprints[7] = new Blueprint(
                        MultiCannon,
                        "s",
                        "Sturdy",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(MultiCannon, "al", "Auto Loader", new[] { new MaterialQuantity(Material.MechanicalEquipment, 4), new MaterialQuantity(Material.HighDensityComposites, 3), new MaterialQuantity(Material.MechanicalComponents, 3) });
                    experimentalEffects[1] = new ExperimentalEffect(MultiCannon, "cs", "Corrosive Shell", new[] { new MaterialQuantity(Material.ChemicalStorageUnits, 5), new MaterialQuantity(Material.PrecipitatedAlloys, 4), new MaterialQuantity(Material.Arsenic, 3) });
                    experimentalEffects[2] = new ExperimentalEffect(MultiCannon, "db", "Double Braced", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Vanadium, 3) });
                    experimentalEffects[3] = new ExperimentalEffect(MultiCannon, "em", "Emissive Munitions", new[] { new MaterialQuantity(Material.MechanicalEquipment, 4), new MaterialQuantity(Material.HeatExchangers, 3), new MaterialQuantity(Material.Manganese, 3), new MaterialQuantity(Material.UnexpectedEmissionData, 3) });
                    experimentalEffects[4] = new ExperimentalEffect(MultiCannon, "fc", "Flow Control", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1) });
                    experimentalEffects[5] = new ExperimentalEffect(MultiCannon, "ir", "Incendiary Rounds", new[] { new MaterialQuantity(Material.HeatConductionWiring, 5), new MaterialQuantity(Material.Phosphorus, 5), new MaterialQuantity(Material.Sulphur, 5), new MaterialQuantity(Material.PhaseAlloys, 3) });
                    experimentalEffects[6] = new ExperimentalEffect(MultiCannon, "ms", "Multi-servos", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.FocusCrystals, 4), new MaterialQuantity(Material.ConductivePolymers, 2), new MaterialQuantity(Material.ConfigurableComponents, 2) });
                    experimentalEffects[7] = new ExperimentalEffect(MultiCannon, "o", "Oversized", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.MechanicalComponents, 3), new MaterialQuantity(Material.Ruthenium, 1) });
                    experimentalEffects[8] = new ExperimentalEffect(MultiCannon, "sr", "Smart Rounds", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.ClassifiedScanDatabanks, 3), new MaterialQuantity(Material.DecodedEmissionData, 3), new MaterialQuantity(Material.SecurityFirmwarePatch, 3) });
                    experimentalEffects[9] = new ExperimentalEffect(MultiCannon, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.Carbon, 5), new MaterialQuantity(Material.SalvagedAlloys, 5), new MaterialQuantity(Material.Tin, 1) });
                    experimentalEffects[10] = new ExperimentalEffect(MultiCannon, "ts", "Thermal Shock", new[] { new MaterialQuantity(Material.FlawedFocusCrystals, 5), new MaterialQuantity(Material.ConductiveComponents, 3), new MaterialQuantity(Material.HeatResistantCeramics, 3), new MaterialQuantity(Material.Tungsten, 3) });
                }
                {
                    var blueprints = new Blueprint[8];
                    var experimentalEffects = new ExperimentalEffect[11];
                    modules[7] = PlasmaAccelerator = new Module(Hardpoints, "pa", "Plasma Accelerator", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        PlasmaAccelerator,
                        "e",
                        "Efficient",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.ExceptionalScrambledEmissionData, 1), new MaterialQuantity(Material.HeatExchangers, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.HeatVanes, 1), new MaterialQuantity(Material.IrregularEmissionData, 1), new MaterialQuantity(Material.Selenium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Cadmium, 1), new MaterialQuantity(Material.ProtoHeatRadiators, 1), new MaterialQuantity(Material.UnexpectedEmissionData, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        PlasmaAccelerator,
                        "f",
                        "Focused",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.PolymerCapacitors, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.MilitarySupercapacitors, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.RefinedFocusCrystals, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        PlasmaAccelerator,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        PlasmaAccelerator,
                        "lr",
                        "Long Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.BiotechConductors, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) })
                        }
                    );
                    blueprints[4] = new Blueprint(
                        PlasmaAccelerator,
                        "o",
                        "Overcharged",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.PolymerCapacitors, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1), new MaterialQuantity(Material.Zirconium, 1) })
                        }
                    );
                    blueprints[5] = new Blueprint(
                        PlasmaAccelerator,
                        "rf",
                        "Rapid Fire",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[6] = new Blueprint(
                        PlasmaAccelerator,
                        "sr",
                        "Short Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.BiotechConductors, 1), new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1) })
                        }
                    );
                    blueprints[7] = new Blueprint(
                        PlasmaAccelerator,
                        "s",
                        "Sturdy",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(PlasmaAccelerator, "ds", "Dazzle Shell", new[] { new MaterialQuantity(Material.HybridCapacitors, 5), new MaterialQuantity(Material.MechanicalComponents, 5), new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Manganese, 4) });
                    experimentalEffects[1] = new ExperimentalEffect(PlasmaAccelerator, "df", "Dispersal Field", new[] { new MaterialQuantity(Material.ConductiveComponents, 5), new MaterialQuantity(Material.HybridCapacitors, 5), new MaterialQuantity(Material.IrregularEmissionData, 5), new MaterialQuantity(Material.WornShieldEmitters, 5) });
                    experimentalEffects[2] = new ExperimentalEffect(PlasmaAccelerator, "db", "Double Braced", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Vanadium, 3) });
                    experimentalEffects[3] = new ExperimentalEffect(PlasmaAccelerator, "fc", "Flow Control", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1) });
                    experimentalEffects[4] = new ExperimentalEffect(PlasmaAccelerator, "ms", "Multi-Servos", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.FocusCrystals, 4), new MaterialQuantity(Material.ConductivePolymers, 2), new MaterialQuantity(Material.ConfigurableComponents, 2) });
                    experimentalEffects[5] = new ExperimentalEffect(PlasmaAccelerator, "o", "Oversized", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.MechanicalComponents, 3), new MaterialQuantity(Material.Ruthenium, 1) });
                    experimentalEffects[6] = new ExperimentalEffect(PlasmaAccelerator, "psqnc", "Phasing Sequence", new[] { new MaterialQuantity(Material.FocusCrystals, 5), new MaterialQuantity(Material.AberrantShieldPatternAnalysis, 3), new MaterialQuantity(Material.ConfigurableComponents, 3), new MaterialQuantity(Material.Niobium, 3) });
                    experimentalEffects[7] = new ExperimentalEffect(PlasmaAccelerator, "pslg", "Plasma Slug", new[] { new MaterialQuantity(Material.Mercury, 4), new MaterialQuantity(Material.HeatExchangers, 3), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 2), new MaterialQuantity(Material.RefinedFocusCrystals, 2) });
                    experimentalEffects[8] = new ExperimentalEffect(PlasmaAccelerator, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.Carbon, 5), new MaterialQuantity(Material.SalvagedAlloys, 5), new MaterialQuantity(Material.Tin, 1) });
                    experimentalEffects[9] = new ExperimentalEffect(PlasmaAccelerator, "tlb", "Target Lock Breaker", new[] { new MaterialQuantity(Material.Selenium, 5), new MaterialQuantity(Material.SecurityFirmwarePatch, 3), new MaterialQuantity(Material.AdaptiveEncryptorsCapture, 1) });
                    experimentalEffects[10] = new ExperimentalEffect(PlasmaAccelerator, "tc", "Thermal Conduit", new[] { new MaterialQuantity(Material.HeatDispersionPlate, 5), new MaterialQuantity(Material.Sulphur, 5), new MaterialQuantity(Material.TemperedAlloys, 5) });
                }
                {
                    var blueprints = new Blueprint[8];
                    var experimentalEffects = new ExperimentalEffect[10];
                    modules[8] = PulseLaser = new Module(Hardpoints, "pl", "Pulse Laser", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        PulseLaser,
                        "e",
                        "Efficient",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.ExceptionalScrambledEmissionData, 1), new MaterialQuantity(Material.HeatExchangers, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.HeatVanes, 1), new MaterialQuantity(Material.IrregularEmissionData, 1), new MaterialQuantity(Material.Selenium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Cadmium, 1), new MaterialQuantity(Material.ProtoHeatRadiators, 1), new MaterialQuantity(Material.UnexpectedEmissionData, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        PulseLaser,
                        "f",
                        "Focused",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.PolymerCapacitors, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.MilitarySupercapacitors, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.RefinedFocusCrystals, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        PulseLaser,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        PulseLaser,
                        "lr",
                        "Long Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.BiotechConductors, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) })
                        }
                    );
                    blueprints[4] = new Blueprint(
                        PulseLaser,
                        "o",
                        "Overcharged",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.PolymerCapacitors, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1), new MaterialQuantity(Material.Zirconium, 1) })
                        }
                    );
                    blueprints[5] = new Blueprint(
                        PulseLaser,
                        "rf",
                        "Rapid Fire",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[6] = new Blueprint(
                        PulseLaser,
                        "sr",
                        "Short Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.BiotechConductors, 1), new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1) })
                        }
                    );
                    blueprints[7] = new Blueprint(
                        PulseLaser,
                        "s",
                        "Sturdy",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(PulseLaser, "cs", "Concordant Sequence", new[] { new MaterialQuantity(Material.FocusCrystals, 5), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 3), new MaterialQuantity(Material.Zirconium, 1) });
                    experimentalEffects[1] = new ExperimentalEffect(PulseLaser, "db", "Double Braced", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Vanadium, 3) });
                    experimentalEffects[2] = new ExperimentalEffect(PulseLaser, "em", "Emissive Munitions", new[] { new MaterialQuantity(Material.MechanicalEquipment, 4), new MaterialQuantity(Material.HeatExchangers, 3), new MaterialQuantity(Material.Manganese, 3), new MaterialQuantity(Material.UnexpectedEmissionData, 3) });
                    experimentalEffects[3] = new ExperimentalEffect(PulseLaser, "fc", "Flow Control", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1) });
                    experimentalEffects[4] = new ExperimentalEffect(PulseLaser, "ms", "Multi-Servos", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.FocusCrystals, 4), new MaterialQuantity(Material.ConductivePolymers, 2), new MaterialQuantity(Material.ConfigurableComponents, 2) });
                    experimentalEffects[5] = new ExperimentalEffect(PulseLaser, "o", "Oversized", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.MechanicalComponents, 3), new MaterialQuantity(Material.Ruthenium, 1) });
                    experimentalEffects[6] = new ExperimentalEffect(PulseLaser, "ps", "Phasing Sequence", new[] { new MaterialQuantity(Material.FocusCrystals, 5), new MaterialQuantity(Material.AberrantShieldPatternAnalysis, 3), new MaterialQuantity(Material.ConfigurableComponents, 3), new MaterialQuantity(Material.Niobium, 3) });
                    experimentalEffects[7] = new ExperimentalEffect(PulseLaser, "ss", "Scramble Spectrum", new[] { new MaterialQuantity(Material.CrystalShards, 5), new MaterialQuantity(Material.ExceptionalScrambledEmissionData, 5), new MaterialQuantity(Material.UntypicalShieldScans, 3) });
                    experimentalEffects[8] = new ExperimentalEffect(PulseLaser, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.Carbon, 5), new MaterialQuantity(Material.SalvagedAlloys, 5), new MaterialQuantity(Material.Tin, 1) });
                    experimentalEffects[9] = new ExperimentalEffect(PulseLaser, "ts", "Thermal Shock", new[] { new MaterialQuantity(Material.FlawedFocusCrystals, 5), new MaterialQuantity(Material.ConductiveComponents, 3), new MaterialQuantity(Material.HeatResistantCeramics, 3), new MaterialQuantity(Material.Tungsten, 3) });
                }
                {
                    var blueprints = new Blueprint[5];
                    var experimentalEffects = new ExperimentalEffect[8];
                    modules[9] = RailGun = new Module(Hardpoints, "rg", "Rail Gun", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        RailGun,
                        "hc",
                        "High Capacity",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.Tin, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.MilitarySupercapacitors, 1), new MaterialQuantity(Material.ProprietaryComposites, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        RailGun,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        RailGun,
                        "lr",
                        "Long Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.BiotechConductors, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        RailGun,
                        "sr",
                        "Short Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.BiotechConductors, 1), new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1) })
                        }
                    );
                    blueprints[4] = new Blueprint(
                        RailGun,
                        "s",
                        "Sturdy",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(RailGun, "db", "Double Braced", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Vanadium, 3) });
                    experimentalEffects[1] = new ExperimentalEffect(RailGun, "fcscd", "Feedback Cascade", new[] { new MaterialQuantity(Material.FilamentComposites, 5), new MaterialQuantity(Material.OpenSymmetricKeys, 3), new MaterialQuantity(Material.ShieldEmitters, 1) });
                    experimentalEffects[2] = new ExperimentalEffect(RailGun, "fctrl", "Flow Control", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1) });
                    experimentalEffects[3] = new ExperimentalEffect(RailGun, "ms", "Multi-Servos", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.FocusCrystals, 4), new MaterialQuantity(Material.ConductivePolymers, 2), new MaterialQuantity(Material.ConfigurableComponents, 2) });
                    experimentalEffects[4] = new ExperimentalEffect(RailGun, "o", "Oversized", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.MechanicalComponents, 3), new MaterialQuantity(Material.Ruthenium, 1) });
                    experimentalEffects[5] = new ExperimentalEffect(RailGun, "ps", "Plasma Slug", new[] { new MaterialQuantity(Material.Mercury, 4), new MaterialQuantity(Material.HeatExchangers, 3), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 2), new MaterialQuantity(Material.RefinedFocusCrystals, 2) });
                    experimentalEffects[6] = new ExperimentalEffect(RailGun, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.Carbon, 5), new MaterialQuantity(Material.SalvagedAlloys, 5), new MaterialQuantity(Material.Tin, 1) });
                    experimentalEffects[7] = new ExperimentalEffect(RailGun, "sp", "Super Penetrator", new[] { new MaterialQuantity(Material.UntypicalShieldScans, 5), new MaterialQuantity(Material.ProtoLightAlloys, 3), new MaterialQuantity(Material.RefinedFocusCrystals, 3), new MaterialQuantity(Material.Zirconium, 3) });
                }
                {
                    var blueprints = new Blueprint[4];
                    var experimentalEffects = new ExperimentalEffect[8];
                    modules[10] = SeekerMissileRack = new Module(Hardpoints, "smr", "Seeker Missile Rack", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        SeekerMissileRack,
                        "hc",
                        "High Capacity",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.Tin, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.MilitarySupercapacitors, 1), new MaterialQuantity(Material.ProprietaryComposites, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        SeekerMissileRack,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        SeekerMissileRack,
                        "rf",
                        "Rapid Fire",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.ThermicAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        SeekerMissileRack,
                        "s",
                        "Sturdy",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(SeekerMissileRack, "db", "Double Braced", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Vanadium, 3) });
                    experimentalEffects[1] = new ExperimentalEffect(SeekerMissileRack, "dm", "Drag Munitions", new[] { new MaterialQuantity(Material.Carbon, 5), new MaterialQuantity(Material.GridResistors, 5), new MaterialQuantity(Material.Molybdenum, 2) });
                    experimentalEffects[2] = new ExperimentalEffect(SeekerMissileRack, "em", "Emissive Munitions", new[] { new MaterialQuantity(Material.MechanicalEquipment, 4), new MaterialQuantity(Material.HeatExchangers, 3), new MaterialQuantity(Material.Manganese, 3), new MaterialQuantity(Material.UnexpectedEmissionData, 3) });
                    experimentalEffects[3] = new ExperimentalEffect(SeekerMissileRack, "fc", "Flow Control", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1) });
                    experimentalEffects[4] = new ExperimentalEffect(SeekerMissileRack, "ms", "Multi-Servos", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.FocusCrystals, 4), new MaterialQuantity(Material.ConductivePolymers, 2), new MaterialQuantity(Material.ConfigurableComponents, 2) });
                    experimentalEffects[5] = new ExperimentalEffect(SeekerMissileRack, "o", "Oversized", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.MechanicalComponents, 3), new MaterialQuantity(Material.Ruthenium, 1) });
                    experimentalEffects[6] = new ExperimentalEffect(SeekerMissileRack, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.Carbon, 5), new MaterialQuantity(Material.SalvagedAlloys, 5), new MaterialQuantity(Material.Tin, 1) });
                    experimentalEffects[7] = new ExperimentalEffect(SeekerMissileRack, "tc", "Thermal Cascade", new[] { new MaterialQuantity(Material.HeatConductionWiring, 5), new MaterialQuantity(Material.Phosphorus, 5), new MaterialQuantity(Material.HybridCapacitors, 4), new MaterialQuantity(Material.HighDensityComposites, 3) });
                }
                {
                    var blueprints = new Blueprint[2];
                    var experimentalEffects = new ExperimentalEffect[7];
                    modules[11] = TorpedoPylon = new Module(Hardpoints, "tp", "Torpedo Pylon", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        TorpedoPylon,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        TorpedoPylon,
                        "s",
                        "Sturdy",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(TorpedoPylon, "db", "Double Braced", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.Vanadium, 3) });
                    experimentalEffects[1] = new ExperimentalEffect(TorpedoPylon, "fc", "Flow Control", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.HybridCapacitors, 3), new MaterialQuantity(Material.ModifiedEmbeddedFirmware, 1) });
                    experimentalEffects[2] = new ExperimentalEffect(TorpedoPylon, "mlm", "Mass Lock Munition", new[] { new MaterialQuantity(Material.MechanicalEquipment, 5), new MaterialQuantity(Material.AberrantShieldPatternAnalysis, 3), new MaterialQuantity(Material.HighDensityComposites, 3) });
                    experimentalEffects[3] = new ExperimentalEffect(TorpedoPylon, "o", "Oversized", new[] { new MaterialQuantity(Material.MechanicalScrap, 5), new MaterialQuantity(Material.MechanicalComponents, 3), new MaterialQuantity(Material.Ruthenium, 1) });
                    experimentalEffects[4] = new ExperimentalEffect(TorpedoPylon, "pp", "Penetrator Payload", new[] { new MaterialQuantity(Material.AnomalousBulkScanData, 5), new MaterialQuantity(Material.MechanicalComponents, 3), new MaterialQuantity(Material.Selenium, 3), new MaterialQuantity(Material.Tungsten, 3) });
                    experimentalEffects[5] = new ExperimentalEffect(TorpedoPylon, "rc", "Reverberating Cascade", new[] { new MaterialQuantity(Material.Chromium, 4), new MaterialQuantity(Material.FilamentComposites, 4), new MaterialQuantity(Material.ClassifiedScanDatabanks, 3), new MaterialQuantity(Material.ConfigurableComponents, 2) });
                    experimentalEffects[6] = new ExperimentalEffect(TorpedoPylon, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.Carbon, 5), new MaterialQuantity(Material.SalvagedAlloys, 5), new MaterialQuantity(Material.Tin, 1) });
                }
            }

            {
                var modules = new Module[12];
                OptionalInternals = new ModuleType("oi", "Optional Internals", modules);
                {
                    var blueprints = new Blueprint[1];
                    modules[0] = AutoFieldMaintenanceUnit = new Module(OptionalInternals, "amfu", "Auto Field-Maintenance Unit", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        AutoFieldMaintenanceUnit,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[3];
                    modules[1] = CollectorLimpetController = new Module(OptionalInternals, "clp", "Collector Limpet Controller", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        CollectorLimpetController,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        CollectorLimpetController,
                        "r",
                        "Reinforced",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        CollectorLimpetController,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[1];
                    modules[2] = DetailedSurfaceScanner = new Module(OptionalInternals, "dss", "Detailed Surface Scanner", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        DetailedSurfaceScanner,
                        "epsr",
                        "Expanded Probe Scanning Radius",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.PhaseAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1), new MaterialQuantity(Material.Tin, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[2];
                    modules[3] = FrameShiftDriveInterdictor = new Module(OptionalInternals, "fsdi", "Frame Shift Drive Interdictor", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        FrameShiftDriveInterdictor,
                        "eca",
                        "Expanded Capture Arc",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.UnusualEncryptedFiles, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.GridResistors, 1), new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.TaggedEncryptionCodes, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.DivergendScanData, 1), new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.StrangeWakeSolutions, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ClassifiedScanFragment, 1), new MaterialQuantity(Material.EccentricHyperspaceTrajectories, 1), new MaterialQuantity(Material.MechanicalComponents, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        FrameShiftDriveInterdictor,
                        "lr",
                        "Long Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.UnusualEncryptedFiles, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.AtypicalDisruptedWakeEchoes, 1), new MaterialQuantity(Material.TaggedEncryptionCodes, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.AnomalousBulkScanData, 1), new MaterialQuantity(Material.AnomalousFsdTelemetry, 1), new MaterialQuantity(Material.OpenSymmetricKeys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.AtypicalEncryptionArchives, 1), new MaterialQuantity(Material.StrangeWakeSolutions, 1), new MaterialQuantity(Material.UnidentifiedScanArchives, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.AdaptiveEncryptorsCapture, 1), new MaterialQuantity(Material.ClassifiedScanDatabanks, 1), new MaterialQuantity(Material.EccentricHyperspaceTrajectories, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[1];
                    modules[4] = FuelScoop = new Module(OptionalInternals, "fs", "Fuel Scoop", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        FuelScoop,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[3];
                    modules[5] = FuelTransferLimpetController = new Module(OptionalInternals, "ftlc", "Fuel Transfer Limpet Controller", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        FuelTransferLimpetController,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        FuelTransferLimpetController,
                        "r",
                        "Reinforced",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        FuelTransferLimpetController,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[3];
                    modules[6] = HatchBreakerLimpetController = new Module(OptionalInternals, "hblc", "Hatch Breaker Limpet Controller", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        HatchBreakerLimpetController,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        HatchBreakerLimpetController,
                        "r",
                        "Reinforced",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        HatchBreakerLimpetController,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[5];
                    var experimentalEffects = new ExperimentalEffect[4];
                    modules[7] = HullReinforcementPackage = new Module(OptionalInternals, "hrp", "Hull Reinforcement Package", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        HullReinforcementPackage,
                        "br",
                        "Blast Resistant",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.SalvagedAlloys, 1), new MaterialQuantity(Material.Vanadium, 1), new MaterialQuantity(Material.Zirconium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.GalvanisingAlloys, 1), new MaterialQuantity(Material.Mercury, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.Ruthenium, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        HullReinforcementPackage,
                        "hd",
                        "Heavy Duty",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Carbon, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompactComposites, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        HullReinforcementPackage,
                        "kr",
                        "Kinetic Resistant",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.SalvagedAlloys, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.GalvanisingAlloys, 1), new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.PhaseAlloys, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        HullReinforcementPackage,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.ProprietaryComposites, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.MilitaryGradeAlloys, 1), new MaterialQuantity(Material.Tin, 1) })
                        }
                    );
                    blueprints[4] = new Blueprint(
                        HullReinforcementPackage,
                        "tr",
                        "Thermal Resistant",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.HeatConductionWiring, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatConductionWiring, 1), new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.HeatExchangers, 1), new MaterialQuantity(Material.SalvagedAlloys, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.GalvanisingAlloys, 1), new MaterialQuantity(Material.HeatVanes, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoHeatRadiators, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(HullReinforcementPackage, "ap", "Angled Plating", new[] { new MaterialQuantity(Material.Carbon, 5), new MaterialQuantity(Material.TemperedAlloys, 5), new MaterialQuantity(Material.HighDensityComposites, 3), new MaterialQuantity(Material.Zirconium, 3) });
                    experimentalEffects[1] = new ExperimentalEffect(HullReinforcementPackage, "dp", "Deep Plating", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.Molybdenum, 3), new MaterialQuantity(Material.Ruthenium, 2) });
                    experimentalEffects[2] = new ExperimentalEffect(HullReinforcementPackage, "lp", "Layered Plating", new[] { new MaterialQuantity(Material.HeatConductionWiring, 5), new MaterialQuantity(Material.ShieldingSensors, 3), new MaterialQuantity(Material.Tungsten, 3) });
                    experimentalEffects[3] = new ExperimentalEffect(HullReinforcementPackage, "rp", "Reflective Plating", new[] { new MaterialQuantity(Material.HeatConductionWiring, 5), new MaterialQuantity(Material.Zinc, 4), new MaterialQuantity(Material.HeatDispersionPlate, 3), new MaterialQuantity(Material.ProtoLightAlloys, 1) });
                }
                {
                    var blueprints = new Blueprint[3];
                    modules[8] = ProspectorLimpetController = new Module(OptionalInternals, "plc", "Prospector Limpet Controller", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        ProspectorLimpetController,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        ProspectorLimpetController,
                        "r",
                        "Reinforced",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        ProspectorLimpetController,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[1];
                    modules[9] = Refinery = new Module(OptionalInternals, "r", "Refinery", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        Refinery,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[2];
                    var experimentalEffects = new ExperimentalEffect[5];
                    modules[10] = ShieldCellBank = new Module(OptionalInternals, "scb", "Shield Cell Bank", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        ShieldCellBank,
                        "rc",
                        "Rapid Charge",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.GridResistors, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.HybridCapacitors, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1), new MaterialQuantity(Material.Sulphur, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Chromium, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.ThermicAlloys, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        ShieldCellBank,
                        "s",
                        "Specialised",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.SpecialisedLegacyFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1), new MaterialQuantity(Material.ExceptionalScrambledEmissionData, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.CrackedIndustrialFirmware, 1), new MaterialQuantity(Material.Yttrium, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(ShieldCellBank, "bc", "Boss Cells", new[] { new MaterialQuantity(Material.ChemicalStorageUnits, 5), new MaterialQuantity(Material.Chromium, 3), new MaterialQuantity(Material.PolymerCapacitors, 1) });
                    experimentalEffects[1] = new ExperimentalEffect(ShieldCellBank, "db", "Double Braced", new[] { new MaterialQuantity(Material.ChemicalStorageUnits, 5), new MaterialQuantity(Material.Chromium, 3), new MaterialQuantity(Material.Yttrium, 1) });
                    experimentalEffects[2] = new ExperimentalEffect(ShieldCellBank, "fc", "Flow Control", new[] { new MaterialQuantity(Material.ChemicalStorageUnits, 5), new MaterialQuantity(Material.Chromium, 3), new MaterialQuantity(Material.ConductivePolymers, 1) });
                    experimentalEffects[3] = new ExperimentalEffect(ShieldCellBank, "rc", "Recycling Cell", new[] { new MaterialQuantity(Material.ChemicalStorageUnits, 5), new MaterialQuantity(Material.Chromium, 3), new MaterialQuantity(Material.ConfigurableComponents, 1) });
                    experimentalEffects[4] = new ExperimentalEffect(ShieldCellBank, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.ChemicalStorageUnits, 5), new MaterialQuantity(Material.Chromium, 3), new MaterialQuantity(Material.ProtoLightAlloys, 1) });
                }
                {
                    var blueprints = new Blueprint[4];
                    var experimentalEffects = new ExperimentalEffect[8];
                    modules[11] = ShieldGenerator = new Module(OptionalInternals, "sg", "Shield Generator", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        ShieldGenerator,
                        "elp",
                        "Enhanced Low Power",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.DistortedShieldCycleRecordings, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.DistortedShieldCycleRecordings, 1), new MaterialQuantity(Material.Germanium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.DistortedShieldCycleRecordings, 1), new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.PrecipitatedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.InconsistentShieldSoakAnalysis, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.ThermicAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.MilitaryGradeAlloys, 1), new MaterialQuantity(Material.Tin, 1), new MaterialQuantity(Material.UntypicalShieldScans, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        ShieldGenerator,
                        "kr",
                        "Kinetic Resistant",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.DistortedShieldCycleRecordings, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.DistortedShieldCycleRecordings, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.DistortedShieldCycleRecordings, 1), new MaterialQuantity(Material.ModifiedConsumerFirmware, 1), new MaterialQuantity(Material.Selenium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.InconsistentShieldSoakAnalysis, 1), new MaterialQuantity(Material.Mercury, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.RefinedFocusCrystals, 1), new MaterialQuantity(Material.Ruthenium, 1), new MaterialQuantity(Material.UntypicalShieldScans, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        ShieldGenerator,
                        "r",
                        "Reinforced",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ConfigurableComponents, 1), new MaterialQuantity(Material.Manganese, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Arsenic, 1), new MaterialQuantity(Material.ConductivePolymers, 1), new MaterialQuantity(Material.ImprovisedComponents, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        ShieldGenerator,
                        "tr",
                        "Thermal Resistant",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.DistortedShieldCycleRecordings, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.DistortedShieldCycleRecordings, 1), new MaterialQuantity(Material.Germanium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.DistortedShieldCycleRecordings, 1), new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.Selenium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.InconsistentShieldSoakAnalysis, 1), new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.Mercury, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.RefinedFocusCrystals, 1), new MaterialQuantity(Material.Ruthenium, 1), new MaterialQuantity(Material.UntypicalShieldScans, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(ShieldGenerator, "db", "Double Braced", new[] { new MaterialQuantity(Material.WornShieldEmitters, 5), new MaterialQuantity(Material.FlawedFocusCrystals, 3), new MaterialQuantity(Material.ConfigurableComponents, 1) });
                    experimentalEffects[1] = new ExperimentalEffect(ShieldGenerator, "fc", "Fast Charge", new[] { new MaterialQuantity(Material.WornShieldEmitters, 5), new MaterialQuantity(Material.FlawedFocusCrystals, 3), new MaterialQuantity(Material.CompoundShielding, 1) });
                    experimentalEffects[2] = new ExperimentalEffect(ShieldGenerator, "fb", "Force Block", new[] { new MaterialQuantity(Material.WornShieldEmitters, 5), new MaterialQuantity(Material.FlawedFocusCrystals, 3), new MaterialQuantity(Material.DecodedEmissionData, 1) });
                    experimentalEffects[3] = new ExperimentalEffect(ShieldGenerator, "hc", "Hi-Cap", new[] { new MaterialQuantity(Material.WornShieldEmitters, 5), new MaterialQuantity(Material.FlawedFocusCrystals, 3), new MaterialQuantity(Material.ConductivePolymers, 1) });
                    experimentalEffects[4] = new ExperimentalEffect(ShieldGenerator, "ld", "Lo-Draw", new[] { new MaterialQuantity(Material.WornShieldEmitters, 5), new MaterialQuantity(Material.FlawedFocusCrystals, 3), new MaterialQuantity(Material.ConductivePolymers, 1) });
                    experimentalEffects[5] = new ExperimentalEffect(ShieldGenerator, "mw", "Multi-Weave", new[] { new MaterialQuantity(Material.WornShieldEmitters, 5), new MaterialQuantity(Material.FlawedFocusCrystals, 3), new MaterialQuantity(Material.AberrantShieldPatternAnalysis, 1) });
                    experimentalEffects[6] = new ExperimentalEffect(ShieldGenerator, "sd", "Stripped Down", new[] { new MaterialQuantity(Material.WornShieldEmitters, 5), new MaterialQuantity(Material.FlawedFocusCrystals, 3), new MaterialQuantity(Material.ProtoLightAlloys, 1) });
                    experimentalEffects[7] = new ExperimentalEffect(ShieldGenerator, "tb", "Thermo Block", new[] { new MaterialQuantity(Material.WornShieldEmitters, 5), new MaterialQuantity(Material.FlawedFocusCrystals, 3), new MaterialQuantity(Material.HeatVanes, 1) });
                }
            }

            {
                var modules = new Module[8];
                UtilityMounts = new ModuleType("um", "Utility Mounts", modules);
                {
                    var blueprints = new Blueprint[4];
                    modules[0] = ChaffLauncher = new Module(UtilityMounts, "cl", "Chaff Launcher", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        ChaffLauncher,
                        "ac",
                        "Ammo Capacity",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        ChaffLauncher,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        ChaffLauncher,
                        "r",
                        "Reinforced",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        ChaffLauncher,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[3];
                    modules[1] = ElectronicCountermeasure = new Module(UtilityMounts, "ec", "Electronic Countermeasure", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        ElectronicCountermeasure,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        ElectronicCountermeasure,
                        "r",
                        "Reinforced",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        ElectronicCountermeasure,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[6];
                    modules[2] = FrameShiftWakeScanner = new Module(UtilityMounts, "fsws", "Frame Shift Wake Scanner", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        FrameShiftWakeScanner,
                        "fs",
                        "Fast Scan",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.FlawedFocusCrystals, 1), new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.FlawedFocusCrystals, 1), new MaterialQuantity(Material.OpenSymmetricKeys, 1), new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.AtypicalEncryptionArchives, 1), new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.Manganese, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.AdaptiveEncryptorsCapture, 1), new MaterialQuantity(Material.Arsenic, 1), new MaterialQuantity(Material.RefinedFocusCrystals, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        FrameShiftWakeScanner,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        FrameShiftWakeScanner,
                        "lr",
                        "Long Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HybridCapacitors, 1), new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.HybridCapacitors, 1), new MaterialQuantity(Material.Iron, 1), new MaterialQuantity(Material.UnexpectedEmissionData, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.DecodedEmissionData, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.Germanium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.AbnormalCompactEmissionsData, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.PolymerCapacitors, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        FrameShiftWakeScanner,
                        "r",
                        "Reinforced",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[4] = new Blueprint(
                        FrameShiftWakeScanner,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                    blueprints[5] = new Blueprint(
                        FrameShiftWakeScanner,
                        "wa",
                        "Wide Angle",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ClassifiedScanDatabanks, 1), new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.DivergendScanData, 1), new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.Niobium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ClassifiedScanFragment, 1), new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.Tin, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[4];
                    modules[3] = HeatSinkLauncher = new Module(UtilityMounts, "hsl", "Heat Sink Launcher", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        HeatSinkLauncher,
                        "ac",
                        "Ammo Capacity",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.Vanadium, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        HeatSinkLauncher,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        HeatSinkLauncher,
                        "r",
                        "Reinforced",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        HeatSinkLauncher,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[6];
                    modules[4] = KillWarrantScanner = new Module(UtilityMounts, "kws", "Kill Warrant Scanner", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        KillWarrantScanner,
                        "fs",
                        "Fast Scan",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.FlawedFocusCrystals, 1), new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.FlawedFocusCrystals, 1), new MaterialQuantity(Material.OpenSymmetricKeys, 1), new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.AtypicalEncryptionArchives, 1), new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.Manganese, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.AdaptiveEncryptorsCapture, 1), new MaterialQuantity(Material.Arsenic, 1), new MaterialQuantity(Material.RefinedFocusCrystals, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        KillWarrantScanner,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        KillWarrantScanner,
                        "lr",
                        "Long Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HybridCapacitors, 1), new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.HybridCapacitors, 1), new MaterialQuantity(Material.Iron, 1), new MaterialQuantity(Material.UnexpectedEmissionData, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.DecodedEmissionData, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.Germanium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.AbnormalCompactEmissionsData, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.PolymerCapacitors, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        KillWarrantScanner,
                        "r",
                        "Reinforced",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[4] = new Blueprint(
                        KillWarrantScanner,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                    blueprints[5] = new Blueprint(
                        KillWarrantScanner,
                        "wa",
                        "Wide Angle",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ClassifiedScanDatabanks, 1), new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.DivergendScanData, 1), new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.Niobium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ClassifiedScanFragment, 1), new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.Tin, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[6];
                    modules[5] = ManifestScanner = new Module(UtilityMounts, "ms", "Manifest Scanner", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        ManifestScanner,
                        "fs",
                        "Fast Scan",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.FlawedFocusCrystals, 1), new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.FlawedFocusCrystals, 1), new MaterialQuantity(Material.OpenSymmetricKeys, 1), new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.AtypicalEncryptionArchives, 1), new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.Manganese, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.AdaptiveEncryptorsCapture, 1), new MaterialQuantity(Material.Arsenic, 1), new MaterialQuantity(Material.RefinedFocusCrystals, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        ManifestScanner,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        ManifestScanner,
                        "lr",
                        "Long Range",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HybridCapacitors, 1), new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.HybridCapacitors, 1), new MaterialQuantity(Material.Iron, 1), new MaterialQuantity(Material.UnexpectedEmissionData, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.DecodedEmissionData, 1), new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.Germanium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.AbnormalCompactEmissionsData, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.PolymerCapacitors, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        ManifestScanner,
                        "r",
                        "Reinforced",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[4] = new Blueprint(
                        ManifestScanner,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                    blueprints[5] = new Blueprint(
                        ManifestScanner,
                        "wa",
                        "Wide Angle",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ClassifiedScanDatabanks, 1), new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.MechanicalScrap, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.DivergendScanData, 1), new MaterialQuantity(Material.MechanicalEquipment, 1), new MaterialQuantity(Material.Niobium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ClassifiedScanFragment, 1), new MaterialQuantity(Material.MechanicalComponents, 1), new MaterialQuantity(Material.Tin, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[4];
                    modules[6] = PointDefence = new Module(UtilityMounts, "pdfc", "Point Defence", blueprints, Array.Empty<ExperimentalEffect>());

                    blueprints[0] = new Blueprint(
                        PointDefence,
                        "ac",
                        "Ammo Capacity",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.MechanicalScrap, 1), new MaterialQuantity(Material.Niobium, 1), new MaterialQuantity(Material.Vanadium, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        PointDefence,
                        "l",
                        "Lightweight",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.PhaseAlloys, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ProtoLightAlloys, 1), new MaterialQuantity(Material.ProtoRadiolicAlloys, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        PointDefence,
                        "r",
                        "Reinforced",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Nickel, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Nickel, 1), new MaterialQuantity(Material.ShieldEmitters, 1), new MaterialQuantity(Material.Tungsten, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Tungsten, 1), new MaterialQuantity(Material.Zinc, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.Molybdenum, 1), new MaterialQuantity(Material.Technetium, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        PointDefence,
                        "s",
                        "Shielded",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.WornShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.Carbon, 1), new MaterialQuantity(Material.HighDensityComposites, 1), new MaterialQuantity(Material.ShieldEmitters, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ProprietaryComposites, 1), new MaterialQuantity(Material.ShieldingSensors, 1), new MaterialQuantity(Material.Vanadium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.CompoundShielding, 1), new MaterialQuantity(Material.CoreDynamicsComposites, 1), new MaterialQuantity(Material.Tungsten, 1) })
                        }
                    );
                }
                {
                    var blueprints = new Blueprint[5];
                    var experimentalEffects = new ExperimentalEffect[6];
                    modules[7] = ShieldBooster = new Module(UtilityMounts, "sb", "Shield Booster", blueprints, experimentalEffects);

                    blueprints[0] = new Blueprint(
                        ShieldBooster,
                        "br",
                        "Blast Resistant",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Iron, 1), new MaterialQuantity(Material.FocusCrystals, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.RefinedFocusCrystals, 1), new MaterialQuantity(Material.UntypicalShieldScans, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.AberrantShieldPatternAnalysis, 1), new MaterialQuantity(Material.ExquisiteFocusCrystals, 1), new MaterialQuantity(Material.Niobium, 1) })
                        }
                    );
                    blueprints[1] = new Blueprint(
                        ShieldBooster,
                        "l",
                        "Heavy Duty",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.GridResistors, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.DistortedShieldCycleRecordings, 1), new MaterialQuantity(Material.HybridCapacitors, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.DistortedShieldCycleRecordings, 1), new MaterialQuantity(Material.HybridCapacitors, 1), new MaterialQuantity(Material.Niobium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ElectrochemicalArrays, 1), new MaterialQuantity(Material.InconsistentShieldSoakAnalysis, 1), new MaterialQuantity(Material.Tin, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.Antimony, 1), new MaterialQuantity(Material.PolymerCapacitors, 1), new MaterialQuantity(Material.UntypicalShieldScans, 1) })
                        }
                    );
                    blueprints[2] = new Blueprint(
                        ShieldBooster,
                        "kr",
                        "Kinetic Resistant",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.GridResistors, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.HybridCapacitors, 1), new MaterialQuantity(Material.SalvagedAlloys, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.GalvanisingAlloys, 1), new MaterialQuantity(Material.RefinedFocusCrystals, 1), new MaterialQuantity(Material.UntypicalShieldScans, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.AberrantShieldPatternAnalysis, 1), new MaterialQuantity(Material.ExquisiteFocusCrystals, 1), new MaterialQuantity(Material.PhaseAlloys, 1) })
                        }
                    );
                    blueprints[3] = new Blueprint(
                        ShieldBooster,
                        "ra",
                        "Resistance Augmented",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.ConductiveComponents, 1), new MaterialQuantity(Material.FocusCrystals, 1), new MaterialQuantity(Material.Phosphorus, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.Manganese, 1), new MaterialQuantity(Material.RefinedFocusCrystals, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.ConductiveCeramics, 1), new MaterialQuantity(Material.ImperialShielding, 1), new MaterialQuantity(Material.RefinedFocusCrystals, 1) })
                        }
                    );
                    blueprints[4] = new Blueprint(
                        ShieldBooster,
                        "tr",
                        "Thermal Resistant",
                        new[]
                        {
                            new BlueprintGradeRequirements(BlueprintGrade.Grade1, new[] { new MaterialQuantity(Material.Iron, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade2, new[] { new MaterialQuantity(Material.HeatConductionWiring, 1), new MaterialQuantity(Material.Germanium, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade3, new[] { new MaterialQuantity(Material.HeatConductionWiring, 1), new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.FocusCrystals, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade4, new[] { new MaterialQuantity(Material.HeatDispersionPlate, 1), new MaterialQuantity(Material.RefinedFocusCrystals, 1), new MaterialQuantity(Material.UntypicalShieldScans, 1) }),
                            new BlueprintGradeRequirements(BlueprintGrade.Grade5, new[] { new MaterialQuantity(Material.AberrantShieldPatternAnalysis, 1), new MaterialQuantity(Material.ExquisiteFocusCrystals, 1), new MaterialQuantity(Material.HeatExchangers, 1) })
                        }
                    );

                    experimentalEffects[0] = new ExperimentalEffect(ShieldBooster, "bb", "Blast Block", new[] { new MaterialQuantity(Material.InconsistentShieldSoakAnalysis, 5), new MaterialQuantity(Material.HeatDispersionPlate, 3), new MaterialQuantity(Material.HeatResistantCeramics, 3), new MaterialQuantity(Material.Selenium, 2) });
                    experimentalEffects[1] = new ExperimentalEffect(ShieldBooster, "db", "Double Braced", new[] { new MaterialQuantity(Material.DistortedShieldCycleRecordings, 5), new MaterialQuantity(Material.ShieldEmitters, 3), new MaterialQuantity(Material.GalvanisingAlloys, 3) });
                    experimentalEffects[2] = new ExperimentalEffect(ShieldBooster, "fc", "Flow Control", new[] { new MaterialQuantity(Material.InconsistentShieldSoakAnalysis, 5), new MaterialQuantity(Material.FocusCrystals, 3), new MaterialQuantity(Material.Niobium, 3), new MaterialQuantity(Material.SecurityFirmwarePatch, 2) });
                    experimentalEffects[3] = new ExperimentalEffect(ShieldBooster, "fb", "Force Block", new[] { new MaterialQuantity(Material.UnidentifiedScanArchives, 5), new MaterialQuantity(Material.ShieldingSensors, 3), new MaterialQuantity(Material.AberrantShieldPatternAnalysis, 3) });
                    experimentalEffects[4] = new ExperimentalEffect(ShieldBooster, "sc", "Super Capacitors", new[] { new MaterialQuantity(Material.CompactComposites, 5), new MaterialQuantity(Material.UntypicalShieldScans, 3), new MaterialQuantity(Material.Cadmium, 3) });
                    experimentalEffects[5] = new ExperimentalEffect(ShieldBooster, "tb", "Thermo Block", new[] { new MaterialQuantity(Material.AnomalousBulkScanData, 5), new MaterialQuantity(Material.ConductiveCeramics, 3), new MaterialQuantity(Material.HeatVanes, 3) });
                }
            }

            All = CoreInternals
                .Modules
                .Concat(Hardpoints.Modules)
                .Concat(OptionalInternals.Modules)
                .Concat(UtilityMounts.Modules)
                .OrderBy(module => module.Name, StringComparer.OrdinalIgnoreCase)
                .ToArray();

            var modulesById = new Dictionary<string, Module>(All.Count, StringComparer.OrdinalIgnoreCase);
            foreach (var module in All)
                modulesById.Add(module.Id, module);
            _modulesById = modulesById;
        }
    }
}