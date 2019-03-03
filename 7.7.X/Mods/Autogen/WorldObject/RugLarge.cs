namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(HousingComponent))]
	[RequireComponent(typeof(SolidGroundComponent))] 	
    public partial class RugLargeObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Rug"); } } 

        public virtual Type RepresentedItemType { get { return typeof(RugLargeItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Housing");                                 
            this.GetComponent<HousingComponent>().Set(RugLargeItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1500)]
    public partial class RugLargeItem : WorldObjectItem<RugLargeObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Rug"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A large area rug to cover that weird stain."); } }

        static RugLargeItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 4,                                   
                                                    TypeForRoomLimit = "Rug", 
                                                    DiminishingReturnPercent = 0.5f    
        };}}
    }


    [RequiresSkill(typeof(TailoringSkill), 4)]
    public partial class RugLargeRecipe : Recipe
    {
        public RugLargeRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RugLargeItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ClothItem>(typeof(TailoringSkill), 20, TailoringSkill.MultiplicativeStrategy),
                new CraftingElement<CelluloseFiberItem>(typeof(TailoringSkill), 15, TailoringSkill.MultiplicativeStrategy),   
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(RugLargeRecipe), Item.Get<RugLargeItem>().UILink(), 25, typeof(TailoringSkill));
            this.Initialize(Localizer.DoStr("Rug Large"), typeof(RugLargeRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    }
}