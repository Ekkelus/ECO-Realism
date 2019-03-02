namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(LumberSkill), 3)] 
    public class SawBoardsRecipe : Recipe
    {
        public SawBoardsRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<BoardItem>(2),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(LumberSkill), 2, LumberSkill.MultiplicativeStrategy), 
            };
            this.Initialize(Localizer.DoStr("Saw Boards"), typeof(SawBoardsRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SawBoardsRecipe), this.UILink(), 0.5f, typeof(LumberSkill));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}