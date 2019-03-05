namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;

    [RequiresSkill(typeof(HuntingSkill), 0)]   
    public partial class BowRecipe : Recipe
    {
        public BowRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<BowItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(HuntingSkill), 4, HuntingSkill.MultiplicativeStrategy),
                new CraftingElement<StringItem>(typeof(HuntingSkill), 3, HuntingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = new ConstantValue(5);
            Initialize(Localizer.DoStr("Bow"), typeof(BowRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


}