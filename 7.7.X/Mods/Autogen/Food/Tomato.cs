namespace Eco.Mods.TechTree
{
    using Gameplay.Items;
    using Gameplay.Players;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]
    [Weight(8)]                                          
     [Yield(typeof(TomatoItem), typeof(GatheringSkill), new[] {1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f})]      
    public partial class TomatoItem :
        FoodItem            
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tomato"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Intelligence is knowing this is a fruit; wisdom is not putting it in a fruit salad."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 4, Fat = 0, Protein = 1, Vitamins = 3};
        public override float Calories                          { get { return 240; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}