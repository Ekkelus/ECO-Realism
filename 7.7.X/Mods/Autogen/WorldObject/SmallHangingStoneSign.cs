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
    public partial class SmallHangingStoneSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Hanging Stone Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SmallHangingStoneSignItem); } } 


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
    public partial class SmallHangingStoneSignItem :
        WorldObjectItem<SmallHangingStoneSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Hanging Stone Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A small sign for all of your smaller text needs!"); } }
    }


    [RequiresSkill(typeof(MortaringSkill), 2)]
    public partial class SmallHangingStoneSignRecipe : Recipe
    {
        public SmallHangingStoneSignRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SmallHangingStoneSignItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(MortaringSkill), 30, MortaringSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(SmallHangingStoneSignRecipe), Item.Get<SmallHangingStoneSignItem>().UILink(), 10, typeof(MortaringSkill));
            Initialize(Localizer.DoStr("Small Hanging Stone Sign"), typeof(SmallHangingStoneSignRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}