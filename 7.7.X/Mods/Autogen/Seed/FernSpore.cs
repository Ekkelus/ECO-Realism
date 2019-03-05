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
    public partial class FernSporeItem : SeedItem
    {
        static FernSporeItem() { }
        
        private static Nutrients nutrition = new Nutrients() { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0 };

        public override LocString DisplayName { get { return Localizer.DoStr("Fern Spore"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow ferns."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Fern"); } }

        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }
    }


    [Serialized]
    [Category("Hidden")]
    [Weight(10)]  
    public partial class FernSporePackItem : SeedPackItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fern Spore Pack"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Plant to grow ferns."); } }
        public override LocString SpeciesName { get { return Localizer.DoStr("Fern"); } }
    }

    [RequiresSkill(typeof(FarmingSkill), 1)]    
    public class FernSporeRecipe : Recipe
    {
        public FernSporeRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<FernSporeItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<FiddleheadsItem>(typeof(FarmingSkill), 4, FarmingSkill.MultiplicativeStrategy),   
            };
            CraftMinutes = CreateCraftTimeValue(typeof(FernSporeRecipe), Item.Get<FernSporeItem>().UILink(), 0.5f, typeof(FarmingSkill));

            Initialize(Localizer.DoStr("Fern Spore"), typeof(FernSporeRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }
}