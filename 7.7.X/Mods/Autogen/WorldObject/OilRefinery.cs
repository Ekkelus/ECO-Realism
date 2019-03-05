namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Housing;
    using Gameplay.Items;
    using Gameplay.Objects;
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
    [RequireComponent(typeof(LiquidConverterComponent))]
    public partial class OilRefineryObject :
        WorldObject,
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Oil Refinery"); } }

        public virtual Type RepresentedItemType { get { return typeof(OilRefineryItem); } }


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

            GetComponent<MinimapComponent>().Initialize("Crafting");
            GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);
            GetComponent<FuelConsumptionComponent>().Initialize(50);
            GetComponent<HousingComponent>().Set(OilRefineryItem.HousingVal);

            GetComponent<LiquidProducerComponent>().Setup(typeof(SmogItem), (int)(1.4f * 1000f), NamedOccupancyOffset("ChimneyOut"));
            GetComponent<LiquidConverterComponent>().Setup(typeof(WaterItem), typeof(SewageItem), NamedOccupancyOffset("WaterInputPort"), NamedOccupancyOffset("SewageOutputPort"), 300);
        }

        public override void Destroy()
        {
            base.Destroy();
        }

    }

    [Serialized]
    public partial class OilRefineryItem :
        WorldObjectItem<OilRefineryObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Oil Refinery"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Sets of pipes and tanks which refine crude petroleum into usable products."); } }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren]
        public static HousingValue HousingVal
        {
            get
            {
                return new HousingValue()
                {
                    Category = "Industrial",
                    TypeForRoomLimit = "",
                };
            }
        }

        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(50))); } }
    }


    [RequiresSkill(typeof(MechanicsSkill), 0)]
    public partial class OilRefineryRecipe : Recipe
    {
        public OilRefineryRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<OilRefineryItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BrickItem>(typeof(MechanicsSkill), 30, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<GearItem>(typeof(MechanicsSkill), 40, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(MechanicsSkill), 40, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<CopperPipeItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(OilRefineryRecipe), Item.Get<OilRefineryItem>().UILink(), 50, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Oil Refinery"), typeof(OilRefineryRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }
}