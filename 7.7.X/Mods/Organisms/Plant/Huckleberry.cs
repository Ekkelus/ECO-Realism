namespace Eco.Mods.Organisms
{
    using Eco.Gameplay.Plants;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Simulation;
    using Eco.Simulation.Types;
    using Eco.World.Blocks;

    [Serialized]
    public class Huckleberry : PlantEntity
    {
        public Huckleberry(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public Huckleberry() { }
        static PlantSpecies species;
        public class HuckleberrySpecies : BalancedPlantSpecies
        {
            public HuckleberrySpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Huckleberry);

                // Info
                this.Decorative = false;
                this.Name = "Huckleberry";
                this.DisplayName = Localizer.DoStr("Huckleberry");
                // Lifetime
                this.MaturityAgeDays = 0.8f;
                // Generation
                this.Water = false;
                // Food
                this.CalorieValue = 4f;
                // Resources
                this.SeedDropChance = 0.3f;
                this.SeedsAtGrowth = 0.6f;
                this.SeedsBonusAtGrowth = 0.9f;
                this.SeedRange = new Range(1f, 2f);
                this.SeedItemType = typeof(HuckleberrySeedItem);
                this.PostHarvestingGrowth = 0.5f;
                this.ScythingKills = true;
                this.PickableAtPercent = 0.8f;
                this.ResourceItemType = typeof(HuckleberriesItem);
                this.ResourceRange = new Range(1f, 4f);
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(HuckleberryBlock);
                // Climate
                this.ReleasesCO2ppmPerDay = -8E-06f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Nitrogen", HalfSpeedConcentration = 0.1f, MaxResourceContent = 0.1f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Phosphorus", HalfSpeedConcentration = 0.1f, MaxResourceContent = 0.15f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Potassium", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.2f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.05f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "FertileGround" , ConsumedCapacityPerPop = 1f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "ShrubSpace" , ConsumedCapacityPerPop = 2.5f });
                this.IdealTemperatureRange = new Range(0.42f, 0.55f);
                this.IdealMoistureRange = new Range(0.48f, 0.53f);
                this.TemperatureExtremes = new Range(0.4f, 0.8f);
                this.MoistureExtremes = new Range(0.45f, 0.65f);
                this.MaxPollutionDensity = 0.7f;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 5;
            }
        }
    }

    [Serialized, MoveEfficiency(0.5f)]
    public class HuckleberryBlock : InteractablePlantBlock { }
}