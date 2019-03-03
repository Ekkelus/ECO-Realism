namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [Weight(10)]                                          
     [Yield(typeof(FiddleheadsItem), typeof(GatheringSkill), new float[] {1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f})]      
    public partial class FiddleheadsItem :
        FoodItem            
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fiddleheads"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Fiddleheads"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Acollection of the furled fronds of young ferns; a unique addition to a meal."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 3, Fat = 0, Protein = 1, Vitamins = 3};
        public override float Calories                          { get { return 80; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}