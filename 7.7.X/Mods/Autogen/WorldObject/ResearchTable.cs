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
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    [RequireRoomMaterialTier(0.5f)]        
    public partial class ResearchTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Research Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ResearchTableItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Research");                                 


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class ResearchTableItem :
        WorldObjectItem<ResearchTableObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Research Table"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A basic table for researching new technologies and skills."); } }
    }



    public partial class ResearchTableRecipe : Recipe
    {
        public ResearchTableRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<ResearchTableItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(30),
                new CraftingElement<StoneItem>(40),
                new CraftingElement<PlantFibersItem>(30),                                                                    
            };
            CraftMinutes = new ConstantValue(5); 
            Initialize(Localizer.DoStr("Research Table"), typeof(ResearchTableRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}