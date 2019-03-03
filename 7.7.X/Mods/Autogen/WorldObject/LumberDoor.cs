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
    public partial class LumberDoorObject : 
        DoorObject, 
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lumber Door"); } } 

        public override Type RepresentedItemType { get { return typeof(LumberDoorItem); } } 


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
    [Weight(1500)]
    public partial class LumberDoorItem :
        WorldObjectItem<LumberDoorObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Lumber Door"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A door made from finely cut lumber."); } }


        static LumberDoorItem()
        {
            
        }

    }


    [RequiresSkill(typeof(LumberSkill), 2)]
    public partial class LumberDoorRecipe : Recipe
    {
        public LumberDoorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LumberDoorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LumberItem>(typeof(LumberSkill), 10, LumberSkill.MultiplicativeStrategy),   
                new CraftingElement<HingeItem>(typeof(HewingSkill), 2, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(HewingSkill), 5, HewingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(LumberDoorRecipe), Item.Get<LumberDoorItem>().UILink(), 3, typeof(LumberSkill));
            this.Initialize(Localizer.DoStr("Lumber Door"), typeof(LumberDoorRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}