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
    public partial class SmallStandingHewnLogSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Standing Hewn Log Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SmallStandingHewnLogSignItem); } } 


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
    [Weight(500)]
    public partial class SmallStandingHewnLogSignItem :
        WorldObjectItem<SmallStandingHewnLogSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Standing Hewn Log Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A small sign for all of your smaller text needs!"); } }
    }


    [RequiresSkill(typeof(HewingSkill), 1)]
    public partial class SmallStandingHewnLogSignRecipe : Recipe
    {
        public SmallStandingHewnLogSignRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SmallStandingHewnLogSignItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(HewingSkill), 4, HewingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(SmallStandingHewnLogSignRecipe), Item.Get<SmallStandingHewnLogSignItem>().UILink(), 10, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Small Standing Hewn Log Sign"), typeof(SmallStandingHewnLogSignRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}