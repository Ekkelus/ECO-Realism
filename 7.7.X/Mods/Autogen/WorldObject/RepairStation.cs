namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Shared.Localization;
    using Shared.Serialization;

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
            GetComponent<MinimapComponent>().Initialize("Crafting");                                 


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
    }


    public partial class RepairStationRecipe : Recipe
    {
        public RepairStationRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<RepairStationItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(20),
                new CraftingElement<StoneItem>(10),                                                                    
            };
            CraftMinutes = new ConstantValue(1); 
            Initialize(Localizer.DoStr("Repair Station"), typeof(RepairStationRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}