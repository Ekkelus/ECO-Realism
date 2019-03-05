namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Economy;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    public partial class WoodSignObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wood Sign"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WoodSignItem); } } 


        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Sign");                                 


            this.AddAsPOI("Marker");  
        }

        public override void Destroy()
        {
            base.Destroy();
            this.RemoveAsPOI("Marker");  
        }
       
    }

    [Serialized]
    [Weight(1000)]
    public partial class WoodSignItem : WorldObjectItem<WoodSignObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wood Sign"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A large sign for all your large text needs!"); } }
    }


    [RequiresSkill(typeof(LumberSkill), 1)]
    public partial class WoodSignRecipe : Recipe
    {
        public WoodSignRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<WoodSignItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LumberItem>(typeof(LumberSkill), 4, LumberSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(LumberSkill), 8, LumberSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(WoodSignRecipe), Item.Get<WoodSignItem>().UILink(), 1, typeof(LumberSkill));
            Initialize(Localizer.DoStr("Wood Sign"), typeof(WoodSignRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}