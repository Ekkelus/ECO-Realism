namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Property;
    using Gameplay.Skills;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    public partial class FarmersTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Farmers Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(FarmersTableItem); } } 


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
    [Weight(5000)]
    public partial class FarmersTableItem : WorldObjectItem<FarmersTableObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Farmers Table"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A basic table for creating farming tools and similar products."); } }
    }


    [RequiresSkill(typeof(FarmingSkill), 1)]
    public partial class FarmersTableRecipe : Recipe
    {
        public FarmersTableRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<FarmersTableItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<DirtItem>(10),
                new CraftingElement<LogItem>(20),   
            };
            CraftMinutes = new ConstantValue(10);     
            Initialize(Localizer.DoStr("Farmers Table"), typeof(FarmersTableRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}