namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [Weight(10)]                                          
     [Yield(typeof(AmanitaMushroomsItem), typeof(WetlandsWandererSkill), new float[] {1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f})]      
    public partial class AmanitaMushroomsItem :
        FoodItem            
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Amanita Mushrooms"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A potentially poisonous mushroom. It might be wise to not eat it."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0};
        public override float Calories                          { get { return -200; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}