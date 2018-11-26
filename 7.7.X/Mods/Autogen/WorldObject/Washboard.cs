namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(HousingComponent))]                          
    [RequireComponent(typeof(RoomRequirementsComponent))]
	[RequireComponent(typeof(SolidGroundComponent))] 
    [RequireRoomContainment]
    [RequireRoomVolume(4)]                              
    public partial class WashboardObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Washboard"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WashboardItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Housing");                                 
            this.GetComponent<HousingComponent>().Set(WashboardItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(3000)]
    public partial class WashboardItem : WorldObjectItem<WashboardObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Washboard"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Sometimes it can be nice to have clean clothes."); } }

        static WashboardItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Bathroom",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Washing", 
                                                    DiminishingReturnPercent = 0.5f    
        };}}
    }


    [RequiresSkill(typeof(ClothProductionSkill), 1)]
    public partial class WashboardRecipe : Recipe
    {
        public WashboardRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WashboardItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(ClothProductionEfficiencySkill), 10, ClothProductionEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(ClothProductionEfficiencySkill), 4, ClothProductionEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(5, ClothProductionSpeedSkill.MultiplicativeStrategy, typeof(ClothProductionSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(WashboardRecipe), Item.Get<WashboardItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<WashboardItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.Do("Washboard"), typeof(WashboardRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    }
}