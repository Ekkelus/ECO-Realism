namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Math;
    using Shared.Localization;
    using Shared.Serialization;

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(FuelConsumptionComponent))]
    [RequireComponent(typeof(SolidGroundComponent))]
    public partial class BeehiveObject :
        WorldObject
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Beehive"); } }

        private static Type[] fuelTypeList = new[]
        {
            typeof(BeeItem),
        };

        protected override void Initialize()
        {
            GetComponent<MinimapComponent>().Initialize("Crafting");
            GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);
            GetComponent<FuelConsumptionComponent>().Initialize(1);
            GetComponent<PropertyAuthComponent>().Initialize();



        }

        public override void Destroy()
        {
            base.Destroy();
        }

    }

    [Serialized]
    [Weight(1000)]
    public partial class BeehiveItem : WorldObjectItem<BeehiveObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Beehive"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Oh, beehive!"); } }

        static BeehiveItem()
        {
            WorldObject.AddOccupancy<BeehiveObject>(new List<BlockOccupancy>(){
            new BlockOccupancy(new Vector3i(0, 1, 0)),
            });
        }

    }


    [RequiresSkill(typeof(FarmingSkill), 1)]
    public partial class BeehiveRecipe : Recipe
    {
        public BeehiveRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<BeehiveItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(FarmingSkill), 50, FarmingSkill.MultiplicativeStrategy),
                new CraftingElement<GlassItem>(typeof(FarmingSkill), 10, FarmingSkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(FarmingSkill), 4, FarmingSkill.MultiplicativeStrategy),
                new CraftingElement<GlueItem>(typeof(FarmingSkill), 8, FarmingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(BeehiveRecipe), Item.Get<BeehiveItem>().UILink(), 1, typeof(FarmingSkill));
            Initialize(Localizer.DoStr("Beehive"), typeof(BeehiveRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}