namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(RepairComponent))]                     
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class RepairStationObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Repair Station"); } } 

        public virtual Type RepresentedItemType { get { return typeof(RepairStationItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Crafting");                                 


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(3000)]
    public partial class RepairStationItem :
        WorldObjectItem<RepairStationObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Repair Station"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A place to fix up broken tools."); } }

        static RepairStationItem()
        {
            
        }

    }


    public partial class RepairStationRecipe : Recipe
    {
        public RepairStationRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RepairStationItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(20),
                new CraftingElement<StoneItem>(10),                                                                    
            };
            this.CraftMinutes = new ConstantValue(1); 
            this.Initialize("Repair Station", typeof(RepairStationRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}