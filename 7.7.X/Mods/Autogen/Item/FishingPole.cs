namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;

    [RequiresSkill(typeof(HuntingSkill), 1)]   
    public partial class FishingPoleRecipe : Recipe
    {
        public FishingPoleRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<FishingPoleItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(HuntingSkill), 5, HuntingSkill.MultiplicativeStrategy),
                new CraftingElement<StringItem>(typeof(HuntingSkill), 2, HuntingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(FishingPoleRecipe), Item.Get<FishingPoleItem>().UILink(), 5, typeof(HuntingSkill));    
            Initialize(Localizer.DoStr("Fishing Pole"), typeof(FishingPoleRecipe));

            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }


}