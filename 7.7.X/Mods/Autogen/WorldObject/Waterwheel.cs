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
    using Eco.Shared.Utils;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerGeneratorComponent))]         
    [RequireComponent(typeof(HousingComponent))]                  
    public partial class WaterwheelObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Waterwheel"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WaterwheelItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Power");                                 
            this.GetComponent<PowerGridComponent>().Initialize(10, new MechanicalPower());        
            this.GetComponent<PowerGeneratorComponent>().Initialize(200);                       
            this.GetComponent<HousingComponent>().Set(WaterwheelItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(10000)]
    public partial class WaterwheelItem : WorldObjectItem<WaterwheelObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Waterwheel"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Use the power of flowing water to produce mechanical power."); } }

        static WaterwheelItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(8)] private LocString PowerProductionTooltip  { get { return new LocString(string.Format(Localizer.DoStr("Produces: {0}w"), Text.Info(200))); } } 
    }


    [RequiresSkill(typeof(PrimitiveMechanicsSkill), 2)]
    public partial class WaterwheelRecipe : Recipe
    {
        public WaterwheelRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WaterwheelItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(PrimitiveMechanicsEfficiencySkill), 30, PrimitiveMechanicsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(PrimitiveMechanicsEfficiencySkill), 50, PrimitiveMechanicsEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<WoodenGearItem>(typeof(PrimitiveMechanicsEfficiencySkill), 1, PrimitiveMechanicsEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(30, PrimitiveMechanicsSpeedSkill.MultiplicativeStrategy, typeof(PrimitiveMechanicsSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(WaterwheelRecipe), Item.Get<WaterwheelItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<WaterwheelItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Waterwheel", typeof(WaterwheelRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}