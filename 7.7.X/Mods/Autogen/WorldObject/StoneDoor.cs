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
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class StoneDoorObject : 
        DoorObject, 
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Stone Door"); } } 

        public override Type RepresentedItemType { get { return typeof(StoneDoorItem); } } 


        protected override void Initialize()
        {
            base.Initialize(); 


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(2000)]
    public partial class StoneDoorItem :
        WorldObjectItem<StoneDoorObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Stone Door"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A heavy stone door."); } }


        static StoneDoorItem()
        {
            
        }

    }


    [RequiresSkill(typeof(MortaringSkill), 2)]
    public partial class StoneDoorRecipe : Recipe
    {
        public StoneDoorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StoneDoorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(MortaringSkill), 40, MortaringSkill.MultiplicativeStrategy),   
                new CraftingElement<HingeItem>(typeof(HewingSkill), 2, HewingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(StoneDoorRecipe), Item.Get<StoneDoorItem>().UILink(), 3, typeof(MortaringSkill));
            this.Initialize(Localizer.DoStr("Stone Door"), typeof(StoneDoorRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}