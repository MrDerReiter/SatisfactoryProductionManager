export default [
  // --- INGOTS ---
  {
    name: "IronIngot",
    category: "Ingots",
    machine: "Smelter",
    outputs: [["IronIngot", 30]],
    inputs: [["IronOre", 30]]
  },
  {
    name: "IronAlloyIngot",
    category: "Ingots",
    machine: "Foundry",
    outputs: [["IronIngot", 75]],
    inputs: [["IronOre", 40], ["CopperOre", 10]]
  },
  {
    name: "BasicIronIngot",
    category: "Ingots",
    machine: "Foundry",
    outputs: [["IronIngot", 50]],
    inputs: [["IronOre", 25], ["Limestone", 40]]
  },
  {
    name: "PureIronIngot",
    category: "Ingots",
    machine: "Refinery",
    outputs: [["IronIngot", 65]],
    inputs: [["IronOre", 35], ["Water", 20]]
  },
  {
    name: "LeachedIronIngot",
    category: "Ingots",
    machine: "Refinery",
    outputs: [["IronIngot", 100]],
    inputs: [["IronOre", 50], ["SulfuricAcid", 10]]
  },
  {
    name: "CopperIngot",
    category: "Ingots",
    machine: "Smelter",
    outputs: [["CopperIngot", 30]],
    inputs: [["CopperOre", 30]]
  },
  {
    name: "CopperAlloyIngot",
    category: "Ingots",
    machine: "Foundry",
    outputs: [["CopperIngot", 100]],
    inputs: [["CopperOre", 50], ["IronOre", 50]]
  },
  {
    name: "TemperedCopperIngot",
    category: "Ingots",
    machine: "Foundry",
    outputs: [["CopperIngot", 60]],
    inputs: [["CopperOre", 25], ["PetroleumCoke", 40]]
  },
  {
    name: "PureCopperIngot",
    category: "Ingots",
    machine: "Refinery",
    outputs: [["CopperIngot", 37.5]],
    inputs: [["CopperOre", 15], ["Water", 10]]
  },
  {
    name: "LeachedCopperIngot",
    category: "Ingots",
    machine: "Refinery",
    outputs: [["CopperIngot", 110]],
    inputs: [["CopperOre", 45], ["SulfuricAcid", 25]]
  },
  {
    name: "SteelIngot",
    category: "Ingots",
    machine: "Foundry",
    outputs: [["SteelIngot", 45]],
    inputs: [["IronOre", 45], ["Coal", 45]]
  },
  {
    name: "CokeSteelIngot",
    category: "Ingots",
    machine: "Foundry",
    outputs: [["SteelIngot", 100]],
    inputs: [["IronOre", 75], ["PetroleumCoke", 75]]
  },
  {
    name: "CompactedSteelIngot",
    category: "Ingots",
    machine: "Foundry",
    outputs: [["SteelIngot", 37.5]],
    inputs: [["IronOre", 22.5], ["CompactedCoal", 11.25]]
  },
  {
    name: "SolidSteelIngot",
    category: "Ingots",
    machine: "Foundry",
    outputs: [["SteelIngot", 60]],
    inputs: [["IronIngot", 40], ["Coal", 40]]
  },
  {
    name: "CateriumIngot",
    category: "Ingots",
    machine: "Smelter",
    outputs: [["CateriumIngot", 15]],
    inputs: [["CateriumOre", 45]]
  },
  {
    name: "TemperedCateriumIngot",
    category: "Ingots",
    machine: "Foundry",
    outputs: [["CateriumIngot", 22.5]],
    inputs: [["CateriumOre", 45], ["PetroleumCoke", 15]]
  },
  {
    name: "PureCateriumIngot",
    category: "Ingots",
    machine: "Refinery",
    outputs: [["CateriumIngot", 12]],
    inputs: [["CateriumOre", 24], ["Water", 24]]
  },
  {
    name: "LeachedCateriumIngot",
    category: "Ingots",
    machine: "Refinery",
    outputs: [["CateriumIngot", 36]],
    inputs: [["CateriumOre", 54], ["SulfuricAcid", 30]]
  },
  {
    name: "AluminumIngot",
    category: "Ingots",
    machine: "Foundry",
    outputs: [["AluminumIngot", 60]],
    inputs: [["AluminumScrap", 90], ["Silica", 75]]
  },
  {
    name: "PureAluminumIngot",
    category: "Ingots",
    machine: "Smelter",
    outputs: [["AluminumIngot", 30]],
    inputs: [["AluminumScrap", 60]]
  },
  {
    name: "FicsiteIngot(Iron)",
    category: "Ingots",
    machine: "Converter",
    outputs: [["FicsiteIngot", 10]],
    inputs: [["IronIngot", 240], ["ReanimatedSAM", 40]]
  },
  {
    name: "FicsiteIngot(Aluminum)",
    category: "Ingots",
    machine: "Converter",
    outputs: [["FicsiteIngot", 30]],
    inputs: [["AluminumIngot", 120], ["ReanimatedSAM", 60]]
  },
  {
    name: "FicsiteIngot(Caterium)",
    category: "Ingots",
    machine: "Converter",
    outputs: [["FicsiteIngot", 15]],
    inputs: [["CateriumIngot", 60], ["ReanimatedSAM", 45]]
  },

  // --- MINERALS ---
  {
    name: "Concrete",
    category: "Minerals",
    machine: "Constructor",
    outputs: [["Concrete", 15]],
    inputs: [["Limestone", 45]]
  },
  {
    name: "FineConcrete",
    category: "Minerals",
    machine: "Assembler",
    outputs: [["Concrete", 25]],
    inputs: [["Silica", 7.5], ["Limestone", 30]]
  },
  {
    name: "RubberConcrete",
    category: "Minerals",
    machine: "Assembler",
    outputs: [["Concrete", 45]],
    inputs: [["Limestone", 50], ["Rubber", 10]]
  },
  {
    name: "WetConcrete",
    category: "Minerals",
    machine: "Refinery",
    outputs: [["Concrete", 80]],
    inputs: [["Limestone", 120], ["Water", 100]]
  },
  {
    name: "QuartzCrystal",
    category: "Minerals",
    machine: "Constructor",
    outputs: [["QuartzCrystal", 22.5]],
    inputs: [["Quartz", 37.5]]
  },
  {
    name: "PureQuartzCrystal",
    category: "Minerals",
    machine: "Refinery",
    outputs: [["QuartzCrystal", 52.5]],
    inputs: [["Quartz", 67.5], ["Water", 37.5]]
  },
  {
    name: "QuartzPurification",
    category: "Minerals",
    machine: "Refinery",
    outputs: [["QuartzCrystal", 75], ["DissolvedSilica", 60]],
    inputs: [["Quartz", 120], ["NitricAcid", 10]]
  },
  {
    name: "FusedQuartzCrystal",
    category: "Minerals",
    machine: "Foundry",
    outputs: [["QuartzCrystal", 54]],
    inputs: [["Quartz", 75], ["Coal", 36]]
  },
  {
    name: "Silica",
    category: "Minerals",
    machine: "Constructor",
    outputs: [["Silica", 37.5]],
    inputs: [["Quartz", 22.5]]
  },
  {
    name: "CheapSilica",
    category: "Minerals",
    machine: "Assembler",
    outputs: [["Silica", 26.25]],
    inputs: [["Quartz", 11.25], ["Limestone", 18.75]]
  },
  {
    name: "DistilledSilica",
    category: "Minerals",
    machine: "Blender",
    outputs: [["Silica", 270], ["Water", 80]],
    inputs: [["DissolvedSilica", 120], ["Limestone", 50], ["Water", 100]]
  },
  {
    name: "Diamond",
    category: "Minerals",
    machine: "Collider",
    outputs: [["Diamond", 30]],
    inputs: [["Coal", 600]]
  },
  {
    name: "PetroleumDiamond",
    category: "Minerals",
    machine: "Collider",
    outputs: [["Diamond", 30]],
    inputs: [["PetroleumCoke", 720]]
  },
  {
    name: "OilBasedDiamond",
    category: "Minerals",
    machine: "Collider",
    outputs: [["Diamond", 40]],
    inputs: [["CrudeOil", 200]]
  },
  {
    name: "CloudyDiamond",
    category: "Minerals",
    machine: "Collider",
    outputs: [["Diamond", 20]],
    inputs: [["Coal", 240], ["Limestone", 480]]
  },
  {
    name: "TurboDiamond",
    category: "Minerals",
    machine: "Collider",
    outputs: [["Diamond", 60]],
    inputs: [["Coal", 600], ["PackagedTurboFuel", 40]]
  },
  {
    name: "PinkDiamond",
    category: "Minerals",
    machine: "Converter",
    outputs: [["Diamond", 15]],
    inputs: [["Coal", 120], ["QuartzCrystal", 45]]
  },
  {
    name: "CopperPowder",
    category: "Minerals",
    machine: "Constructor",
    outputs: [["CopperPowder", 50]],
    inputs: [["CopperIngot", 300]]
  },
  {
    name: "AluminumScrap",
    category: "Minerals",
    machine: "Refinery",
    outputs: [["AluminumScrap", 360], ["Water", 120]],
    inputs: [["AluminaSolution", 240], ["Coal", 120]]
  },
  {
    name: "ElectrodeAluminumScrap",
    category: "Minerals",
    machine: "Refinery",
    outputs: [["AluminumScrap", 300], ["Water", 105]],
    inputs: [["AluminaSolution", 180], ["PetroleumCoke", 60]]
  },
  {
    name: "InstantScrap",
    category: "Minerals",
    machine: "Blender",
    outputs: [["AluminumScrap", 300], ["Water", 50]],
    inputs: [["Bauxite", 150], ["Coal", 100], ["SulfuricAcid", 50], ["Water", 60]]
  },
  {
    name: "PetroleumCoke",
    category: "Minerals",
    machine: "Refinery",
    outputs: [["PetroleumCoke", 120]],
    inputs: [["HeavyOil", 40]]
  },
  {
    name: "PolymerResin",
    category: "Minerals",
    machine: "Refinery",
    outputs: [["PolymerResin", 130], ["HeavyOil", 20]],
    inputs: [["CrudeOil", 60]]
  },

  // --- STANDARD PARTS ---
  {
    name: "IronPlate",
    category: "StandartParts",
    machine: "Constructor",
    outputs: [["IronPlate", 20]],
    inputs: [["IronIngot", 30]]
  },
  {
    name: "CoatedIronPlate",
    category: "StandartParts",
    machine: "Assembler",
    outputs: [["IronPlate", 75]],
    inputs: [["IronIngot", 50], ["Plastic", 10]]
  },
  {
    name: "SteelCastPlate",
    category: "StandartParts",
    machine: "Foundry",
    outputs: [["IronPlate", 45]],
    inputs: [["IronIngot", 15], ["SteelIngot", 15]]
  },
  {
    name: "IronRod",
    category: "StandartParts",
    machine: "Constructor",
    outputs: [["IronRod", 15]],
    inputs: [["IronIngot", 15]]
  },
  {
    name: "SteelRod",
    category: "StandartParts",
    machine: "Constructor",
    outputs: [["IronRod", 48]],
    inputs: [["SteelIngot", 12]]
  },
  {
    name: "AluminumRod",
    category: "StandartParts",
    machine: "Constructor",
    outputs: [["IronRod", 52.5]],
    inputs: [["AluminumIngot", 7.5]]
  },
  {
    name: "Screw",
    category: "StandartParts",
    machine: "Constructor",
    outputs: [["Screw", 40]],
    inputs: [["IronRod", 10]]
  },
  {
    name: "CastScrew",
    category: "StandartParts",
    machine: "Constructor",
    outputs: [["Screw", 50]],
    inputs: [["IronIngot", 12.5]]
  },
  {
    name: "SteelScrew",
    category: "StandartParts",
    machine: "Constructor",
    outputs: [["Screw", 260]],
    inputs: [["SteelBeam", 5]]
  },
  {
    name: "CopperSheet",
    category: "StandartParts",
    machine: "Constructor",
    outputs: [["CopperSheet", 10]],
    inputs: [["CopperIngot", 20]]
  },
  {
    name: "SteamedCopperSheet",
    category: "StandartParts",
    machine: "Refinery",
    outputs: [["CopperSheet", 22.5]],
    inputs: [["CopperIngot", 22.5], ["Water", 22.5]]
  },
  {
    name: "SteelBeam",
    category: "StandartParts",
    machine: "Constructor",
    outputs: [["SteelBeam", 15]],
    inputs: [["SteelIngot", 60]]
  },
  {
    name: "AluminumBeam",
    category: "StandartParts",
    machine: "Constructor",
    outputs: [["SteelBeam", 22.5]],
    inputs: [["AluminumIngot", 22.5]]
  },
  {
    name: "MoldedBeam",
    category: "StandartParts",
    machine: "Foundry",
    outputs: [["SteelBeam", 45]],
    inputs: [["SteelIngot", 120], ["Concrete", 80]]
  },
  {
    name: "SteelPipe",
    category: "StandartParts",
    machine: "Constructor",
    outputs: [["SteelPipe", 20]],
    inputs: [["SteelIngot", 30]]
  },
  {
    name: "IronPipe",
    category: "StandartParts",
    machine: "Constructor",
    outputs: [["SteelPipe", 25]],
    inputs: [["IronIngot", 100]]
  },
  {
    name: "MoldedSteelPipe",
    category: "StandartParts",
    machine: "Foundry",
    outputs: [["SteelPipe", 50]],
    inputs: [["SteelIngot", 50], ["Concrete", 30]]
  },
  {
    name: "AluminumCasing",
    category: "StandartParts",
    machine: "Constructor",
    outputs: [["AluminumCasing", 60]],
    inputs: [["AluminumIngot", 90]]
  },
  {
    name: "AlcladCasing",
    category: "StandartParts",
    machine: "Assembler",
    outputs: [["AluminumCasing", 112.5]],
    inputs: [["AluminumIngot", 150], ["CopperIngot", 75]]
  },
  {
    name: "ReinforcedIronPlate",
    category: "StandartParts",
    machine: "Assembler",
    outputs: [["ReinforcedIronPlate", 5]],
    inputs: [["IronPlate", 30], ["Screw", 60]]
  },
  {
    name: "AdheredIronPlate",
    category: "StandartParts",
    machine: "Assembler",
    outputs: [["ReinforcedIronPlate", 3.75]],
    inputs: [["IronPlate", 11.25], ["Rubber", 3.75]]
  },
  {
    name: "BoltedIronPlate",
    category: "StandartParts",
    machine: "Assembler",
    outputs: [["ReinforcedIronPlate", 15]],
    inputs: [["IronPlate", 90], ["Screw", 250]]
  },
  {
    name: "StitchedIronPlate",
    category: "StandartParts",
    machine: "Assembler",
    outputs: [["ReinforcedIronPlate", 5.625]],
    inputs: [["IronPlate", 18.75], ["Wire", 37.5]]
  },
  {
    name: "ModularFrame",
    category: "StandartParts",
    machine: "Assembler",
    outputs: [["ModularFrame", 2]],
    inputs: [["ReinforcedIronPlate", 3], ["IronRod", 12]]
  },
  {
    name: "BoltedFrame",
    category: "StandartParts",
    machine: "Assembler",
    outputs: [["ModularFrame", 5]],
    inputs: [["ReinforcedIronPlate", 7.5], ["Screw", 140]]
  },
  {
    name: "SteeledFrame",
    category: "StandartParts",
    machine: "Assembler",
    outputs: [["ModularFrame", 3]],
    inputs: [["ReinforcedIronPlate", 2], ["SteelPipe", 10]]
  },
  {
    name: "EncasedIndustrialBeam",
    category: "StandartParts",
    machine: "Assembler",
    outputs: [["EncasedIndustrialBeam", 6]],
    inputs: [["SteelBeam", 18], ["Concrete", 36]]
  },
  {
    name: "EncasedIndustrialPipe",
    category: "StandartParts",
    machine: "Assembler",
    outputs: [["EncasedIndustrialBeam", 4]],
    inputs: [["SteelPipe", 24], ["Concrete", 20]]
  },
  {
    name: "AlcladAluminumSheet",
    category: "StandartParts",
    machine: "Assembler",
    outputs: [["AlcladAluminumSheet", 30]],
    inputs: [["AluminumIngot", 30], ["CopperIngot", 10]]
  },
  {
    name: "Fabric",
    category: "StandartParts",
    machine: "Assembler",
    outputs: [["Fabric", 15]],
    inputs: [["Mycelia", 15], ["Biomass", 75]]
  },
  {
    name: "PolyesterFabric",
    category: "StandartParts",
    machine: "Refinery",
    outputs: [["Fabric", 30]],
    inputs: [["PolymerResin", 30], ["Water", 30]]
  },
  {
    name: "HeavyModularFrame",
    category: "StandartParts",
    machine: "Manufacturer",
    outputs: [["HeavyModularFrame", 2]],
    inputs: [["ModularFrame", 10], ["SteelPipe", 30], ["EncasedIndustrialBeam", 10], ["Screw", 200]]
  },
  {
    name: "HeavyEncasedFrame",
    category: "StandartParts",
    machine: "Manufacturer",
    outputs: [["HeavyModularFrame", 2.8125]],
    inputs: [["ModularFrame", 7.5], ["EncasedIndustrialBeam", 9.375], ["SteelPipe", 33.75], ["Concrete", 20.625]]
  },
  {
    name: "HeavyFlexibleFrame",
    category: "StandartParts",
    machine: "Manufacturer",
    outputs: [["HeavyModularFrame", 3.75]],
    inputs: [["ModularFrame", 18.75], ["EncasedIndustrialBeam", 11.25], ["Rubber", 75], ["Screw", 390]]
  },
  {
    name: "FusedModularFrame",
    category: "StandartParts",
    machine: "Blender",
    outputs: [["FusedModularFrame", 1.5]],
    inputs: [["HeavyModularFrame", 1.5], ["AluminumCasing", 75], ["Nitrogen", 37.5]]
  },
  {
    name: "HeatFusedFrame",
    category: "StandartParts",
    machine: "Blender",
    outputs: [["FusedModularFrame", 3]],
    inputs: [["HeavyModularFrame", 3], ["AluminumIngot", 150], ["NitricAcid", 24], ["Fuel", 30]]
  },
  {
    name: "PressureConversionCube",
    category: "StandartParts",
    machine: "Assembler",
    outputs: [["PressureConversionCube", 1]],
    inputs: [["FusedModularFrame", 1], ["RadioControlUnit", 2]]
  },
  {
    name: "FicsiteTrigon",
    category: "StandartParts",
    machine: "Constructor",
    outputs: [["FicsiteTrigon", 30]],
    inputs: [["FicsiteIngot", 10]]
  },
  {
    name: "Plastic",
    category: "StandartParts",
    machine: "Refinery",
    outputs: [["Plastic", 20], ["HeavyOil", 10]],
    inputs: [["CrudeOil", 30]]
  },
  {
    name: "RecycledPlastic",
    category: "StandartParts",
    machine: "Refinery",
    outputs: [["Plastic", 60]],
    inputs: [["Rubber", 30], ["Fuel", 30]]
  },
  {
    name: "ResidualPlastic",
    category: "StandartParts",
    machine: "Refinery",
    outputs: [["Plastic", 20]],
    inputs: [["PolymerResin", 60], ["Water", 20]]
  },
  {
    name: "Rubber",
    category: "StandartParts",
    machine: "Refinery",
    outputs: [["Rubber", 20], ["HeavyOil", 20]],
    inputs: [["CrudeOil", 30]]
  },
  {
    name: "RecycledRubber",
    category: "StandartParts",
    machine: "Refinery",
    outputs: [["Rubber", 60]],
    inputs: [["Plastic", 30], ["Fuel", 30]]
  },
  {
    name: "ResidualRubber",
    category: "StandartParts",
    machine: "Refinery",
    outputs: [["Rubber", 20]],
    inputs: [["PolymerResin", 40], ["Water", 40]]
  },

  // --- INDUSTRIAL PARTS ---
  {
    name: "Rotor",
    category: "IndustrialParts",
    machine: "Assembler",
    outputs: [["Rotor", 4]],
    inputs: [["IronRod", 20], ["Screw", 100]]
  },
  {
    name: "SteelRotor",
    category: "IndustrialParts",
    machine: "Assembler",
    outputs: [["Rotor", 5]],
    inputs: [["SteelPipe", 10], ["Wire", 30]]
  },
  {
    name: "CopperRotor",
    category: "IndustrialParts",
    machine: "Assembler",
    outputs: [["Rotor", 11.25]],
    inputs: [["CopperSheet", 22.5], ["Screw", 195]]
  },
  {
    name: "Stator",
    category: "IndustrialParts",
    machine: "Assembler",
    outputs: [["Stator", 5]],
    inputs: [["SteelPipe", 15], ["Wire", 40]]
  },
  {
    name: "QuickwireStator",
    category: "IndustrialParts",
    machine: "Assembler",
    outputs: [["Stator", 8]],
    inputs: [["SteelPipe", 16], ["Quickwire", 60]]
  },
  {
    name: "Motor",
    category: "IndustrialParts",
    machine: "Assembler",
    outputs: [["Motor", 5]],
    inputs: [["Rotor", 10], ["Stator", 10]]
  },
  {
    name: "ElectricMotor",
    category: "IndustrialParts",
    machine: "Assembler",
    outputs: [["Motor", 7.5]],
    inputs: [["Rotor", 7.5], ["ElectromagneticControlRod", 3.75]]
  },
  {
    name: "RigorMotor",
    category: "IndustrialParts",
    machine: "Manufacturer",
    outputs: [["Motor", 7.5]],
    inputs: [["Rotor", 3.75], ["Stator", 3.75], ["CrystalOscillator", 1.25]]
  },
  {
    name: "HeatSink",
    category: "IndustrialParts",
    machine: "Assembler",
    outputs: [["HeatSink", 7.5]],
    inputs: [["AlcladAluminumSheet", 37.5], ["CopperSheet", 22.5]]
  },
  {
    name: "HeatExchanger",
    category: "IndustrialParts",
    machine: "Assembler",
    outputs: [["HeatSink", 10]],
    inputs: [["AluminumCasing", 30], ["Rubber", 30]]
  },
  {
    name: "TurboMotor",
    category: "IndustrialParts",
    machine: "Manufacturer",
    outputs: [["TurboMotor", 1.875]],
    inputs: [["Motor", 7.5], ["CoolingSystem", 7.5], ["RadioControlUnit", 3.75], ["Rubber", 45]]
  },
  {
    name: "TurboElectricMotor",
    category: "IndustrialParts",
    machine: "Manufacturer",
    outputs: [["TurboMotor", 2.8125]],
    inputs: [["Motor", 6.5625], ["RadioControlUnit", 8.4375], ["ElectromagneticControlRod", 4.6875], ["Rotor", 6.5625]]
  },
  {
    name: "TurboPressureMotor",
    category: "IndustrialParts",
    machine: "Manufacturer",
    outputs: [["TurboMotor", 3.75]],
    inputs: [["Motor", 7.5], ["PressureConversionCube", 1.875], ["PackagedNitrogen", 45], ["Stator", 15]]
  },
  {
    name: "CoolingSystem",
    category: "IndustrialParts",
    machine: "Blender",
    outputs: [["CoolingSystem", 6]],
    inputs: [["HeatSink", 12], ["Rubber", 12], ["Water", 30], ["Nitrogen", 150]]
  },
  {
    name: "CoolingDevice",
    category: "IndustrialParts",
    machine: "Blender",
    outputs: [["CoolingSystem", 5]],
    inputs: [["HeatSink", 10], ["Motor", 2.5], ["Nitrogen", 60]]
  },
  {
    name: "Battery",
    category: "IndustrialParts",
    machine: "Blender",
    outputs: [["Battery", 20], ["Water", 30]],
    inputs: [["SulfuricAcid", 50], ["AluminaSolution", 40], ["AluminumCasing", 20]]
  },
  {
    name: "ClassicBattery",
    category: "IndustrialParts",
    machine: "Manufacturer",
    outputs: [["Battery", 30]],
    inputs: [["Sulfur", 45], ["AlcladAluminumSheet", 52.5], ["Plastic", 60], ["Wire", 90]]
  },

  // --- ELECTRONICS ---
  {
    name: "Wire",
    category: "Electronics",
    machine: "Constructor",
    outputs: [["Wire", 30]],
    inputs: [["CopperIngot", 15]]
  },
  {
    name: "IronWire",
    category: "Electronics",
    machine: "Constructor",
    outputs: [["Wire", 22.5]],
    inputs: [["IronIngot", 12.5]]
  },
  {
    name: "CateriumWire",
    category: "Electronics",
    machine: "Constructor",
    outputs: [["Wire", 120]],
    inputs: [["CateriumIngot", 15]]
  },
  {
    name: "FusedWire",
    category: "Electronics",
    machine: "Assembler",
    outputs: [["Wire", 90]],
    inputs: [["CopperIngot", 12], ["CateriumIngot", 3]]
  },
  {
    name: "Quickwire",
    category: "Electronics",
    machine: "Constructor",
    outputs: [["Quickwire", 60]],
    inputs: [["CateriumIngot", 12]]
  },
  {
    name: "FusedQuickwire",
    category: "Electronics",
    machine: "Assembler",
    outputs: [["Quickwire", 90]],
    inputs: [["CateriumIngot", 7.5], ["CopperIngot", 37.5]]
  },
  {
    name: "Cable",
    category: "Electronics",
    machine: "Constructor",
    outputs: [["Cable", 30]],
    inputs: [["Wire", 60]]
  },
  {
    name: "InsulatedCable",
    category: "Electronics",
    machine: "Assembler",
    outputs: [["Cable", 100]],
    inputs: [["Wire", 45], ["Rubber", 30]]
  },
  {
    name: "QuickwireCable",
    category: "Electronics",
    machine: "Assembler",
    outputs: [["Cable", 27.5]],
    inputs: [["Quickwire", 7.5], ["Rubber", 5]]
  },
  {
    name: "CoatedCable",
    category: "Electronics",
    machine: "Refinery",
    outputs: [["Cable", 67.5]],
    inputs: [["Wire", 37.5], ["HeavyOil", 15]]
  },
  {
    name: "CircuitBoard",
    category: "Electronics",
    machine: "Assembler",
    outputs: [["CircuitBoard", 7.5]],
    inputs: [["CopperSheet", 15], ["Plastic", 30]]
  },
  {
    name: "CateriumCircuitBoard",
    category: "Electronics",
    machine: "Assembler",
    outputs: [["CircuitBoard", 8.75]],
    inputs: [["Plastic", 12.5], ["Quickwire", 37.5]]
  },
  {
    name: "SiliconCircuitBoard",
    category: "Electronics",
    machine: "Assembler",
    outputs: [["CircuitBoard", 12.5]],
    inputs: [["CopperSheet", 27.5], ["Silica", 27.5]]
  },
  {
    name: "ElectrodeCircuitBoard",
    category: "Electronics",
    machine: "Assembler",
    outputs: [["CircuitBoard", 5]],
    inputs: [["Rubber", 20], ["PetroleumCoke", 40]]
  },
  {
    name: "AILimiter",
    category: "Electronics",
    machine: "Assembler",
    outputs: [["AILimiter", 5]],
    inputs: [["CopperSheet", 25], ["Quickwire", 100]]
  },
  {
    name: "PlasticAILimiter",
    category: "Electronics",
    machine: "Assembler",
    outputs: [["AILimiter", 8]],
    inputs: [["Plastic", 28], ["Quickwire", 120]]
  },
  {
    name: "HighSpeedConnector",
    category: "Electronics",
    machine: "Manufacturer",
    outputs: [["HighSpeedConnector", 3.75]],
    inputs: [["Quickwire", 210], ["Cable", 37.5], ["CircuitBoard", 3.75]]
  },
  {
    name: "SiliconHighSpeedConnector",
    category: "Electronics",
    machine: "Manufacturer",
    outputs: [["HighSpeedConnector", 3]],
    inputs: [["Quickwire", 90], ["Silica", 37.5], ["CircuitBoard", 3]]
  },
  {
    name: "ReanimatedSAM",
    category: "Electronics",
    machine: "Constructor",
    outputs: [["ReanimatedSAM", 30]],
    inputs: [["SAM", 120]]
  },
  {
    name: "SAMFluctuator",
    category: "Electronics",
    machine: "Manufacturer",
    outputs: [["SAMFluctuator", 10]],
    inputs: [["ReanimatedSAM", 60], ["Wire", 50], ["SteelPipe", 30]]
  },

  // --- COMMUNICATIONS ---
  {
    name: "Computer",
    category: "Communications",
    machine: "Manufacturer",
    outputs: [["Computer", 2.5]],
    inputs: [["CircuitBoard", 10], ["Cable", 20], ["Plastic", 40]]
  },
  {
    name: "CateriumComputer",
    category: "Communications",
    machine: "Manufacturer",
    outputs: [["Computer", 3.75]],
    inputs: [["CircuitBoard", 15], ["Quickwire", 52.5], ["Rubber", 22.5]]
  },
  {
    name: "CrystalComputer",
    category: "Communications",
    machine: "Assembler",
    outputs: [["Computer", 3.333333]],
    inputs: [["CircuitBoard", 5], ["CrystalOscillator", 1.666666]]
  },
  {
    name: "SuperComputer",
    category: "Communications",
    machine: "Manufacturer",
    outputs: [["SuperComputer", 1.875]],
    inputs: [["Computer", 7.5], ["AILimiter", 3.75], ["HighSpeedConnector", 5.625], ["Plastic", 52.5]]
  },
  {
    name: "SuperStateComputer",
    category: "Communications",
    machine: "Manufacturer",
    outputs: [["SuperComputer", 2.4]],
    inputs: [["Computer", 7.2], ["ElectromagneticControlRod", 2.4], ["Battery", 24], ["Wire", 60]]
  },
  {
    name: "OCSuperComputer",
    category: "Communications",
    machine: "Assembler",
    outputs: [["SuperComputer", 3]],
    inputs: [["RadioControlUnit", 6], ["CoolingSystem", 6]]
  },
  {
    name: "RadioControlUnit",
    category: "Communications",
    machine: "Manufacturer",
    outputs: [["RadioControlUnit", 2.5]],
    inputs: [["AluminumCasing", 40], ["CrystalOscillator", 1.25], ["Computer", 2.5]]
  },
  {
    name: "RadioConnectionUnit",
    category: "Communications",
    machine: "Manufacturer",
    outputs: [["RadioControlUnit", 3.75]],
    inputs: [["HeatSink", 15], ["HighSpeedConnector", 7.5], ["QuartzCrystal", 45]]
  },
  {
    name: "RadioControlSystem",
    category: "Communications",
    machine: "Manufacturer",
    outputs: [["RadioControlUnit", 4.5]],
    inputs: [["CrystalOscillator", 1.5], ["CircuitBoard", 15], ["AluminumCasing", 90], ["Rubber", 45]]
  },
  {
    name: "CrystalOscillator",
    category: "Communications",
    machine: "Manufacturer",
    outputs: [["CrystalOscillator", 1]],
    inputs: [["QuartzCrystal", 18], ["Cable", 14], ["ReinforcedIronPlate", 2.5]]
  },
  {
    name: "InsulatedCrystalOscillator",
    category: "Communications",
    machine: "Manufacturer",
    outputs: [["CrystalOscillator", 1.875]],
    inputs: [["QuartzCrystal", 18.75], ["Rubber", 13.125], ["AILimiter", 1.875]]
  },

  // --- QUANTUM TECH ---
  {
    name: "TimeCrystal",
    category: "QuantumTech",
    machine: "Converter",
    outputs: [["TimeCrystal", 6]],
    inputs: [["Diamond", 12]]
  },
  {
    name: "DarkMatterCrystal",
    category: "QuantumTech",
    machine: "Collider",
    outputs: [["DarkMatterCrystal", 30]],
    inputs: [["Diamond", 30], ["DarkMatter", 150]]
  },
  {
    name: "DarkMatterTrap",
    category: "QuantumTech",
    machine: "Collider",
    outputs: [["DarkMatterCrystal", 60]],
    inputs: [["TimeCrystal", 30], ["DarkMatter", 150]]
  },
  {
    name: "DarkMatterCrystallization",
    category: "QuantumTech",
    machine: "Collider",
    outputs: [["DarkMatterCrystal", 20]],
    inputs: [["DarkMatter", 200]]
  },
  {
    name: "SuperpositionOscillator",
    category: "QuantumTech",
    machine: "QuantumEncoder",
    outputs: [["SuperpositionOscillator", 5], ["DarkMatter", 125]],
    inputs: [["CrystalOscillator", 5], ["DarkMatterCrystal", 30], ["AlcladAluminumSheet", 45], ["ExcitedPhotonicMatter", 125]]
  },
  {
    name: "NeuralQuantumProcessor",
    category: "QuantumTech",
    machine: "QuantumEncoder",
    outputs: [["NeuralQuantumProcessor", 3], ["DarkMatter", 75]],
    inputs: [["TimeCrystal", 15], ["SuperComputer", 3], ["FicsiteTrigon", 45], ["ExcitedPhotonicMatter", 75]]
  },
  {
    name: "AlienPowerMatrix",
    category: "QuantumTech",
    machine: "QuantumEncoder",
    outputs: [["AlienPowerMatrix", 2.5], ["DarkMatter", 60]],
    inputs: [["SAMFluctuator", 12.5], ["SyntheticPowerShard", 7.5], ["SuperpositionOscillator", 7.5], ["ExcitedPhotonicMatter", 60]]
  },
  {
    name: "SingularityCell",
    category: "QuantumTech",
    machine: "Manufacturer",
    outputs: [["SingularityCell", 10]],
    inputs: [["NuclearPasta", 1], ["DarkMatterCrystal", 20], ["IronPlate", 100], ["Concrete", 200]]
  },
  {
    name: "SyntheticPowerShard",
    category: "QuantumTech",
    machine: "QuantumEncoder",
    outputs: [["SyntheticPowerShard", 5], ["DarkMatter", 60]],
    inputs: [["TimeCrystal", 10], ["DarkMatterCrystal", 10], ["QuartzCrystal", 60], ["ExcitedPhotonicMatter", 60]]
  },

  // --- SPACE ELEVATOR PARTS ---
  {
    name: "SmartPlating",
    category: "SpaceElevatorParts",
    machine: "Assembler",
    outputs: [["SmartPlating", 2]],
    inputs: [["ReinforcedIronPlate", 2], ["Rotor", 2]]
  },
  {
    name: "PlasticSmartPlating",
    category: "SpaceElevatorParts",
    machine: "Manufacturer",
    outputs: [["SmartPlating", 5]],
    inputs: [["ReinforcedIronPlate", 2.5], ["Rotor", 2.5], ["Plastic", 7.5]]
  },
  {
    name: "VersatileFramework",
    category: "SpaceElevatorParts",
    machine: "Assembler",
    outputs: [["VersatileFramework", 5]],
    inputs: [["ModularFrame", 2.5], ["SteelBeam", 30]]
  },
  {
    name: "FlexibleFramework",
    category: "SpaceElevatorParts",
    machine: "Manufacturer",
    outputs: [["VersatileFramework", 7.5]],
    inputs: [["ModularFrame", 3.75], ["SteelBeam", 22.5], ["Rubber", 30]]
  },
  {
    name: "AutomatedWiring",
    category: "SpaceElevatorParts",
    machine: "Assembler",
    outputs: [["AutomatedWiring", 2.5]],
    inputs: [["Stator", 2.5], ["Cable", 50]]
  },
  {
    name: "AutomatedSpeedWiring",
    category: "SpaceElevatorParts",
    machine: "Manufacturer",
    outputs: [["AutomatedWiring", 7.5]],
    inputs: [["Stator", 3.75], ["Wire", 75], ["HighSpeedConnector", 1.875]]
  },
  {
    name: "ModularEngine",
    category: "SpaceElevatorParts",
    machine: "Manufacturer",
    outputs: [["ModularEngine", 1]],
    inputs: [["Motor", 2], ["Rubber", 15], ["SmartPlating", 2]]
  },
  {
    name: "AdaptiveControlUnit",
    category: "SpaceElevatorParts",
    machine: "Manufacturer",
    outputs: [["AdaptiveControlUnit", 1]],
    inputs: [["AutomatedWiring", 5], ["CircuitBoard", 5], ["HeavyModularFrame", 1], ["Computer", 2]]
  },
  {
    name: "AssemblyDirectorSystem",
    category: "SpaceElevatorParts",
    machine: "Assembler",
    outputs: [["AssemblyDirectorSystem", 0.75]],
    inputs: [["AdaptiveControlUnit", 1.5], ["SuperComputer", 0.75]]
  },
  {
    name: "MagneticFieldGenerator",
    category: "SpaceElevatorParts",
    machine: "Assembler",
    outputs: [["MagneticFieldGenerator", 1]],
    inputs: [["VersatileFramework", 2.5], ["ElectromagneticControlRod", 1]]
  },
  {
    name: "ThermalPropulsionRocket",
    category: "SpaceElevatorParts",
    machine: "Manufacturer",
    outputs: [["ThermalPropulsionRocket", 1]],
    inputs: [["ModularEngine", 2.5], ["TurboMotor", 1], ["CoolingSystem", 3], ["FusedModularFrame", 1]]
  },
  {
    name: "NuclearPasta",
    category: "SpaceElevatorParts",
    machine: "Collider",
    outputs: [["NuclearPasta", 0.5]],
    inputs: [["CopperPowder", 100], ["PressureConversionCube", 0.5]]
  },
  {
    name: "BiochemicalSculptor",
    category: "SpaceElevatorParts",
    machine: "Blender",
    outputs: [["BiochemicalSculptor", 2]],
    inputs: [["AssemblyDirectorSystem", 0.5], ["FicsiteTrigon", 40], ["Water", 10]]
  },
  {
    name: "AIExpansionServer",
    category: "SpaceElevatorParts",
    machine: "QuantumEncoder",
    outputs: [["AIExpansionServer", 4], ["DarkMatter", 100]],
    inputs: [["MagneticFieldGenerator", 4], ["NeuralQuantumProcessor", 4], ["SuperpositionOscillator", 4], ["ExcitedPhotonicMatter", 100]]
  },
  {
    name: "BallisticWarpDrive",
    category: "SpaceElevatorParts",
    machine: "Manufacturer",
    outputs: [["BallisticWarpDrive", 1]],
    inputs: [["ThermalPropulsionRocket", 1], ["SingularityCell", 5], ["SuperpositionOscillator", 2], ["DarkMatterCrystal", 40]]
  },

  // --- CONVERTING ---
  {
    name: "Bauxite(Caterium)",
    category: "Converting",
    machine: "Converter",
    outputs: [["Bauxite", 120]],
    inputs: [["CateriumOre", 150], ["ReanimatedSAM", 10]]
  },
  {
    name: "Bauxite(Copper)",
    category: "Converting",
    machine: "Converter",
    outputs: [["Bauxite", 120]],
    inputs: [["CopperOre", 180], ["ReanimatedSAM", 10]]
  },
  {
    name: "CateriumOre(Copper)",
    category: "Converting",
    machine: "Converter",
    outputs: [["CateriumOre", 120]],
    inputs: [["CopperOre", 150], ["ReanimatedSAM", 10]]
  },
  {
    name: "CateriumOre(Quartz)",
    category: "Converting",
    machine: "Converter",
    outputs: [["CateriumOre", 120]],
    inputs: [["Quartz", 120], ["ReanimatedSAM", 10]]
  },
  {
    name: "Coal(Iron)",
    category: "Converting",
    machine: "Converter",
    outputs: [["Coal", 120]],
    inputs: [["IronOre", 180], ["ReanimatedSAM", 10]]
  },
  {
    name: "Coal(Limestone)",
    category: "Converting",
    machine: "Converter",
    outputs: [["Coal", 120]],
    inputs: [["Limestone", 360], ["ReanimatedSAM", 10]]
  },
  {
    name: "CopperOre(Quartz)",
    category: "Converting",
    machine: "Converter",
    outputs: [["CopperOre", 120]],
    inputs: [["Quartz", 100], ["ReanimatedSAM", 10]]
  },
  {
    name: "CopperOre(Sulfur)",
    category: "Converting",
    machine: "Converter",
    outputs: [["CopperOre", 120]],
    inputs: [["Sulfur", 120], ["ReanimatedSAM", 10]]
  },
  {
    name: "IronOre(Limestone)",
    category: "Converting",
    machine: "Converter",
    outputs: [["IronOre", 120]],
    inputs: [["Limestone", 240], ["ReanimatedSAM", 10]]
  },
  {
    name: "Limestone(Sulfur)",
    category: "Converting",
    machine: "Converter",
    outputs: [["Limestone", 120]],
    inputs: [["Sulfur", 20], ["ReanimatedSAM", 10]]
  },
  {
    name: "Nitrogen(Bauxite)",
    category: "Converting",
    machine: "Converter",
    outputs: [["Nitrogen", 120]],
    inputs: [["Bauxite", 100], ["ReanimatedSAM", 10]]
  },
  {
    name: "Nitrogen(Caterium)",
    category: "Converting",
    machine: "Converter",
    outputs: [["Nitrogen", 120]],
    inputs: [["CateriumOre", 120], ["ReanimatedSAM", 10]]
  },
  {
    name: "Quartz(Bauxite)",
    category: "Converting",
    machine: "Converter",
    outputs: [["Quartz", 120]],
    inputs: [["Bauxite", 100], ["ReanimatedSAM", 10]]
  },
  {
    name: "Quartz(Coal)",
    category: "Converting",
    machine: "Converter",
    outputs: [["Quartz", 120]],
    inputs: [["Coal", 240], ["ReanimatedSAM", 10]]
  },
  {
    name: "Sulfur(Coal)",
    category: "Converting",
    machine: "Converter",
    outputs: [["Sulfur", 120]],
    inputs: [["Coal", 200], ["ReanimatedSAM", 10]]
  },
  {
    name: "Sulfur(Iron)",
    category: "Converting",
    machine: "Converter",
    outputs: [["Sulfur", 120]],
    inputs: [["IronOre", 300], ["ReanimatedSAM", 10]]
  },
  {
    name: "Uranium(Bauxite)",
    category: "Converting",
    machine: "Converter",
    outputs: [["Uranium", 120]],
    inputs: [["Bauxite", 480], ["ReanimatedSAM", 10]]
  },

  // --- SUPPLIES ---
  {
    name: "BlackPowder",
    category: "Supplies",
    machine: "Assembler",
    outputs: [["BlackPowder", 30]],
    inputs: [["Coal", 15], ["Sulfur", 15]]
  },
  {
    name: "FineBlackPowder",
    category: "Supplies",
    machine: "Assembler",
    outputs: [["BlackPowder", 15]],
    inputs: [["Sulfur", 7.5], ["CompactedCoal", 3.75]]
  },
  {
    name: "SmokelessPowder",
    category: "Supplies",
    machine: "Refinery",
    outputs: [["SmokelessPowder", 20]],
    inputs: [["BlackPowder", 20], ["HeavyOil", 10]]
  },
  {
    name: "GasFilter",
    category: "Supplies",
    machine: "Manufacturer",
    outputs: [["GasFilter", 7.5]],
    inputs: [["Coal", 37.5], ["Rubber", 15], ["Fabric", 15]]
  },
  {
    name: "IodineInfusedFilter",
    category: "Supplies",
    machine: "Manufacturer",
    outputs: [["GasFilter", 3.75]],
    inputs: [["GasFilter", 3.75], ["Quickwire", 30], ["AluminumCasing", 3.75]]
  },
  {
    name: "IronRebar",
    category: "Supplies",
    machine: "Constructor",
    outputs: [["IronRebar", 15]],
    inputs: [["IronRod", 15]]
  },
  {
    name: "StunRebar",
    category: "Supplies",
    machine: "Assembler",
    outputs: [["StunRebar", 10]],
    inputs: [["IronRebar", 10], ["Quickwire", 50]]
  },
  {
    name: "ShatterRebar",
    category: "Supplies",
    machine: "Assembler",
    outputs: [["ShatterRebar", 5]],
    inputs: [["IronRebar", 10], ["QuartzCrystal", 15]]
  },
  {
    name: "ExplosiveRebar",
    category: "Supplies",
    machine: "Manufacturer",
    outputs: [["ExplosiveRebar", 5]],
    inputs: [["IronRebar", 10], ["SmokelessPowder", 10], ["SteelPipe", 10]]
  },
  {
    name: "RifleAmmo",
    category: "Supplies",
    machine: "Assembler",
    outputs: [["RifleAmmo", 75]],
    inputs: [["CopperSheet", 15], ["SmokelessPowder", 10]]
  },
  {
    name: "HomingRifleAmmo",
    category: "Supplies",
    machine: "Assembler",
    outputs: [["HomingRifleAmmo", 25]],
    inputs: [["RifleAmmo", 50], ["HighSpeedConnector", 2.5]]
  },
  {
    name: "TurboRifleAmmoPacked",
    category: "Supplies",
    machine: "Manufacturer",
    outputs: [["TurboRifleAmmo", 250]],
    inputs: [["RifleAmmo", 125], ["AluminumCasing", 15], ["PackagedTurboFuel", 15]]
  },
  {
    name: "TurboRifleAmmo",
    category: "Supplies",
    machine: "Blender",
    outputs: [["TurboRifleAmmo", 250]],
    inputs: [["RifleAmmo", 125], ["AluminumCasing", 15], ["TurboFuel", 15]]
  },
  {
    name: "Nobelisk",
    category: "Supplies",
    machine: "Assembler",
    outputs: [["Nobelisk", 10]],
    inputs: [["BlackPowder", 20], ["SteelPipe", 20]]
  },
  {
    name: "GasNobelisk",
    category: "Supplies",
    machine: "Assembler",
    outputs: [["GasNobelisk", 5]],
    inputs: [["Nobelisk", 5], ["Biomass", 50]]
  },
  {
    name: "PulseNobelisk",
    category: "Supplies",
    machine: "Assembler",
    outputs: [["PulseNobelisk", 5]],
    inputs: [["Nobelisk", 5], ["CrystalOscillator", 1]]
  },
  {
    name: "ClusterNobelisk",
    category: "Supplies",
    machine: "Assembler",
    outputs: [["ClusterNobelisk", 2.5]],
    inputs: [["Nobelisk", 7.5], ["SmokelessPowder", 10]]
  },
  {
    name: "NukeNobelisk",
    category: "Supplies",
    machine: "Manufacturer",
    outputs: [["NukeNobelisk", 0.5]],
    inputs: [["Nobelisk", 2.5], ["EncasedUraniumCell", 10], ["SmokelessPowder", 5], ["AILimiter", 3]]
  },

  // --- LIQUIDS ---
  {
    name: "Fuel",
    category: "Liquids",
    machine: "Refinery",
    outputs: [["Fuel", 40], ["PolymerResin", 30]],
    inputs: [["CrudeOil", 60]]
  },
  {
    name: "ResidualFuel",
    category: "Liquids",
    machine: "Refinery",
    outputs: [["Fuel", 40]],
    inputs: [["HeavyOil", 60]]
  },
  {
    name: "DilutedFuel",
    category: "Liquids",
    machine: "Blender",
    outputs: [["Fuel", 100]],
    inputs: [["HeavyOil", 50], ["Water", 100]]
  },
  {
    name: "TurboFuel",
    category: "Liquids",
    machine: "Refinery",
    outputs: [["TurboFuel", 18.75]],
    inputs: [["Fuel", 22.5], ["CompactedCoal", 15]]
  },
  {
    name: "TurboHeavyFuel",
    category: "Liquids",
    machine: "Refinery",
    outputs: [["TurboFuel", 30]],
    inputs: [["HeavyOil", 37.5], ["CompactedCoal", 30]]
  },
  {
    name: "TurboBlendFuel",
    category: "Liquids",
    machine: "Blender",
    outputs: [["TurboFuel", 45]],
    inputs: [["Fuel", 15], ["HeavyOil", 30], ["Sulfur", 22.5], ["PetroleumCoke", 22.5]]
  },
  {
    name: "RocketFuel",
    category: "Liquids",
    machine: "Blender",
    outputs: [["RocketFuel", 100], ["CompactedCoal", 10]],
    inputs: [["TurboFuel", 60], ["NitricAcid", 10]]
  },
  {
    name: "NitroRocketFuel",
    category: "Liquids",
    machine: "Blender",
    outputs: [["RocketFuel", 150], ["CompactedCoal", 25]],
    inputs: [["Fuel", 100], ["Nitrogen", 75], ["Sulfur", 100], ["Coal", 50]]
  },
  {
    name: "IonizedFuel",
    category: "Liquids",
    machine: "Refinery",
    outputs: [["IonizedFuel", 40], ["CompactedCoal", 5]],
    inputs: [["RocketFuel", 40], ["SyntheticPowerShard", 2.5]]
  },
  {
    name: "DarkIonFuel",
    category: "Liquids",
    machine: "Converter",
    outputs: [["IonizedFuel", 200], ["CompactedCoal", 40]],
    inputs: [["PackagedRocketFuel", 240], ["DarkMatterCrystal", 80]]
  },
  {
    name: "LiquidBiofuel",
    category: "Liquids",
    machine: "Refinery",
    outputs: [["LiquidBiofuel", 60]],
    inputs: [["SolidBiofuel", 90], ["Water", 45]]
  },
  {
    name: "AluminaSolution",
    category: "Liquids",
    machine: "Refinery",
    outputs: [["AluminaSolution", 120], ["Silica", 50]],
    inputs: [["Bauxite", 120], ["Water", 180]]
  },
  {
    name: "SloppyAlumina",
    category: "Liquids",
    machine: "Refinery",
    outputs: [["AluminaSolution", 240]],
    inputs: [["Bauxite", 200], ["Water", 200]]
  },
  {
    name: "HeavyOil",
    category: "Liquids",
    machine: "Refinery",
    outputs: [["HeavyOil", 40], ["PolymerResin", 20]],
    inputs: [["CrudeOil", 30]]
  },
  {
    name: "SulfuricAcid",
    category: "Liquids",
    machine: "Refinery",
    outputs: [["SulfuricAcid", 50]],
    inputs: [["Sulfur", 50], ["Water", 50]]
  },
  {
    name: "NitricAcid",
    category: "Liquids",
    machine: "Blender",
    outputs: [["NitricAcid", 30]],
    inputs: [["Nitrogen", 120], ["Water", 30], ["IronPlate", 10]]
  },
  {
    name: "DarkMatter",
    category: "Liquids",
    machine: "Converter",
    outputs: [["DarkMatter", 100]],
    inputs: [["ReanimatedSAM", 50]]
  },
  {
    name: "ExcitedPhotonicMatter",
    category: "Liquids",
    machine: "Converter",
    outputs: [["ExcitedPhotonicMatter", 200]],
    inputs: []
  },

  // --- PACKAGES ---
  {
    name: "EmptyCanister",
    category: "Packages",
    machine: "Constructor",
    outputs: [["EmptyCanister", 60]],
    inputs: [["Plastic", 60]]
  },
  {
    name: "SteelCanister",
    category: "Packages",
    machine: "Constructor",
    outputs: [["EmptyCanister", 40]],
    inputs: [["SteelIngot", 60]]
  },
  {
    name: "CoatedIronCanister",
    category: "Packages",
    machine: "Assembler",
    outputs: [["EmptyCanister", 60]],
    inputs: [["IronPlate", 30], ["CopperSheet", 15]]
  },
  {
    name: "EmptyGasTank",
    category: "Packages",
    machine: "Constructor",
    outputs: [["EmptyGasTank", 60]],
    inputs: [["AluminumIngot", 60]]
  },
  {
    name: "PackagedWater",
    category: "Packages",
    machine: "Packager",
    outputs: [["PackagedWater", 60]],
    inputs: [["Water", 60], ["EmptyCanister", 60]]
  },
  {
    name: "PackagedCrudeOil",
    category: "Packages",
    machine: "Packager",
    outputs: [["PackagedCrudeOil", 30]],
    inputs: [["CrudeOil", 30], ["EmptyCanister", 30]]
  },
  {
    name: "PackagedHeavyOil",
    category: "Packages",
    machine: "Packager",
    outputs: [["PackagedHeavyOil", 30]],
    inputs: [["HeavyOil", 30], ["EmptyCanister", 30]]
  },
  {
    name: "PackagedFuel",
    category: "Packages",
    machine: "Packager",
    outputs: [["PackagedFuel", 40]],
    inputs: [["Fuel", 40], ["EmptyCanister", 40]]
  },
  {
    name: "DilutedPackagedFuel",
    category: "Packages",
    machine: "Refinery",
    outputs: [["PackagedFuel", 60]],
    inputs: [["HeavyOil", 30], ["PackagedWater", 60]]
  },
  {
    name: "PackagedLiquidBiofuel",
    category: "Packages",
    machine: "Packager",
    outputs: [["PackagedLiquidBiofuel", 40]],
    inputs: [["LiquidBiofuel", 40], ["EmptyCanister", 40]]
  },
  {
    name: "PackagedTurboFuel",
    category: "Packages",
    machine: "Packager",
    outputs: [["PackagedTurboFuel", 20]],
    inputs: [["TurboFuel", 20], ["EmptyCanister", 20]]
  },
  {
    name: "PackagedRocketFuel",
    category: "Packages",
    machine: "Packager",
    outputs: [["PackagedRocketFuel", 60]],
    inputs: [["RocketFuel", 120], ["EmptyGasTank", 60]]
  },
  {
    name: "PackagedIonizedFuel",
    category: "Packages",
    machine: "Packager",
    outputs: [["PackagedIonizedFuel", 40]],
    inputs: [["IonizedFuel", 80], ["EmptyGasTank", 40]]
  },
  {
    name: "PackagedAluminaSolution",
    category: "Packages",
    machine: "Packager",
    outputs: [["PackagedAluminaSolution", 120]],
    inputs: [["AluminaSolution", 120], ["EmptyCanister", 120]]
  },
  {
    name: "PackagedSulfuricAcid",
    category: "Packages",
    machine: "Packager",
    outputs: [["PackagedSulfuricAcid", 40]],
    inputs: [["SulfuricAcid", 40], ["EmptyCanister", 40]]
  },
  {
    name: "PackagedNitricAcid",
    category: "Packages",
    machine: "Packager",
    outputs: [["PackagedNitricAcid", 30]],
    inputs: [["NitricAcid", 30], ["EmptyGasTank", 30]]
  },
  {
    name: "PackagedNitrogen",
    category: "Packages",
    machine: "Packager",
    outputs: [["PackagedNitrogen", 60]],
    inputs: [["Nitrogen", 240], ["EmptyGasTank", 60]]
  },
  {
    name: "UnpackageWater",
    category: "Packages",
    machine: "Packager",
    outputs: [["Water", 120], ["EmptyCanister", 120]],
    inputs: [["PackagedWater", 120]]
  },
  {
    name: "UnpackageCrudeOil",
    category: "Packages",
    machine: "Packager",
    outputs: [["CrudeOil", 60], ["EmptyCanister", 60]],
    inputs: [["PackagedCrudeOil", 60]]
  },
  {
    name: "UnpackageHeavyOil",
    category: "Packages",
    machine: "Packager",
    outputs: [["HeavyOil", 20], ["EmptyCanister", 20]],
    inputs: [["PackagedHeavyOil", 20]]
  },
  {
    name: "UnpackageFuel",
    category: "Packages",
    machine: "Packager",
    outputs: [["Fuel", 60], ["EmptyCanister", 60]],
    inputs: [["PackagedFuel", 60]]
  },
  {
    name: "UnpackageLiquidBiofuel",
    category: "Packages",
    machine: "Packager",
    outputs: [["LiquidBiofuel", 60], ["EmptyCanister", 60]],
    inputs: [["PackagedLiquidBiofuel", 60]]
  },
  {
    name: "UnpackageTurboFuel",
    category: "Packages",
    machine: "Packager",
    outputs: [["TurboFuel", 20], ["EmptyCanister", 20]],
    inputs: [["PackagedTurboFuel", 20]]
  },
  {
    name: "UnpackageRocketFuel",
    category: "Packages",
    machine: "Packager",
    outputs: [["RocketFuel", 120], ["EmptyGasTank", 60]],
    inputs: [["PackagedRocketFuel", 60]]
  },
  {
    name: "UnpackageIonizedFuel",
    category: "Packages",
    machine: "Packager",
    outputs: [["IonizedFuel", 80], ["EmptyGasTank", 40]],
    inputs: [["PackagedIonizedFuel", 40]]
  },
  {
    name: "UnpackageAluminaSolution",
    category: "Packages",
    machine: "Packager",
    outputs: [["AluminaSolution", 120], ["EmptyCanister", 120]],
    inputs: [["PackagedAluminaSolution", 120]]
  },
  {
    name: "UnpackageSulfuricAcid",
    category: "Packages",
    machine: "Packager",
    outputs: [["SulfuricAcid", 60], ["EmptyCanister", 60]],
    inputs: [["PackagedSulfuricAcid", 60]]
  },
  {
    name: "UnpackageNitricAcid",
    category: "Packages",
    machine: "Packager",
    outputs: [["NitricAcid", 20], ["EmptyGasTank", 20]],
    inputs: [["PackagedNitricAcid", 20]]
  },
  {
    name: "UnpackageNitrogen",
    category: "Packages",
    machine: "Packager",
    outputs: [["Nitrogen", 240], ["EmptyGasTank", 60]],
    inputs: [["PackagedNitrogen", 60]]
  },

  // --- BURNABLE ---
  {
    name: "Biomass(Leaves)",
    category: "Burnable",
    machine: "Constructor",
    outputs: [["Biomass", 60]],
    inputs: [["Leaves", 120]]
  },
  {
    name: "Biomass(Wood)",
    category: "Burnable",
    machine: "Constructor",
    outputs: [["Biomass", 300]],
    inputs: [["Wood", 60]]
  },
  {
    name: "Biomass(Mycelia)",
    category: "Burnable",
    machine: "Constructor",
    outputs: [["Biomass", 150]],
    inputs: [["Mycelia", 15]]
  },
  {
    name: "CompactedCoal",
    category: "Burnable",
    machine: "Assembler",
    outputs: [["CompactedCoal", 25]],
    inputs: [["Coal", 25], ["Sulfur", 25]]
  },
  {
    name: "SolidBiofuel",
    category: "Burnable",
    machine: "Constructor",
    outputs: [["SolidBiofuel", 60]],
    inputs: [["Biomass", 120]]
  },

  // --- NUCLEAR ---
  {
    name: "ElectromagneticControlRod",
    category: "Nuclear",
    machine: "Assembler",
    outputs: [["ElectromagneticControlRod", 4]],
    inputs: [["Stator", 6], ["AILimiter", 4]]
  },
  {
    name: "ElectromagneticConnectionRod",
    category: "Nuclear",
    machine: "Assembler",
    outputs: [["ElectromagneticControlRod", 8]],
    inputs: [["Stator", 8], ["HighSpeedConnector", 4]]
  },
  {
    name: "UraniumFuelRod",
    category: "Nuclear",
    machine: "Manufacturer",
    outputs: [["UraniumFuelRod", 0.4]],
    inputs: [["EncasedUraniumCell", 20], ["EncasedIndustrialBeam", 1.2], ["ElectromagneticControlRod", 2]]
  },
  {
    name: "UraniumFuelUnit",
    category: "Nuclear",
    machine: "Manufacturer",
    outputs: [["UraniumFuelRod", 0.6]],
    inputs: [["EncasedUraniumCell", 100], ["ElectromagneticControlRod", 2], ["CrystalOscillator", 0.6], ["Rotor", 2]]
  },
  {
    name: "PlutoniumFuelRod",
    category: "Nuclear",
    machine: "Manufacturer",
    outputs: [["PlutoniumFuelRod", 0.25]],
    inputs: [["EncasedPlutoniumCell", 7.5], ["SteelBeam", 4.5], ["ElectromagneticControlRod", 1.5], ["HeatSink", 2.5]]
  },
  {
    name: "PlutoniumFuelUnit",
    category: "Nuclear",
    machine: "Assembler",
    outputs: [["PlutoniumFuelRod", 0.5]],
    inputs: [["EncasedPlutoniumCell", 10], ["PressureConversionCube", 0.5]]
  },
  {
    name: "EncasedUraniumCell",
    category: "Nuclear",
    machine: "Blender",
    outputs: [["EncasedUraniumCell", 25], ["SulfuricAcid", 10]],
    inputs: [["Uranium", 50], ["Concrete", 15], ["SulfuricAcid", 40]]
  },
  {
    name: "InfusedUraniumCell",
    category: "Nuclear",
    machine: "Manufacturer",
    outputs: [["EncasedUraniumCell", 20]],
    inputs: [["Uranium", 25], ["Silica", 15], ["Sulfur", 25], ["Quickwire", 75]]
  },
  {
    name: "EncasedPlutoniumCell",
    category: "Nuclear",
    machine: "Assembler",
    outputs: [["EncasedPlutoniumCell", 5]],
    inputs: [["PlutoniumPellet", 10], ["Concrete", 20]]
  },
  {
    name: "InstantPlutoniumCell",
    category: "Nuclear",
    machine: "Collider",
    outputs: [["EncasedPlutoniumCell", 10]],
    inputs: [["NonFissileUranium", 75], ["AluminumCasing", 10]]
  },
  {
    name: "NonFissileUranium",
    category: "Nuclear",
    machine: "Blender",
    outputs: [["NonFissileUranium", 50], ["Water", 15]],
    inputs: [["UraniumWaste", 37.5], ["Silica", 25], ["NitricAcid", 15], ["SulfuricAcid", 15]]
  },
  {
    name: "FertileUranium",
    category: "Nuclear",
    machine: "Blender",
    outputs: [["NonFissileUranium", 100], ["Water", 40]],
    inputs: [["Uranium", 25], ["UraniumWaste", 25], ["NitricAcid", 15], ["SulfuricAcid", 25]]
  },
  {
    name: "PlutoniumPellet",
    category: "Nuclear",
    machine: "Collider",
    outputs: [["PlutoniumPellet", 30]],
    inputs: [["NonFissileUranium", 100], ["UraniumWaste", 25]]
  },
  {
    name: "Ficsonium",
    category: "Nuclear",
    machine: "Collider",
    outputs: [["Ficsonium", 10]],
    inputs: [["PlutoniumWaste", 10], ["SingularityCell", 10], ["DarkMatter", 200]]
  },
  {
    name: "FicsoniumFuelRod",
    category: "Nuclear",
    machine: "QuantumEncoder",
    outputs: [["FicsoniumFuelRod", 2.5], ["DarkMatter", 50]],
    inputs: [["Ficsonium", 5], ["ElectromagneticControlRod", 5], ["FicsiteTrigon", 100], ["ExcitedPhotonicMatter", 50]]
  },

  // --- POWER GENERATING ---
  {
    name: "BiomassBurning",
    category: "PowerGenerating",
    machine: "BiomassBurner",
    outputs: [["Power", 30]],
    inputs: [["Biomass", 10]]
  },
  {
    name: "SolidBiofuelBurning",
    category: "PowerGenerating",
    machine: "BiomassBurner",
    outputs: [["Power", 30]],
    inputs: [["SolidBiofuel", 4]]
  },
  {
    name: "LiquidBiofuelBurning",
    category: "PowerGenerating",
    machine: "BiomassBurner",
    outputs: [["Power", 30]],
    inputs: [["PackagedLiquidBiofuel", 4]]
  },
  {
    name: "CoalBurning",
    category: "PowerGenerating",
    machine: "CoalGenerator",
    outputs: [["Power", 75]],
    inputs: [["Coal", 15], ["Water", 45]]
  },
  {
    name: "CompactedCoalBurning",
    category: "PowerGenerating",
    machine: "CoalGenerator",
    outputs: [["Power", 75]],
    inputs: [["CompactedCoal", 7.142857142857], ["Water", 45]]
  },
  {
    name: "PetroleumCokeBurning",
    category: "PowerGenerating",
    machine: "CoalGenerator",
    outputs: [["Power", 75]],
    inputs: [["PetroleumCoke", 25], ["Water", 45]]
  },
  {
    name: "FuelBurning",
    category: "PowerGenerating",
    machine: "FuelGenerator",
    outputs: [["Power", 250]],
    inputs: [["Fuel", 20]]
  },
  {
    name: "TurboFuelBurning",
    category: "PowerGenerating",
    machine: "FuelGenerator",
    outputs: [["Power", 250]],
    inputs: [["TurboFuel", 7.5]]
  },
  {
    name: "RocketFuelBurning",
    category: "PowerGenerating",
    machine: "FuelGenerator",
    outputs: [["Power", 250]],
    inputs: [["RocketFuel", 4.16666666]]
  },
  {
    name: "IonizedFuelBurning",
    category: "PowerGenerating",
    machine: "FuelGenerator",
    outputs: [["Power", 250]],
    inputs: [["IonizedFuel", 3]]
  },
  {
    name: "UraniumFuelRodFission",
    category: "PowerGenerating",
    machine: "NuclearPowerPlant",
    outputs: [["Power", 2500], ["UraniumWaste", 10]],
    inputs: [["UraniumFuelRod", 0.2], ["Water", 240]]
  },
  {
    name: "PlutoniumFuelRodFission",
    category: "PowerGenerating",
    machine: "NuclearPowerPlant",
    outputs: [["Power", 2500], ["PlutoniumWaste", 1]],
    inputs: [["PlutoniumFuelRod", 0.1], ["Water", 240]]
  },
  {
    name: "FicsoniumFuelRodFission",
    category: "PowerGenerating",
    machine: "NuclearPowerPlant",
    outputs: [["Power", 2500]],
    inputs: [["FicsoniumFuelRod", 1], ["Water", 240]]
  }
];