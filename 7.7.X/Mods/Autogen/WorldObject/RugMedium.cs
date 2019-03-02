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
    public partial class RugMediumObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Medium Rug"); } } 

        public virtual Type RepresentedItemType { get { return typeof(RugMediumItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Housing");                                 
            this.GetComponent<HousingComponent>().Set(RugMediumItem.HousingVal);                                


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

        static RugMediumItem()
        {
            
        }
        
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
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RugMediumItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ClothItem>(typeof(TailoringSkill), 10, TailoringSkill.MultiplicativeStrategy),
                new CraftingElement<CelluloseFiberItem>(typeof(TailoringSkill), 5, TailoringSkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(20, TailoringSkill.MultiplicativeStrategy, typeof(TailoringSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(RugMediumRecipe), Item.Get<RugMediumItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<RugMediumItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Rug Medium"), typeof(RugMediumRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    }
}