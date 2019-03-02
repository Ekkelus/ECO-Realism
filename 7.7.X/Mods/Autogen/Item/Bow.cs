namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(HuntingSkill), 0)]   
    public partial class BowRecipe : Recipe
    {
        public BowRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BowItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(HuntingSkill), 4, HuntingSkill.MultiplicativeStrategy),
                new CraftingElement<StringItem>(typeof(HuntingSkill), 3, HuntingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(5);
            this.Initialize(Localizer.DoStr("Bow"), typeof(BowRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


}