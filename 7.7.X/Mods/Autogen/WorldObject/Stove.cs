namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Housing;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Property;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Gameplay.Pipes.LiquidComponents;
    using Gameplay.Pipes.Gases;
    using Gameplay.Systems.Tooltip;
    using Shared.Localization;
    using Shared.Serialization;
    using Shared.Utils;

    [Serialized]
    [RequireComponent(typeof(ChimneyComponent))]
    [RequireComponent(typeof(LiquidProducerComponent))]
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(FuelConsumptionComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(SolidGroundComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]
    [RequireRoomMaterialTier(1.8f, typeof(SmeltingLavishReqTalent), typeof(SmeltingFrugalReqTalent))]
    public partial class StoveObject :
        WorldObject,
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Stove"); } }

        public virtual Type RepresentedItemType { get { return typeof(StoveItem); } }


        private static Type[] fuelTypeList = new[]
        {
            typeof(LogItem),
            typeof(LumberItem),
            typeof(CharcoalItem),
            typeof(ArrowItem),
            typeof(BoardItem),
            typeof(CoalItem)
        };

        protected override void Initialize()
        {

            GetComponent<MinimapComponent>().Initialize("Cooking");
            GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);
            GetComponent<FuelConsumptionComponent>().Initialize(10);
            GetComponent<HousingComponent>().Set(StoveItem.HousingVal);

            GetComponent<LiquidProducerComponent>().Setup(typeof(SmogItem), (int)(0.4f * 1000f), NamedOccupancyOffset("ChimneyOut"));
        }

        public override void Destroy()
        {
            base.Destroy();
        }

    }

    [Serialized]
    public partial class StoveItem :
        WorldObjectItem<StoveObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Stove"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A heavy stove for cooking more complex dishes."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren]
        public static HousingValue HousingVal
        {
            get
            {
                return new HousingValue()
                {
                    Category = "Kitchen",
                    Val = 4,
                    TypeForRoomLimit = "Cooking",
                    DiminishingReturnPercent = 0.3f
                };
            }
        }

        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(10))); } }
    }


    [RequiresSkill(typeof(SmeltingSkill), 0)]
    public partial class StoveRecipe : Recipe
    {
        public StoveRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<StoveItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelPlateItem>(typeof(SmeltingSkill), 40, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(SmeltingSkill), 30, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<HingeItem>(typeof(SmeltingSkill), 10, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<GlassJarItem>(typeof(SmeltingSkill), 5, SmeltingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(StoveRecipe), Item.Get<StoveItem>().UILink(), 20, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Stove"), typeof(StoveRecipe));
            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }
}