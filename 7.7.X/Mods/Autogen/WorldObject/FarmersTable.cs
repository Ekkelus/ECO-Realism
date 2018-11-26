namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

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
            this.GetComponent<MinimapComponent>().Initialize("Crafting");                                 



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
        public override string Description  { get { return  "A basic table for creating farming tools and similar products."; } }

        static FarmersTableItem()
        {
            
        }

    }


    [RequiresSkill(typeof(FarmingSkill), 1)]
    public partial class FarmersTableRecipe : Recipe
    {
        public FarmersTableRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FarmersTableItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<DirtItem>(10),
                new CraftingElement<LogItem>(20),   
            };
            this.CraftMinutes = new ConstantValue(10);     
            this.Initialize("Farmers Table", typeof(FarmersTableRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}