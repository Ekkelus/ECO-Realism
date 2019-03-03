namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
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

    [RequiresSkill(typeof(SeedProductionSkill), 1)]
    public class CriminiMushroomSporesRecipe : Recipe
    {
        public CriminiMushroomSporesRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CriminiMushroomSporesItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CriminiMushroomsItem>(typeof(SeedProductionEfficiencySkill), 2, SeedProductionEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CriminiMushroomSporesRecipe), Item.Get<CriminiMushroomSporesItem>().UILink(), 0.5f, typeof(SeedProductionSpeedSkill));

            this.Initialize(Localizer.DoStr("Crimini Mushroom Spores"), typeof(CriminiMushroomSporesRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }



    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class CriminiMushroomSporesPackItem : SeedPackItem
    {
        static CriminiMushroomSporesPackItem() { }

        public override LocString DisplayName { get { return Localizer.DoStr("Crimini Mushroom Spores Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow crimini mushrooms."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("CriminiMushroom"); } }
    }

}