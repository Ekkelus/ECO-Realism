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
    public partial class SmallHangingHewnLogSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Hanging Hewn Log Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SmallHangingHewnLogSignItem); } } 


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
    public partial class SmallHangingHewnLogSignItem :
        WorldObjectItem<SmallHangingHewnLogSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Hanging Hewn Log Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A small sign for all of your smaller text needs!"); } }
    }


    [RequiresSkill(typeof(HewingSkill), 2)]
    public partial class SmallHangingHewnLogSignRecipe : Recipe
    {
        public SmallHangingHewnLogSignRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SmallHangingHewnLogSignItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(SmallHangingHewnLogSignRecipe), Item.Get<SmallHangingHewnLogSignItem>().UILink(), 10, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Small Hanging Hewn Log Sign"), typeof(SmallHangingHewnLogSignRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}