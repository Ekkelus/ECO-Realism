namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class FishTrapObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fish Trap"); } } 

        public virtual Type RepresentedItemType { get { return typeof(FishTrapItem); } } 


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
    [Weight(1000)]
    public partial class FishTrapItem : WorldObjectItem<FishTrapObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fish Trap"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A trap to catch fish as they swim. "); } }

        static FishTrapItem()
        {
            
        }
        
    }


    [RequiresSkill(typeof(HuntingSkill), 3)]
    public partial class FishTrapRecipe : Recipe
    {
        public FishTrapRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FishTrapItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(HuntingSkill), 20, HuntingSkill.MultiplicativeStrategy),
                new CraftingElement<RopeItem>(typeof(HuntingSkill), 4, HuntingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(FishTrapRecipe), Item.Get<FishTrapItem>().UILink(), 10, typeof(HuntingSkill));
            this.Initialize(Localizer.DoStr("Fish Trap"), typeof(FishTrapRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
}