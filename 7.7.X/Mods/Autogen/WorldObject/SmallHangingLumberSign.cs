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
    public partial class SmallHangingLumberSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Hanging Lumber Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(SmallHangingLumberSignItem); } } 


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
    public partial class SmallHangingLumberSignItem :
        WorldObjectItem<SmallHangingLumberSignObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Hanging Lumber Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A small sign for all of your smaller text needs!"); } }
    }


    [RequiresSkill(typeof(LumberSkill), 2)]
    public partial class SmallHangingLumberSignRecipe : Recipe
    {
        public SmallHangingLumberSignRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SmallHangingLumberSignItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(LumberSkill), 10, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(LumberSkill), 4, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(LumberSkill), 4, LumberSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(SmallHangingLumberSignRecipe), Item.Get<SmallHangingLumberSignItem>().UILink(), 10, typeof(LumberSkill), typeof(LumberFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Small Hanging Lumber Sign"), typeof(SmallHangingLumberSignRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}