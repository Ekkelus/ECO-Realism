namespace Eco.Mods.TechTree
{
    using Gameplay.Items;
    using Gameplay.Players;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]
    [Weight(10)]                                          
     [Yield(typeof(WheatItem), typeof(GatheringSkill), new[] {1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f})]      
    public partial class WheatItem :
        FoodItem            
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wheat"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Wheat"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A common grain that is significantly more useful processed."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 6, Fat = 0, Protein = 2, Vitamins = 0};
        public override float Calories                          { get { return 130; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}