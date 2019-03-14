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
    public class AmanitaMushroom : PlantEntity
    {
        public AmanitaMushroom(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public AmanitaMushroom() { }
        static PlantSpecies species;
        public class AmanitaMushroomSpecies : BalancedPlantSpecies
        {
            public AmanitaMushroomSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(AmanitaMushroom);

                // Info
                this.Decorative = false;
                this.Name = "AmanitaMushroom";
                this.DisplayName = Localizer.DoStr("Amanita Mushroom");
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
                this.SeedItemType = typeof(AmanitaMushroomSporesItem);
                this.PostHarvestingGrowth = 0f;
                this.ScythingKills = true;
                this.PickableAtPercent = 0f;
                this.ResourceItemType = typeof(AmanitaMushroomsItem);
                this.ResourceRange = new Range(1f, 2f);
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(AmanitaMushroomBlock);
                // Climate
                this.ReleasesCO2ppmPerDay = -5E-06f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "Phosphorus", HalfSpeedConcentration = 0.1f, MaxResourceContent = 0.2f });
                this.ResourceConstraints.Add( new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration = 0.1f, MaxResourceContent = 0.05f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "FertileGround" , ConsumedCapacityPerPop = 1f });
                this.CapacityConstraints.Add( new CapacityConstraint() { CapacityLayerName = "ShrubSpace" , ConsumedCapacityPerPop = 3f });
                this.IdealTemperatureRange = new Range(19f, 21f);
                this.IdealMoistureRange = new Range(0.74f, 0.78f);
                this.TemperatureExtremes = new Range(17f, 26f);
                this.MoistureExtremes = new Range(0.4f, 1.3f);
                this.MaxPollutionDensity = 0.7f;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 5;

            }
        }
    }

    [Serialized]
    public class AmanitaMushroomBlock : InteractablePlantBlock { }
}