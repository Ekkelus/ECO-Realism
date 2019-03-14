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
    public class Agave : PlantEntity
    {
        public Agave(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public Agave() { }
        static PlantSpecies species;
        public class AgaveSpecies : BalancedPlantSpecies
        {
            public AgaveSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Agave);

                // Info
                this.Decorative = false;
                this.Name = "Agave";
                this.DisplayName = Localizer.DoStr("Agave");
                // Lifetime
                this.MaturityAgeDays = 0.7f;
                // Generation
                this.Water = false;
                // Food
                this.CalorieValue = 4f;
                // Resources
                this.SeedDropChance = 0.66f;
                this.SeedsAtGrowth = 0.6f;
                this.SeedsBonusAtGrowth = 0.9f;
                this.SeedRange = new Range(0f, 1f);
                this.SeedItemType = typeof(AgaveSeedItem);
                this.PostHarvestingGrowth = 0f;
                this.ScythingKills = true;
                this.PickableAtPercent = 0f;
                this.ResourceItemType = typeof(AgaveLeavesItem);
                this.ResourceRange = new Range(2f, 4f);
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(AgaveBlock);
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
                this.GenerationSpawnCountPerPoint = new Range(5, 11);
                this.GenerationSpawnPointMultiplier = 0.02f;
                this.IdealTemperatureRange = new Range(0.75f, 0.85f);
                this.IdealMoistureRange = new Range(0.1f, 0.25f);
                this.TemperatureExtremes = new Range(0.7f, 1f);
                this.MoistureExtremes = new Range(0f, 0.35f);
                this.MaxPollutionDensity = 0.7f;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 5;

            }
        }
    }

    [Serialized, Reapable, MoveEfficiency(0.5f)]
    public class AgaveBlock : PlantBlock { }
}