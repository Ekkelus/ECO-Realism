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
    public partial class HewnLogDoorObject : 
        DoorObject, 
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Hewn Log Door"); } } 

        public override Type RepresentedItemType { get { return typeof(HewnLogDoorItem); } } 


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
    [Weight(1000)]
    public partial class HewnLogDoorItem :
        WorldObjectItem<HewnLogDoorObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Hewn Log Door"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A door made from roughly hewn logs."); } }


        static HewnLogDoorItem()
        {
            
        }

    }


    [RequiresSkill(typeof(HewingSkill), 2)]
    public partial class HewnLogDoorRecipe : Recipe
    {
        public HewnLogDoorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HewnLogDoorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),   
                new CraftingElement<HingeItem>(typeof(HewingSkill), 2, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(HewingSkill), 5, HewingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(HewnLogDoorRecipe), Item.Get<HewnLogDoorItem>().UILink(), 3, typeof(HewingSkill));
            this.Initialize(Localizer.DoStr("Hewn Log Door"), typeof(HewnLogDoorRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}