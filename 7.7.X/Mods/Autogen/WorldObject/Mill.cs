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
    using Eco.Shared.Utils;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerConsumptionComponent))]                     
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(1)]        
    public partial class MillObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mill"); } } 

        public virtual Type RepresentedItemType { get { return typeof(MillItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Cooking");                                 
            this.GetComponent<PowerConsumptionComponent>().Initialize(100);                      
            this.GetComponent<PowerGridComponent>().Initialize(5, new MechanicalPower());        
            this.GetComponent<HousingComponent>().Set(MillItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class MillItem : WorldObjectItem<MillObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mill"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Refines food resources by crushing them under a stone millstone."); } }

        static MillItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 3,                                   
                                                    TypeForRoomLimit = "Food Preparation", 
                                                    DiminishingReturnPercent = 0.3f    
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(75))); } }  
    }


    [RequiresSkill(typeof(StoneworkingSkill), 0)]
    public partial class MillRecipe : Recipe
    {
        public MillRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<MillItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(StoneworkingEfficiencySkill), 35, StoneworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LogItem>(typeof(StoneworkingEfficiencySkill), 15, StoneworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(StoneworkingEfficiencySkill), 4, StoneworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(StoneworkingEfficiencySkill), 8, StoneworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(20, StoneworkingSpeedSkill.MultiplicativeStrategy, typeof(StoneworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(MillRecipe), Item.Get<MillItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<MillItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Mill", typeof(MillRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}