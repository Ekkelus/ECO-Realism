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
    using Shared.Utils;

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
            GetComponent<MinimapComponent>().Initialize("Power");                                 
            GetComponent<PowerGridComponent>().Initialize(10, new MechanicalPower());        
            GetComponent<PowerGeneratorComponent>().Initialize(200);                       
            GetComponent<HousingComponent>().Set(WindmillItem.HousingVal);                                


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
            Products = new CraftingElement[]
            {
                new CraftingElement<WindmillItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(BasicEngineeringSkill), 5, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<BoardItem>(typeof(BasicEngineeringSkill), 10, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<ClothItem>(typeof(BasicEngineeringSkill), 25, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<WoodenGearItem>(typeof(BasicEngineeringSkill), 1, BasicEngineeringSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(WindmillRecipe), Item.Get<WindmillItem>().UILink(), 30, typeof(BasicEngineeringSkill));
            Initialize(Localizer.DoStr("Windmill"), typeof(WindmillRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}