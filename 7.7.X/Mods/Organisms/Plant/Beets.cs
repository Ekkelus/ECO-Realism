namespace Eco.Mods.Organisms
{
    using Eco.Gameplay.Plants;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Simulation;
    using Eco.Simulation.Types;

    [Serialized]
    public class Beets : PlantEntity
    {
        public Beets(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public Beets() { }
        static PlantSpecies species;
        public class BeetsSpecies : BalancedPlantSpecies
        {
            public BeetsSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Beets);

                // Info
                this.Decorative = false;
                this.Name = "Beets";
                this.DisplayName = Localizer.DoStr("Beets");
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
                this.SeedRange = new Range(0f, 2f);
                this.SeedItemType = typeof(BeetSeedItem);
                this.PostHarvestingGrowth = 0f;
                this.ScythingKills = true;
                this.PickableAtPercent = 0f;
                this.ResourceItemType = typeof(BeetItem);
                this.ResourceRange = new Range(1f, 3f);
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(BeetsBlock);
                // Climate
                this.ReleasesCO2ppmPerDay = -1E-05f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Nitrogen", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.2f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Phosphorus", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.3f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Potassium", HalfSpeedConcentration = 0.3f, MaxResourceContent = 0.4f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.1f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "FertileGround" , ConsumedCapacityPerPop = 1f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "ShrubSpace" , ConsumedCapacityPerPop = 3f });
                this.IdealTemperatureRange = new Range(0.5f, 0.55f);
                this.IdealMoistureRange = new Range(0.32f, 0.35f);
                this.TemperatureExtremes = new Range(0.4f, 0.8f);
                this.MoistureExtremes = new Range(0.3f, 0.5f);
                this.MaxPollutionDensity = 0.7f;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 5;

            }
        }
    }

    [Serialized]
    public class BeetsBlock : InteractablePlantBlock { }
}