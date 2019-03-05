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
    [RequireComponent(typeof(CustomTextComponent))]              
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class LargeStandingHewnLogSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Standing Hewn Log Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(LargeStandingHewnLogSignItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Sign");                                 
            GetComponent<CustomTextComponent>().Initialize(700);                                       


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1000)]
    public partial class LargeStandingHewnLogSignItem :
        WorldObjectItem<LargeStandingHewnLogSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Large Standing Hewn Log Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A large sign for all your large text needs!"); } }
    }


    [RequiresSkill(typeof(HewingSkill), 3)]
    public partial class LargeStandingHewnLogSignRecipe : Recipe
    {
        public LargeStandingHewnLogSignRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<LargeStandingHewnLogSignItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(HewingSkill), 25, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(HewingSkill), 8, HewingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(LargeStandingHewnLogSignRecipe), Item.Get<LargeStandingHewnLogSignItem>().UILink(), 10, typeof(HewingSkill));
            Initialize(Localizer.DoStr("Large Standing Hewn Log Sign"), typeof(LargeStandingHewnLogSignRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}