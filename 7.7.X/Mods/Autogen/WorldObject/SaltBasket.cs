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
	[RequireComponent(typeof(SolidGroundComponent))] 	
    [RequireComponent(typeof(HousingComponent))]                          
    public partial class SaltBasketObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Salt Basket"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SaltBasketItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Misc");                                 
            GetComponent<HousingComponent>().Set(SaltBasketItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(750)]
    public partial class SaltBasketItem : WorldObjectItem<SaltBasketObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Salt Basket"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A basket of salt."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Spices", 
                                                    DiminishingReturnPercent = 0.4f    
        };}}
    }


    [RequiresSkill(typeof(FertilizersSkill), 1)]
    public partial class SaltBasketRecipe : Recipe
    {
        public SaltBasketRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SaltBasketItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(FertilizersSkill), 10, FertilizersSkill.MultiplicativeStrategy),
                new CraftingElement<RopeItem>(typeof(FertilizersSkill), 6, FertilizersSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(SaltBasketRecipe), Item.Get<SaltBasketItem>().UILink(), 5, typeof(FertilizersSkill));
            Initialize(Localizer.DoStr("Salt Basket"), typeof(SaltBasketRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}