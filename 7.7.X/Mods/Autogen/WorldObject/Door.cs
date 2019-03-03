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
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class DoorObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Door"); } } 

        public virtual Type RepresentedItemType { get { return typeof(DoorItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Misc");                                 



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1000)]
    public partial class DoorItem : WorldObjectItem<DoorObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Door"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A sturdy wooden door. Can be locked for certain players."); } }


        static DoorItem()
        {
            
        }

    }


    [RequiresSkill(typeof(HewingSkill), 0)]
    public partial class DoorRecipe : Recipe
    {
        public DoorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<DoorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
            new CraftingElement<LogItem>(typeof(HewingSkill), 6, HewingSkill.MultiplicativeStrategy),
            new CraftingElement<HingeItem>(typeof(HewingSkill), 2, HewingSkill.MultiplicativeStrategy),
            new CraftingElement<NailsItem>(typeof(HewingSkill), 5, HewingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(DoorRecipe), Item.Get<DoorItem>().UILink(), 5, typeof(HewingSkill));
            this.Initialize(Localizer.DoStr("Door"), typeof(DoorRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}