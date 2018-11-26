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
    [RequireRoomVolume(16)]                              
    [RequireRoomMaterialTier(0.8f)]        
    public partial class WoodenStrawBedObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Straw Bed"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WoodenStrawBedItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Misc");                                 
            this.GetComponent<HousingComponent>().Set(WoodenStrawBedItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(3000)]
    public partial class WoodenStrawBedItem : WorldObjectItem<WoodenStrawBedObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Straw Bed"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A nice, scratchy and horrible uncomfortable bed. But at least it keeps you off the ground."); } }

        static WoodenStrawBedItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Bedroom",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Bed", 
                                                    DiminishingReturnPercent = 0.4f    
        };}}
    }


    [RequiresSkill(typeof(WoodworkingSkill), 3)]
    public partial class WoodenStrawBedRecipe : Recipe
    {
        public WoodenStrawBedRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WoodenStrawBedItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(WoodworkingEfficiencySkill), 10, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(WoodworkingEfficiencySkill), 20, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PlantFibersItem>(typeof(WoodworkingEfficiencySkill), 20, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(WoodworkingEfficiencySkill), 16, WoodworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(5, WoodworkingSpeedSkill.MultiplicativeStrategy, typeof(WoodworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(WoodenStrawBedRecipe), Item.Get<WoodenStrawBedItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<WoodenStrawBedItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Wooden Straw Bed"), typeof(WoodenStrawBedRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}