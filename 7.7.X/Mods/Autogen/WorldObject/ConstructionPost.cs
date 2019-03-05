namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.DynamicValues;
    using Gameplay.Economy;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Shared.Localization;
    using Shared.Serialization;

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
            GetComponent<MinimapComponent>().Initialize("Economy");                                 


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
        public override LocString DisplayDescription { get { return Localizer.DoStr("For contruction contracts."); } }
    }


    public partial class ConstructionPostRecipe : Recipe
    {
        public ConstructionPostRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<ConstructionPostItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(),                                                                    
            };
            CraftMinutes = new ConstantValue(2); 
            Initialize(Localizer.DoStr("Construction Post"), typeof(ConstructionPostRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}