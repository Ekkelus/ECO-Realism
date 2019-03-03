namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [Weight(10)]                                          
     [Yield(typeof(HuckleberriesItem), typeof(GatheringSkill), new float[] {1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f})]      
    public partial class HuckleberriesItem :
        FoodItem            
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Huckleberries"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Huckleberries"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A handfull of small wild berries. I'm your huckleberry."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 2, Fat = 0, Protein = 0, Vitamins = 6};
        public override float Calories                          { get { return 80; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}