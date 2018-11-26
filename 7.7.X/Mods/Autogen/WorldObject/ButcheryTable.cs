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
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(0.8f)]        
    public partial class ButcheryTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Butchery Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ButcheryTableItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Cooking");                                 
            this.GetComponent<HousingComponent>().Set(ButcheryTableItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class ButcheryTableItem : WorldObjectItem<ButcheryTableObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Butchery Table"); } } 
        public override string Description  { get { return  "A block and cleaver to process raw meat into fancier dishes."; } }

        static ButcheryTableItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Cooking", 
                                                    DiminishingReturnPercent = 0.3f    
        };}}
    }


    [RequiresSkill(typeof(WoodworkingSkill), 3)]
    public partial class ButcheryTableRecipe : Recipe
    {
        public ButcheryTableRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ButcheryTableItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(WoodworkingEfficiencySkill), 20, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(WoodworkingEfficiencySkill), 20, WoodworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(5, WoodworkingSpeedSkill.MultiplicativeStrategy, typeof(WoodworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(ButcheryTableRecipe), Item.Get<ButcheryTableItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<ButcheryTableItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Butchery Table", typeof(ButcheryTableRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}