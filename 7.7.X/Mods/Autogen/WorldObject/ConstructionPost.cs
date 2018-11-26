namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class ConstructionPostObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Construction Post"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ConstructionPostItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Economy");                                 


            this.AddAsPOI("Marker");  
        }

        public override void Destroy()
        {
            base.Destroy();
            this.RemoveAsPOI("Marker");  
        }
       
    }

    [Serialized]
    [Weight(250)]
    public partial class ConstructionPostItem :
        WorldObjectItem<ConstructionPostObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Construction Post"); } } 
        public override string Description  { get { return  "For contruction contracts."; } }

        static ConstructionPostItem()
        {
            
        }

    }


    public partial class ConstructionPostRecipe : Recipe
    {
        public ConstructionPostRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ConstructionPostItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(1),                                                                    
            };
            this.CraftMinutes = new ConstantValue(2); 
            this.Initialize("Construction Post", typeof(ConstructionPostRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}