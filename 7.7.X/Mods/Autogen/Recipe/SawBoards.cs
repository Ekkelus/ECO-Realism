namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(LumberSkill), 3)] 
    public class SawBoardsRecipe : Recipe
    {
        public SawBoardsRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<BoardItem>(2),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(LumberSkill), 2, LumberSkill.MultiplicativeStrategy), 
            };
            Initialize(Localizer.DoStr("Saw Boards"), typeof(SawBoardsRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(SawBoardsRecipe), this.UILink(), 0.5f, typeof(LumberSkill));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}