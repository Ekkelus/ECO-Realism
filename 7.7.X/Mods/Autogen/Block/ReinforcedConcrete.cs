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

    [RequiresSkill(typeof(CementSkill), 1)]   
    public partial class ReinforcedConcreteRecipe : Recipe
    {
        public ReinforcedConcreteRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<ReinforcedConcreteItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(typeof(CementSkill), 1, CementSkill.MultiplicativeStrategy),
                new CraftingElement<RebarItem>(typeof(CementSkill), 1, CementSkill.MultiplicativeStrategy), 
            };
            CraftMinutes = CreateCraftTimeValue(typeof(ReinforcedConcreteRecipe), Item.Get<ReinforcedConcreteItem>().UILink(), 2, typeof(CementSkill), typeof(CementFocusedSpeedTalent));    
            Initialize(Localizer.DoStr("Reinforced Concrete"), typeof(ReinforcedConcreteRecipe));

            CraftingComponent.AddRecipe(typeof(CementKilnObject), this);
        }
    }

    [Serialized]
    [Solid, Wall, Constructed,BuildRoomMaterialOption]
    [Tier(3)]                                          
    [RequiresSkill(typeof(CementSkill), 1)]   
    public partial class ReinforcedConcreteBlock :
        Block            
        , IRepresentsItem     
    {
        public Type RepresentedItemType { get { return typeof(ReinforcedConcreteItem); } }    
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(10000)]      
    [Currency]                                              
    [ItemTier(3)]                                      
    public partial class ReinforcedConcreteItem :
    BlockItem<ReinforcedConcreteBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Reinforced Concrete"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Reinforced Concrete"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A study construction material poured around a latice of rebar."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new[] {
            typeof(ReinforcedConcreteStacked1Block),
            typeof(ReinforcedConcreteStacked2Block),
            typeof(ReinforcedConcreteStacked3Block),
            typeof(ReinforcedConcreteStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class ReinforcedConcreteStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class ReinforcedConcreteStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class ReinforcedConcreteStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class ReinforcedConcreteStacked4Block : PickupableBlock { } //Only a wall if it's all 4 ReinforcedConcrete
}