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
    [RequireComponent(typeof(OnOffComponent))]                   
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerGeneratorComponent))]         
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class SolarGeneratorObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("SolarPanels"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SolarGeneratorItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Power");                                 
            GetComponent<PowerGridComponent>().Initialize(30, new ElectricPower());        
            GetComponent<PowerGeneratorComponent>().Initialize(750);                       
            GetComponent<HousingComponent>().Set(SolarGeneratorItem.HousingVal);                                


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(10000)]
    public partial class SolarGeneratorItem : WorldObjectItem<SolarGeneratorObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Solar Generator"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Generates electrical power from the sun! It also stores energy to work at night."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(8)] private LocString PowerProductionTooltip  { get { return new LocString(string.Format(Localizer.DoStr("Produces: {0}w"), Text.Info(750))); } } 
    }


    [RequiresSkill(typeof(ElectronicsSkill), 4)]
    public partial class SolarGeneratorRecipe : Recipe
    {
        public SolarGeneratorRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SolarGeneratorItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<ReinforcedConcreteItem>(typeof(ElectronicsSkill), 6, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<ServoItem>(typeof(ElectronicsSkill), 5, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(ElectronicsSkill), 10, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<GlassItem>(typeof(ElectronicsSkill), 50, ElectronicsSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(SolarGeneratorRecipe), Item.Get<SolarGeneratorItem>().UILink(), 50, typeof(ElectronicsSkill), typeof(ElectronicsFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Solar Generator"), typeof(SolarGeneratorRecipe));
            CraftingComponent.AddRecipe(typeof(ElectronicsAssemblyObject), this);
        }
    }
}