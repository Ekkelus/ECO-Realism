namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Players;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;
    using Shared.Localization;

    [Serialized]
    [Weight(50)]                                          
    public partial class RawFishItem :
        FoodItem            
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Raw Fish"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A fatty cut of raw fish."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 7, Protein = 3, Vitamins = 0};
        public override float Calories                          { get { return 200; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }


    public partial class RawSalmonRecipe : Recipe
    {
        public RawSalmonRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<RawFishItem>(),

            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SalmonFilletItem>(),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(RawSalmonRecipe), Item.Get<RawFishItem>().UILink(), 0.01f, typeof(HuntingSkill));
            Initialize(Localizer.DoStr("Raw Salmon"), typeof(RawSalmonRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }

    public partial class RawTroutRecipe : Recipe
    {
        public RawTroutRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<RawFishItem>(),

            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<TroutFilletItem>(),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(RawTroutRecipe), Item.Get<RawFishItem>().UILink(), 0.01f, typeof(HuntingSkill));
            Initialize(Localizer.DoStr("Raw Trout"), typeof(RawTroutRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }


    public partial class RawTunaRecipe : Recipe
    {
        public RawTunaRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<RawFishItem>(),

            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<TunaFilletItem>(),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(RawTunaRecipe), Item.Get<RawFishItem>().UILink(), 0.01f, typeof(HuntingSkill));
            Initialize(Localizer.DoStr("Raw Tuna"), typeof(RawTunaRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }

} 