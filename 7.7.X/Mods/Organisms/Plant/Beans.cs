namespace Eco.Mods.Organisms
{
    using Gameplay.Plants;
    using TechTree;
    using Shared.Localization;
    using Shared.Math;
    using Shared.Serialization;
    using Simulation;
    using Simulation.Types;

    [Serialized]
    public class Beans : PlantEntity
    {
        public Beans(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public Beans() { }
        static PlantSpecies species;
        public class BeansSpecies : BalancedPlantSpecies
        {
            public BeansSpecies() : base()
            {
                species = this;
                InstanceType = typeof(Beans);

                // Info
                Decorative = false;
                Name = "Beans";
                DisplayName = Localizer.DoStr("Beans");
                // Lifetime
                MaturityAgeDays = 0.8f;
                // Generation
                Water = false;
                // Food
                CalorieValue = 4f;
                // Resources
                SeedDropChance = 0.5f;
                SeedsAtGrowth = 0.6f;
                SeedsBonusAtGrowth = 0.9f;
                SeedRange = new Range(1f, 2f);
                SeedItemType = null;
                PostHarvestingGrowth = 0f;
                ScythingKills = true;
                PickableAtPercent = 0f;
                ResourceItemType = typeof(BeansItem);
                ResourceRange = new Range(1f, 3f);
                ResourceBonusAtGrowth = 0.9f;
                // Visuals
                BlockType = typeof(BeansBlock);
                // Climate
                ReleasesCO2ppmPerDay = -1E-05f;
                // WorldLayers
                MaxGrowthRate = 0.01f;
                MaxDeathRate = 0.005f;
                SpreadRate = 0.001f;
                ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Nitrogen", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.1f });
                ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Phosphorus", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.2f });
                ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Potassium", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.2f });
                ResourceConstraints.Add( new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration = 0.1f, MaxResourceContent = 0.1f });
                CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "FertileGround" , ConsumedCapacityPerPop = 1f });
                CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "ShrubSpace" , ConsumedCapacityPerPop = 3f });
                IdealTemperatureRange = new Range(0.65f, 0.75f);
                IdealMoistureRange = new Range(0.5f, 0.55f);
                TemperatureExtremes = new Range(0.4f, 0.8f);
                MoistureExtremes = new Range(0.45f, 0.65f);
                MaxPollutionDensity = 0.7f;
                PollutionDensityTolerance = 0.1f;
                VoxelsPerEntry = 5;

            }
        }
    }

    [Serialized]
    public class BeansBlock : InteractablePlantBlock { }
}