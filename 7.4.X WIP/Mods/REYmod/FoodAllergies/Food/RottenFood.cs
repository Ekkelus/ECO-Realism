namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    
    
    /// <summary>
    /// Dummy for the Foodallergysystem
    /// </summary>   
    [Serialized]
    [Category("Hidden")]
    public partial class RottenFoodItem :
        FoodItem
    {
        public override string FriendlyName { get { return "Rotten Food"; } }
        public override string Description { get { return "Something you were allergic to"; } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };
        public override float Calories { get { return 100; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }
}
