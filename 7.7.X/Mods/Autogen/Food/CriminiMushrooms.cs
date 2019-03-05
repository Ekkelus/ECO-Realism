namespace Eco.Mods.TechTree
{
    using Gameplay.Items;
    using Gameplay.Players;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]
    [Weight(10)]                                          
     [Yield(typeof(CriminiMushroomsItem), typeof(GatheringSkill), new[] {1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f})]      
    public partial class CriminiMushroomsItem :
        FoodItem            
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Crimini Mushrooms"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Edible mushrooms that are quite tasty."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 3, Fat = 1, Protein = 3, Vitamins = 1};
        public override float Calories                          { get { return 200; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}