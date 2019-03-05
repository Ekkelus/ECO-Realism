namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Housing;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Property;
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
    [RequireRoomVolume(4)]
    public partial class PlanterPotSquareObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Square Pot"); } } 

        public virtual Type RepresentedItemType { get { return typeof(PlanterPotSquareItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Housing");                                 
            GetComponent<HousingComponent>().Set(PlanterPotSquareItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1500)]
    public partial class PlanterPotSquareItem : WorldObjectItem<PlanterPotSquareObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Square Pot"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Sometimes you just want to bring a little bit of nature into your house."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 1,                                   
                                                    TypeForRoomLimit = "Decoration", 
                                                    DiminishingReturnPercent = 0.9f    
        };}}
    }


    [RequiresSkill(typeof(MortaringSkill), 3)]
    public partial class PlanterPotSquareRecipe : Recipe
    {
        public PlanterPotSquareRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<PlanterPotSquareItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(MortaringSkill), 10, MortaringSkill.MultiplicativeStrategy),
                new CraftingElement<PulpFillerItem>(typeof(MortaringSkill), 5, MortaringSkill.MultiplicativeStrategy)
            };
            CraftMinutes = CreateCraftTimeValue(typeof(PlanterPotSquareRecipe), Item.Get<PlanterPotSquareItem>().UILink(), 5, typeof(MortaringSkill));
            Initialize(Localizer.DoStr("Planter Pot Square"), typeof(PlanterPotSquareRecipe));
            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }
}