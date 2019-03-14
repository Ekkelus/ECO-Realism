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
    public class Fern : PlantEntity
    {
        public Fern(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public Fern() { }
        static PlantSpecies species;
        public class FernSpecies : PlantSpecies
        {
            public FernSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Fern);

                // Info
                this.Decorative = false;
                this.Name = "Fern";
                this.DisplayName = Localizer.DoStr("Fern");
                // Lifetime
                this.MaturityAgeDays = 0.6f;
                // Generation
                this.Water = false;
                // Food
                this.CalorieValue = 2f;
                // Resources
                this.SeedDropChance = 0.75f;
                this.SeedsAtGrowth = 0.6f;
                this.SeedsBonusAtGrowth = 0.9f;
                this.SeedRange = new Range(1f, 2f);
                this.SeedItemType = typeof(FernSporeItem);
                this.PostHarvestingGrowth = 0f;
                this.ScythingKills = true;
                this.PickableAtPercent = 0f;
                this.ResourceItemType = typeof(FiddleheadsItem);
                this.ResourceRange = new Range(1f, 4f);
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(FernBlock);
                // Climate
                this.ReleasesCO2ppmPerDay = -5E-06f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Nitrogen", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.1f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Phosphorus", HalfSpeedConcentration = 0.1f, MaxResourceContent = 0.02f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Potassium", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.04f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration = 0.3f, MaxResourceContent = 0.2f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "FertileGround" , ConsumedCapacityPerPop = 1f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "ShrubSpace" , ConsumedCapacityPerPop = 2.5f });
                this.GenerationSpawnPointMultiplier = 0.2f;
                this.GenerationSpawnCountPerPoint = new Range(7, 13);
                this.IdealTemperatureRange = new Range(0.45f, 0.55f);
                this.IdealMoistureRange = new Range(0.5f, 0.55f);
                this.TemperatureExtremes = new Range(0.4f, 0.8f);
                this.MoistureExtremes = new Range(0.45f, 0.65f);
                this.MaxPollutionDensity = 0.7f;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 5;
            }
        }
    }

    [Serialized, Reapable, MoveEfficiency(0.8f)]
    public class FernBlock : InteractablePlantBlock { }
}