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
    public partial class RugMediumObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Medium Rug"); } } 

        public virtual Type RepresentedItemType { get { return typeof(RugMediumItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Housing");                                 
            GetComponent<HousingComponent>().Set(RugMediumItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1000)]
    public partial class RugMediumItem : WorldObjectItem<RugMediumObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Medium Rug"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A medium rug for medium uses."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Rug", 
                                                    DiminishingReturnPercent = 0.5f    
        };}}
    }


    [RequiresSkill(typeof(TailoringSkill), 3)]
    public partial class RugMediumRecipe : Recipe
    {
        public RugMediumRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<RugMediumItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<ClothItem>(typeof(TailoringSkill), 10, TailoringSkill.MultiplicativeStrategy),
                new CraftingElement<CelluloseFiberItem>(typeof(TailoringSkill), 5, TailoringSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(RugMediumRecipe), Item.Get<RugMediumItem>().UILink(), 20, typeof(TailoringSkill), typeof(TailoringFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Rug Medium"), typeof(RugMediumRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    }
}