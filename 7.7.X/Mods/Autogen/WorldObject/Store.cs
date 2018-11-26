namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Property;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

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
            this.GetComponent<MinimapComponent>().Initialize("Economy");                                 


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

        static StoreItem()
        {
            
        }

    }


    public partial class StoreRecipe : Recipe
    {
        public StoreRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StoreItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(10),
                new CraftingElement<StoneItem>(10),                                                                    
            };
            this.CraftMinutes = new ConstantValue(15); 
            this.Initialize(Localizer.Do("Store"), typeof(StoreRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}