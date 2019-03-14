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
    public class PricklyPear : PlantEntity
    {
        public PricklyPear(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public PricklyPear() { }
        static PlantSpecies species;
        public class PricklyPearSpecies : PlantSpecies
        {
            public PricklyPearSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(PricklyPear);

                // Info
                this.Decorative = false;
                this.Name = "PricklyPear";
                this.DisplayName = Localizer.DoStr("Prickly Pear");
                // Lifetime
                this.MaturityAgeDays = 0.7f;
                // Generation
                this.Water = false;
                // Food
                this.CalorieValue = 4f;
                // Resources
                this.SeedDropChance = 0.2f;
                this.SeedsAtGrowth = 0.6f;
                this.SeedsBonusAtGrowth = 0.9f;
                this.SeedRange = new Range(1f, 2f);
                this.SeedItemType = typeof(PricklyPearSeedItem);
                this.PostHarvestingGrowth = 0.5f;
                this.ScythingKills = true;
                this.PickableAtPercent = 0.8f;
                this.ResourceItemType = typeof(PricklyPearFruitItem);
                this.ResourceRange = new Range(1f, 2f);
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(PricklyPearBlock);
                // Climate
                this.ReleasesCO2ppmPerDay = -1E-05f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Nitrogen", HalfSpeedConcentration = 0.3f, MaxResourceContent = 0.5f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Phosphorus", HalfSpeedConcentration = 0.1f, MaxResourceContent = 0.2f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Potassium", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.3f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.1f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "FertileGround" , ConsumedCapacityPerPop = 1f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "ShrubSpace" , ConsumedCapacityPerPop = 4.5f });
                this.GenerationSpawnCountPerPoint = new Range(2, 5);
                this.GenerationSpawnPointMultiplier = 0.02f;
                this.IdealTemperatureRange = new Range(0.72f, 0.85f);
                this.IdealMoistureRange = new Range(0.25f, 0.35f);
                this.TemperatureExtremes = new Range(0.7f, 1f);
                this.MoistureExtremes = new Range(0f, 0.35f);
                this.MaxPollutionDensity = 0.7f;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 5;
            }
        }
    }

    [Serialized, MoveEfficiency(0.625f)]
    public class PricklyPearBlock : InteractablePlantBlock { }
}