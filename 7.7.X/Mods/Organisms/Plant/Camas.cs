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
    public class Camas : PlantEntity
    {
        public Camas(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public Camas() { }
        static PlantSpecies species;
        public class CamasSpecies : BalancedPlantSpecies
        {
            public CamasSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Camas);

                // Info
                this.Decorative = false;
                this.Name = "Camas";
                this.DisplayName = Localizer.DoStr("Camas");
                // Lifetime
                this.MaturityAgeDays = 0.8f;
                // Generation
                this.Water = false;
                // Food
                this.CalorieValue = 5f;
                // Resources
                this.SeedDropChance = 0f;
                this.SeedsAtGrowth = 0.6f;
                this.SeedsBonusAtGrowth = 0.9f;
                this.SeedRange = new Range(0f, 0f);
                this.SeedItemType = null;
                this.PostHarvestingGrowth = 0f;
                this.ScythingKills = true;
                this.PickableAtPercent = 0f;
                this.ResourceItemType = typeof(CamasBulbItem);
                this.ResourceRange = new Range(1f, 4f);
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(CamasBlock);
                // Climate
                this.ReleasesCO2ppmPerDay = -1E-05f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Nitrogen", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.2f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Phosphorus", HalfSpeedConcentration = 0.1f, MaxResourceContent = 0.1f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Potassium", HalfSpeedConcentration = 0.4f, MaxResourceContent = 0.4f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration = 0.15f, MaxResourceContent = 0.05f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "FertileGround" , ConsumedCapacityPerPop = 1f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "ShrubSpace" , ConsumedCapacityPerPop = 3f });
                this.IdealTemperatureRange = new Range(0.6f, 0.7f);
                this.IdealMoistureRange = new Range(0.45f, 0.5f);
                this.TemperatureExtremes = new Range(0.4f, 0.8f);
                this.MoistureExtremes = new Range(0.45f, 0.65f);
                this.MaxPollutionDensity = 0.7f;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 5;

            }
        }
    }

    [Serialized, Diggable]
    public class CamasBlock : PlantBlock { }
}