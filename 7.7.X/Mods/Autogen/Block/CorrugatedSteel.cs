namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Blocks;
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;
    using World;
    using World.Blocks;

    [RequiresSkill(typeof(AdvancedSmeltingSkill), 2)]   
    public partial class CorrugatedSteelRecipe : Recipe
    {
        public CorrugatedSteelRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<CorrugatedSteelItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(AdvancedSmeltingSkill), 2, AdvancedSmeltingSkill.MultiplicativeStrategy), 
            };
            CraftMinutes = CreateCraftTimeValue(typeof(CorrugatedSteelRecipe), Item.Get<CorrugatedSteelItem>().UILink(), 2, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent));    
            Initialize(Localizer.DoStr("Corrugated Steel"), typeof(CorrugatedSteelRecipe));

            CraftingComponent.AddRecipe(typeof(RollingMillObject), this);
        }
    }

    [Serialized]
    [Solid, Wall, Constructed]
    [Tier(3)]                                          
    [RequiresSkill(typeof(AdvancedSmeltingSkill), 2)]   
    public partial class CorrugatedSteelBlock :
        Block            
        , IRepresentsItem     
    {
        public Type RepresentedItemType { get { return typeof(CorrugatedSteelItem); } }    
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(10000)]      
    [Currency]                                              
    [ItemTier(3)]                                      
    public partial class CorrugatedSteelItem :
    BlockItem<CorrugatedSteelBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Corrugated Steel"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Especially useful for industrial buildings."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new[] {
            typeof(CorrugatedSteelStacked1Block),
            typeof(CorrugatedSteelStacked2Block),
            typeof(CorrugatedSteelStacked3Block),
            typeof(CorrugatedSteelStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class CorrugatedSteelStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class CorrugatedSteelStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class CorrugatedSteelStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class CorrugatedSteelStacked4Block : PickupableBlock { } //Only a wall if it's all 4 CorrugatedSteel
}