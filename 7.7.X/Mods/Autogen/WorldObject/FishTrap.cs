namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;

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
            GetComponent<MinimapComponent>().Initialize("Economy");                                 



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
    }


    [RequiresSkill(typeof(HuntingSkill), 3)]
    public partial class FishTrapRecipe : Recipe
    {
        public FishTrapRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<FishTrapItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(HuntingSkill), 20, HuntingSkill.MultiplicativeStrategy),
                new CraftingElement<RopeItem>(typeof(HuntingSkill), 4, HuntingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(FishTrapRecipe), Item.Get<FishTrapItem>().UILink(), 10, typeof(HuntingSkill));
            Initialize(Localizer.DoStr("Fish Trap"), typeof(FishTrapRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
}