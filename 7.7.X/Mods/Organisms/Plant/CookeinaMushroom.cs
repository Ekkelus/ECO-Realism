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
    public class CookeinaMushroom : PlantEntity
    {
        public CookeinaMushroom(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public CookeinaMushroom() { }
        static PlantSpecies species;
        public class CookeinaMushroomSpecies : BalancedPlantSpecies
        {
            public CookeinaMushroomSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(CookeinaMushroom);

                // Info
                this.Decorative = false;
                this.Name = "CookeinaMushroom";
                this.DisplayName = Localizer.DoStr("Cookeina Mushroom");
                // Lifetime
                this.MaturityAgeDays = 0.8f;
                // Generation
                this.Water = false;
                // Food
                this.CalorieValue = 4f;
                // Resources
                this.SeedDropChance = 0.5f;
                this.SeedsAtGrowth = 0.6f;
                this.SeedsBonusAtGrowth = 0.9f;
                this.SeedRange = new Range(1f, 2f);
                this.SeedItemType = typeof(CookeinaMushroomSporesItem);
                this.PostHarvestingGrowth = 0f;
                this.ScythingKills = true;
                this.PickableAtPercent = 0f;
                this.ResourceItemType = typeof(CookeinaMushroomsItem);
                this.ResourceRange = new Range(1f, 3f);
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(CookeinaMushroomBlock);
                // Climate
                this.ReleasesCO2ppmPerDay = -1E-05f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "Nitrogen", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.1f });
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "Phosphorus", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.2f });
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "Potassium", HalfSpeedConcentration = 0.2f, MaxResourceContent = 0.2f });
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration = 0.1f, MaxResourceContent = 0.1f });
                this.CapacityConstraints.Add(new CapacityConstraint() { CapacityLayerName = "FertileGround", ConsumedCapacityPerPop = 1f });
                this.CapacityConstraints.Add(new CapacityConstraint() { CapacityLayerName = "ShrubSpace", ConsumedCapacityPerPop = 3f });
                this.IdealTemperatureRange = new Range(0.65f, 075f);
                this.IdealMoistureRange = new Range(0.75f, 0.95f);
                this.TemperatureExtremes = new Range(0.6f, 0.8f);
                this.MoistureExtremes = new Range(0.7f, 1f);
                this.MaxPollutionDensity = 0.7f;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 5;

            }
        }
    }

    [Serialized]
    public class CookeinaMushroomBlock : InteractablePlantBlock { }
}