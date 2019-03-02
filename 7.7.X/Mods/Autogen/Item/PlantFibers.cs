namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;



    [Serialized]
    [Weight(10)]      
    [Yield(typeof(PlantFibersItem), typeof(GatheringSkill), new float[] { 1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f })]  
    [Currency]              
    public partial class PlantFibersItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Plant Fibers"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Harvested from a number of plants, these fibers are useful for a suprising number of things."); } }

    }

}