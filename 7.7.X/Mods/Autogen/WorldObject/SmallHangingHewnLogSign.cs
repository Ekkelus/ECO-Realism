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
    public partial class SmallHangingHewnLogSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Hanging Hewn Log Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SmallHangingHewnLogSignItem); } } 


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
    public partial class SmallHangingHewnLogSignItem :
        WorldObjectItem<SmallHangingHewnLogSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Hanging Hewn Log Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A small sign for all of your smaller text needs!"); } }

        static SmallHangingHewnLogSignItem()
        {
            
        }

    }


    [RequiresSkill(typeof(HewingSkill), 2)]
    public partial class SmallHangingHewnLogSignRecipe : Recipe
    {
        public SmallHangingHewnLogSignRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SmallHangingHewnLogSignItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(SmallHangingHewnLogSignRecipe), Item.Get<SmallHangingHewnLogSignItem>().UILink(), 10, typeof(HewingSkill));
            this.Initialize(Localizer.DoStr("Small Hanging Hewn Log Sign"), typeof(SmallHangingHewnLogSignRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}