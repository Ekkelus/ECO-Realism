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
    public class Pumpkin : PlantEntity
    {
        public Pumpkin(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public Pumpkin() { }
        static PlantSpecies species;
        public class PumpkinSpecies : PlantSpecies
        {
            public PumpkinSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Pumpkin);

                // Info
                this.Decorative = false;
                this.Name = "Pumpkin";
                this.DisplayName = Localizer.DoStr("Pumpkin");
                // Lifetime
                this.MaturityAgeDays = 0.7f;
                // Generation
                this.Water = false;
                // Food
                this.CalorieValue = 1f;
                // Resources
                this.SeedDropChance = 0.66f;
                this.SeedsAtGrowth = 0.6f;
                this.SeedsBonusAtGrowth = 0.9f;
                this.SeedRange = new Range(0f, 1f);
                this.SeedItemType = typeof(PumpkinSeedItem);
                this.PostHarvestingGrowth = 0f;
                this.ScythingKills = true;
                this.PickableAtPercent = 0f;
                this.ResourceItemType = typeof(PumpkinItem);
                this.ResourceRange = new Range(1f, 2f);
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(PumpkinBlock);
                // Climate
                this.ReleasesCO2ppmPerDay = -5E-06f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "Phosphorus", HalfSpeedConcentration = 0.1f, MaxResourceContent = 0.2f });
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration = 0.1f, MaxResourceContent = 0.05f });
                this.CapacityConstraints.Add(new CapacityConstraint() { CapacityLayerName = "FertileGround", ConsumedCapacityPerPop = 1f });
                this.CapacityConstraints.Add(new CapacityConstraint() { CapacityLayerName = "ShrubSpace", ConsumedCapacityPerPop = 3f });
                this.IdealTemperatureRange = new Range(0.35f, 0.38f);
                this.IdealMoistureRange = new Range(0.32f, 0.35f);
                this.TemperatureExtremes = new Range(0.4f, 0.8f);
                this.MoistureExtremes = new Range(0.3f, 0.5f);
                this.MaxPollutionDensity = 0.7f;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 5;
            }
        }
    }

    [Serialized, MoveEfficiency(0.8f)]
    public class PumpkinBlock : InteractablePlantBlock { }
}