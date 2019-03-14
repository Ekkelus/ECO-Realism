namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Gameplay.Objects;

    [Serialized]
    [Weight(10)]                                          
    [Yield(typeof(PapayaItem), typeof(GatheringSkill), new float[] {1f, 1.8f, 2.0f, 2.2f, 2.4f, 2.6f, 2.8f, 3.0f})][Tag("Crop")]      
    [Crop]                                                      
    public partial class PapayaItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Papaya"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Nice fresh Papaya"); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 4, Fat = 0, Protein = 0, Vitamins = 4};
        public override float Calories                          { get { return 250; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}