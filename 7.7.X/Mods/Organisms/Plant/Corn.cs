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
    public class Corn : PlantEntity
    {
        public Corn(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public Corn() { }
        static PlantSpecies species;
        public class CornSpecies : BalancedPlantSpecies
        {
            public CornSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Corn);

                // Info
                this.Decorative = false;
                this.Name = "Corn";
                this.DisplayName = Localizer.DoStr("Corn");
                // Lifetime
                this.MaturityAgeDays = 0.8f;
                // Generation
                this.Water = false;
                // Food
                this.CalorieValue = 4f;
                // Resources
                this.SeedDropChance = 0.75f;
                this.SeedsAtGrowth = 0.6f;
                this.SeedsBonusAtGrowth = 0.9f;
                this.SeedRange = new Range(1f, 2f);
                this.SeedItemType = typeof(CornSeedItem);
                this.PickableAtPercent = 0.8f;
                this.ScythingKills = true;
                this.ResourceItemType = typeof(CornItem);
                this.ResourceRange = new Range(1f, 3f);
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(CornBlock);
                // Climate
                this.ReleasesCO2ppmPerDay = -1E-05f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Nitrogen", HalfSpeedConcentration = 0.4f, MaxResourceContent = 0.4f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Phosphorus", HalfSpeedConcentration = 0.4f, MaxResourceContent = 0.4f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Potassium", HalfSpeedConcentration = 0.1f, MaxResourceContent = 0.1f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration = 0.1f, MaxResourceContent = 0.1f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "FertileGround" , ConsumedCapacityPerPop = 1f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "ShrubSpace" , ConsumedCapacityPerPop = 3f });
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

    [Serialized, MoveEfficiency(0.66666f)]
    public class CornBlock : InteractablePlantBlock { }
}