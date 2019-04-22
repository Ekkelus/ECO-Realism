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
    }


    [RequiresSkill(typeof(HewingSkill), 2)]
    public partial class HewnLogDoorRecipe : Recipe
    {
        public HewnLogDoorRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<HewnLogDoorItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),   
                new CraftingElement<HingeItem>(typeof(HewingSkill), 2, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(HewingSkill), 5, HewingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(HewnLogDoorRecipe), Item.Get<HewnLogDoorItem>().UILink(), 3, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Hewn Log Door"), typeof(HewnLogDoorRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}