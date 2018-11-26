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
            this.GetComponent<MinimapComponent>().Initialize("Research");                                 


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
        public override string Description  { get { return  "A basic table for researching new technologies and skills."; } }

        static ResearchTableItem()
        {
            
        }

    }



    public partial class ResearchTableRecipe : Recipe
    {
        public ResearchTableRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ResearchTableItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(30),
                new CraftingElement<StoneItem>(40),
                new CraftingElement<PlantFibersItem>(30),                                                                    
            };
            this.CraftMinutes = new ConstantValue(5); 
            this.Initialize("Research Table", typeof(ResearchTableRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}