namespace Eco.Mods.TechTree
{
    using Gameplay.Items;
    using Shared.Localization;
    using Shared.Serialization;



    [Serialized]
    [Weight(10)]      
    [Yield(typeof(PlantFibersItem), typeof(GatheringSkill), new[] { 1f, 1.8f, 2.0f, 2.2f, 2.4f, 2.6f, 2.8f, 3.0f })]  
    [Currency]              
    public partial class PlantFibersItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Plant Fibers"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Harvested from a number of plants, these fibers are useful for a suprising number of things."); } }

    }

}