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
    public partial class WindmillObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Windmill"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WindmillItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Power");                                 
            this.GetComponent<PowerGridComponent>().Initialize(10, new MechanicalPower());        
            this.GetComponent<PowerGeneratorComponent>().Initialize(200);                       
            this.GetComponent<HousingComponent>().Set(WindmillItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(10000)]
    public partial class WindmillItem : WorldObjectItem<WindmillObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Windmill"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Use wind to produce mechanical power."); } }

        static WindmillItem()
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


    [RequiresSkill(typeof(BasicEngineeringSkill), 3)]
    public partial class WindmillRecipe : Recipe
    {
        public WindmillRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WindmillItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(BasicEngineeringSkill), 5, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(BasicEngineeringSkill), 10, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<ClothItem>(typeof(BasicEngineeringSkill), 25, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<WoodenGearItem>(typeof(BasicEngineeringSkill), 1, BasicEngineeringSkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(30, BasicEngineeringSkill.MultiplicativeStrategy, typeof(BasicEngineeringSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(WindmillRecipe), Item.Get<WindmillItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<WindmillItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.DoStr("Windmill"), typeof(WindmillRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}