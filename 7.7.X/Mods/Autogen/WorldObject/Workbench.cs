namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

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
            this.GetComponent<MinimapComponent>().Initialize("Crafting");                                 



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

        static WorkbenchItem()
        {
            
        }

    }


    public partial class WorkbenchRecipe : Recipe
    {
        public WorkbenchRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WorkbenchItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(30),
                new CraftingElement<StoneItem>(20),   
            };
            this.CraftMinutes = new ConstantValue(5);
            this.Initialize(Localizer.Do("Workbench"), typeof(WorkbenchRecipe));
            CraftingComponent.AddRecipe(typeof(CampsiteObject), this);
        }
    }
}