namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Property;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(0.9f)]        
    public partial class StoreObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Store"); } } 

        public virtual Type RepresentedItemType { get { return typeof(StoreItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Economy");                                 


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class StoreItem :
        WorldObjectItem<StoreObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Store"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Allows the selling and trading of items."); } }
    }


    public partial class StoreRecipe : Recipe
    {
        public StoreRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<StoreItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(10),
                new CraftingElement<StoneItem>(10),                                                                    
            };
            CraftMinutes = new ConstantValue(15); 
            Initialize(Localizer.DoStr("Store"), typeof(StoreRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}