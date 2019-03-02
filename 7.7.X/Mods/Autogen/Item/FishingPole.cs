namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(HuntingSkill), 1)]   
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
                new CraftingElement<LogItem>(typeof(HuntingSkill), 5, HuntingSkill.MultiplicativeStrategy),
                new CraftingElement<StringItem>(typeof(HuntingSkill), 2, HuntingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(FishingPoleRecipe), Item.Get<FishingPoleItem>().UILink(), 5, typeof(HuntingSkill));    
            this.Initialize(Localizer.DoStr("Fishing Pole"), typeof(FishingPoleRecipe));

            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }


}