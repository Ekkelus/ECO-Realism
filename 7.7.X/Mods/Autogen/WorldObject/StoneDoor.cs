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
    }


    [RequiresSkill(typeof(MortaringSkill), 2)]
    public partial class StoneDoorRecipe : Recipe
    {
        public StoneDoorRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<StoneDoorItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(MortaringSkill), 40, MortaringSkill.MultiplicativeStrategy),   
                new CraftingElement<HingeItem>(typeof(HewingSkill), 2, HewingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(StoneDoorRecipe), Item.Get<StoneDoorItem>().UILink(), 3, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Stone Door"), typeof(StoneDoorRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}