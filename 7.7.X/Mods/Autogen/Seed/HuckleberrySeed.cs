namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;
    using Gameplay.Players;
    using System.ComponentModel;

    [Serialized]
    [Weight(10)]  
    public partial class HuckleberrySeedItem : SeedItem
    {
        static HuckleberrySeedItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Huckleberry Seed"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow a huckleberry bush."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Huckleberry"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class HuckleberrySeedPackItem : SeedPackItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Huckleberry Seed Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow a huckleberry bush."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Huckleberry"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 2)]    
    public class HuckleberrySeedRecipe : Recipe
    {
        public HuckleberrySeedRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<HuckleberrySeedItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HuckleberriesItem>(typeof(FarmingSkill), 4, FarmingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(HuckleberrySeedRecipe), Item.Get<HuckleberrySeedItem>().UILink(), 0.5f, typeof(FarmingSkill));

            Initialize(Localizer.DoStr("Huckleberry Seed"), typeof(HuckleberrySeedRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}