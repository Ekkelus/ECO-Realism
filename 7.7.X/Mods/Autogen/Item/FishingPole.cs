namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(FishingSkill), 1)]   
    public partial class FishingPoleRecipe : Recipe
    {
        public FishingPoleRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FishingPoleItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(FishingSkill), 5, FishingSkill.MultiplicativeStrategy),
                new CraftingElement<StringItem>(typeof(FishingSkill), 2, FishingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(FishingPoleRecipe), Item.Get<FishingPoleItem>().UILink(), 5, typeof(FishingSkill));    
            this.Initialize(Localizer.Do("Fishing Pole"), typeof(FishingPoleRecipe));

            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }


}