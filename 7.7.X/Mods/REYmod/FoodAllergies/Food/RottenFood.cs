namespace Eco.Mods.TechTree
{
    using System.ComponentModel;
    using Gameplay.Items;
    using Gameplay.Players;
    using Shared.Serialization;
    using Shared.Localization;


    /// <summary>
    /// Dummy for the Foodallergysystem
    /// </summary>   
    [Serialized]
    [Category("Hidden")]
    public partial class RottenFoodItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Rotten Food"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Something you were allergic to"); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };
        public override float Calories { get { return 100; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }
}
