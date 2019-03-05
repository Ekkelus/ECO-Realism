namespace Eco.Mods.TechTree
{
    using System;
    using System.ComponentModel;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.DynamicValues;
    using Gameplay.Housing;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Systems.Tooltip;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class EckoStatueObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Ecko Statue"); } } 

        public virtual Type RepresentedItemType { get { return typeof(EckoStatueItem); } } 


        protected override void Initialize()
        {
            GetComponent<HousingComponent>().Set(EckoStatueItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(3000)]
    [Category("Hidden")]
    public partial class EckoStatueItem : WorldObjectItem<EckoStatueObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Ecko Statue"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A statue of a dolphin. What could it mean?"); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 100,                                   
                                                    TypeForRoomLimit = "Decoration", 
                                                    DiminishingReturnPercent = 1.5f    
        };}}
    }


    public partial class EckoStatueRecipe : Recipe
    {
        public EckoStatueRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<EckoStatueItem>(),
            };

            Ingredients = new CraftingElement[]
            {
            };
            CraftMinutes = new ConstantValue(); 
            Initialize(Localizer.DoStr("Ecko Statue"), typeof(EckoStatueRecipe));
            CraftingComponent.AddRecipe(typeof(Object), this);
        }
    }
}