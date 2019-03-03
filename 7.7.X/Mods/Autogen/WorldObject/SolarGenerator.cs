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
            this.GetComponent<MinimapComponent>().Initialize("Power");                                 
            this.GetComponent<PowerGridComponent>().Initialize(30, new ElectricPower());        
            this.GetComponent<PowerGeneratorComponent>().Initialize(750);                       
            this.GetComponent<HousingComponent>().Set(SolarGeneratorItem.HousingVal);                                


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

        static SolarGeneratorItem()
        {
            
        }
        
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
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SolarGeneratorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ReinforcedConcreteItem>(typeof(ElectronicsSkill), 6, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<ServoItem>(typeof(ElectronicsSkill), 5, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(ElectronicsSkill), 10, ElectronicsSkill.MultiplicativeStrategy),
                new CraftingElement<GlassItem>(typeof(ElectronicsSkill), 50, ElectronicsSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(SolarGeneratorRecipe), Item.Get<SolarGeneratorItem>().UILink(), 50, typeof(ElectronicsSkill));
            this.Initialize(Localizer.DoStr("Solar Generator"), typeof(SolarGeneratorRecipe));
            CraftingComponent.AddRecipe(typeof(ElectronicsAssemblyObject), this);
        }
    }
}