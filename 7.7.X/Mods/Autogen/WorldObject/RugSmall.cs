namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Housing;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Gameplay.Systems.Tooltip;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(HousingComponent))] 
	[RequireComponent(typeof(SolidGroundComponent))] 	
    public partial class RugSmallObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Rug"); } } 

        public virtual Type RepresentedItemType { get { return typeof(RugSmallItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Housing");                                 
            GetComponent<HousingComponent>().Set(RugSmallItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(500)]
    public partial class RugSmallItem : WorldObjectItem<RugSmallObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Rug"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A small rug for when you just didn't have enough materials to make a bigger one."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 1,                                   
                                                    TypeForRoomLimit = "Rug", 
                                                    DiminishingReturnPercent = 0.5f    
        };}}
    }


    [RequiresSkill(typeof(TailoringSkill), 1)]
    public partial class RugSmallRecipe : Recipe
    {
        public RugSmallRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<RugSmallItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<ClothItem>(typeof(TailoringSkill), 10, TailoringSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(RugSmallRecipe), Item.Get<RugSmallItem>().UILink(), 15, typeof(TailoringSkill));
            Initialize(Localizer.DoStr("Rug Small"), typeof(RugSmallRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    }
}