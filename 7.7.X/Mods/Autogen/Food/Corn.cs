namespace Eco.Mods.TechTree
{
    using Gameplay.Items;
    using Gameplay.Players;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]
    [Weight(10)]                                          
     [Yield(typeof(CornItem), typeof(GatheringSkill), new[] {1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f})]      
    public partial class CornItem :
        FoodItem            
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Corn"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Corn"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A warmly colored kernel studded vegetable."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 5, Fat = 0, Protein = 2, Vitamins = 1};
        public override float Calories                          { get { return 230; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}