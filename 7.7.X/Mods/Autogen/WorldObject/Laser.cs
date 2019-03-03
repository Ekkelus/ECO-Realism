namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;


    [Serialized]
    [Weight(5000)]
    public partial class LaserItem : WorldObjectItem<LaserObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Laser"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("AVOID DIRECT EYE EXPOSURE. Needs to be placed on 3x3 reinforced concrete."); } }

        static LaserItem()
        {
            
        }

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
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LaserItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GoldIngotItem>(typeof(ElectronicsSkill), 125, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<SteelItem>(typeof(ElectronicsSkill), 125, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(ElectronicsSkill), 70, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<DiamondCutItem>(typeof(ElectronicsSkill), 50, ElectronicsSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(LaserRecipe), Item.Get<LaserItem>().UILink(), 240, typeof(ElectronicsSkill));
            this.Initialize(Localizer.DoStr("Laser"), typeof(LaserRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}