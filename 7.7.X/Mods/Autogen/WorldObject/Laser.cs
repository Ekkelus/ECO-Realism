namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Housing;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Gameplay.Systems.Tooltip;
    using Shared.Localization;
    using Shared.Serialization;
    using Shared.Utils;


    [Serialized]
    [Weight(5000)]
    public partial class LaserItem : WorldObjectItem<LaserObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Laser"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("AVOID DIRECT EYE EXPOSURE. Needs to be placed on 3x3 reinforced concrete."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(15000))); } }  
    }


    [RequiresSkill(typeof(ElectronicsSkill), 4)]
    public partial class LaserRecipe : Recipe
    {
        public LaserRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<LaserItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<GoldIngotItem>(typeof(ElectronicsSkill), 125, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(ElectronicsSkill), 125, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(ElectronicsSkill), 70, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<DiamondCutItem>(typeof(ElectronicsSkill), 50, ElectronicsSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(LaserRecipe), Item.Get<LaserItem>().UILink(), 240, typeof(ElectronicsSkill));
            Initialize(Localizer.DoStr("Laser"), typeof(LaserRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}