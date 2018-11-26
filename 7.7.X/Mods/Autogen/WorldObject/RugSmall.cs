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
    public partial class RugSmallObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Rug"); } } 

        public virtual Type RepresentedItemType { get { return typeof(RugSmallItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Housing");                                 
            this.GetComponent<HousingComponent>().Set(RugSmallItem.HousingVal);                                



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

        static RugSmallItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 1,                                   
                                                    TypeForRoomLimit = "Rug", 
                                                    DiminishingReturnPercent = 0.5f    
        };}}
    }


    [RequiresSkill(typeof(ClothProductionSkill), 1)]
    public partial class RugSmallRecipe : Recipe
    {
        public RugSmallRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RugSmallItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ClothItem>(typeof(ClothProductionEfficiencySkill), 10, ClothProductionEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(15, ClothProductionSpeedSkill.MultiplicativeStrategy, typeof(ClothProductionSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(RugSmallRecipe), Item.Get<RugSmallItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<RugSmallItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Rug Small"), typeof(RugSmallRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    }
}