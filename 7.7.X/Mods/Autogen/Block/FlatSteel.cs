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

    [RequiresSkill(typeof(AdvancedSmeltingSkill), 3)]   
    public partial class FlatSteelRecipe : Recipe
    {
        public FlatSteelRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<FlatSteelItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<CorrugatedSteelItem>(typeof(AdvancedSmeltingSkill), 4, AdvancedSmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<EpoxyItem>(typeof(AdvancedSmeltingSkill), 1, AdvancedSmeltingSkill.MultiplicativeStrategy), 
            };
            CraftMinutes = CreateCraftTimeValue(typeof(FlatSteelRecipe), Item.Get<FlatSteelItem>().UILink(), 5, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent));    
            Initialize(Localizer.DoStr("Flat Steel"), typeof(FlatSteelRecipe));

            CraftingComponent.AddRecipe(typeof(RollingMillObject), this);
        }
    }

    [Serialized]
    [Solid, Wall, Constructed,BuildRoomMaterialOption]
    [Tier(4)]                                          
    [RequiresSkill(typeof(AdvancedSmeltingSkill), 3)]   
    public partial class FlatSteelBlock :
        Block            
        , IRepresentsItem     
    {
        public Type RepresentedItemType { get { return typeof(FlatSteelItem); } }    
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(10000)]      
    [Currency]                                              
    [ItemTier(4)]                                      
    public partial class FlatSteelItem :
    BlockItem<FlatSteelBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Flat Steel"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Coated with a layer of epoxy, this steel refuses to rust."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new[] {
            typeof(FlatSteelStacked1Block),
            typeof(FlatSteelStacked2Block),
            typeof(FlatSteelStacked3Block),
            typeof(FlatSteelStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class FlatSteelStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class FlatSteelStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class FlatSteelStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class FlatSteelStacked4Block : PickupableBlock { } //Only a wall if it's all 4 FlatSteel
}