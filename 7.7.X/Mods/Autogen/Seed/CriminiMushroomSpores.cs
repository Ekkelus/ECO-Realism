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
    public partial class CriminiMushroomSporesItem : SeedItem
    {
        static CriminiMushroomSporesItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Crimini Mushroom Spores"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow crimini mushrooms."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("CriminiMushroom"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]
    public class CriminiMushroomSporesRecipe : Recipe
    {
        public CriminiMushroomSporesRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<CriminiMushroomSporesItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<CriminiMushroomsItem>(typeof(FarmingSkill), 2, FarmingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(CriminiMushroomSporesRecipe), Item.Get<CriminiMushroomSporesItem>().UILink(), 0.5f, typeof(FarmingSkill));

            Initialize(Localizer.DoStr("Crimini Mushroom Spores"), typeof(CriminiMushroomSporesRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }



    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class CriminiMushroomSporesPackItem : SeedPackItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Crimini Mushroom Spores Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow crimini mushrooms."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("CriminiMushroom"); } }
    }

}