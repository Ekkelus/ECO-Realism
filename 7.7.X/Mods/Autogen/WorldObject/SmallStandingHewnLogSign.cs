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
            this.GetComponent<MinimapComponent>().Initialize("Sign");                                 
            this.GetComponent<CustomTextComponent>().Initialize(700);                                       


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

        static SmallStandingHewnLogSignItem()
        {
            
        }

    }


    [RequiresSkill(typeof(HewingSkill), 1)]
    public partial class SmallStandingHewnLogSignRecipe : Recipe
    {
        public SmallStandingHewnLogSignRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SmallStandingHewnLogSignItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(HewingSkill), 4, HewingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(SmallStandingHewnLogSignRecipe), Item.Get<SmallStandingHewnLogSignItem>().UILink(), 10, typeof(HewingSkill));
            this.Initialize(Localizer.DoStr("Small Standing Hewn Log Sign"), typeof(SmallStandingHewnLogSignRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}