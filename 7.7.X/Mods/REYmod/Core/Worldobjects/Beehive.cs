namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

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

        private static Type[] fuelTypeList = new Type[]
        {
            typeof(BeeItem),
        };

        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Crafting");
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);
            this.GetComponent<FuelConsumptionComponent>().Initialize(1);
            this.GetComponent<PropertyAuthComponent>().Initialize();



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


    [RequiresSkill(typeof(BeekeeperSkill), 1)]
    public partial class BeehiveRecipe : Recipe
    {
        public BeehiveRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BeehiveItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(BeekeeperEfficiencySkill), 50, BeekeeperEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GlassItem>(typeof(BeekeeperEfficiencySkill), 10, BeekeeperEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(BeekeeperEfficiencySkill), 4, BeekeeperEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GlueItem>(typeof(BeekeeperEfficiencySkill), 8, BeekeeperEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(1, BeekeeperSpeedSkill.MultiplicativeStrategy, typeof(BeekeeperSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(BeehiveRecipe), Item.Get<BeehiveItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<BeehiveItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize(Localizer.Do("Beehive"), typeof(BeehiveRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}