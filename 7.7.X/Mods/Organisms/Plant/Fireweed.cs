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
    public class Fireweed : PlantEntity
    {
        public Fireweed(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public Fireweed() { }
        static PlantSpecies species;
        public class FireweedSpecies : BalancedPlantSpecies
        {
            public FireweedSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Fireweed);

                // Info
                this.Decorative = false;
                this.Name = "Fireweed";
                this.DisplayName = Localizer.DoStr("Fireweed");
                // Lifetime
                this.MaturityAgeDays = 0.6f;
                // Generation
                this.Water = false;
                // Food
                this.CalorieValue = 1f;
                // Resources
                this.SeedDropChance = 0.75f;
                this.SeedsAtGrowth = 0.6f;
                this.SeedsBonusAtGrowth = 0.9f;
                this.SeedRange = new Range(1f, 2f);
                this.SeedItemType = typeof(FireweedSeedItem);
                this.PostHarvestingGrowth = 0f;
                this.ScythingKills = true;
                this.PickableAtPercent = 0f;
                this.ResourceItemType = typeof(FireweedShootsItem);
                this.ResourceRange = new Range(1f, 3f);
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(FireweedBlock);
                // Climate
                this.ReleasesCO2ppmPerDay = -1E-05f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Nitrogen", HalfSpeedConcentration = 0.1f, MaxResourceContent = 0.2f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Phosphorus", HalfSpeedConcentration = 0.15f, MaxResourceContent = 0.3f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Potassium", HalfSpeedConcentration = 0.1f, MaxResourceContent = 0.2f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration = 0.05f, MaxResourceContent = 0.1f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "FertileGround" , ConsumedCapacityPerPop = 1f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "ShrubSpace" , ConsumedCapacityPerPop = 3f });
                this.GenerationSpawnCountPerPoint = new Range(6, 8);
                this.GenerationSpawnSpread = new Range(1, 3);
                this.GenerationSpawnPointMultiplier = 0.2f;
                this.IdealTemperatureRange = new Range(0.24f, 0.28f);
                this.IdealMoistureRange = new Range(0.44f, 0.48f);
                this.TemperatureExtremes = new Range(0.2f, 0.3f);
                this.MoistureExtremes = new Range(0.2f, 0.5f);
                this.MaxPollutionDensity = 0.7f;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 5;
            }
        }
    }

    [Serialized, Reapable, MoveEfficiency(0.8f)]
    public class FireweedBlock : PlantBlock { }
}