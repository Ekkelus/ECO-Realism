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
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class WorkbenchObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Workbench"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WorkbenchItem); } } 


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
    public partial class WorkbenchItem : WorldObjectItem<WorkbenchObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Workbench"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A bench for the basics and making even more benches."); } }
    }


    public partial class WorkbenchRecipe : Recipe
    {
        public WorkbenchRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<WorkbenchItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(30), 
            };
            CraftMinutes = new ConstantValue(5);
            Initialize(Localizer.DoStr("Workbench"), typeof(WorkbenchRecipe));
            CraftingComponent.AddRecipe(typeof(CampsiteObject), this);
        }
    }
}