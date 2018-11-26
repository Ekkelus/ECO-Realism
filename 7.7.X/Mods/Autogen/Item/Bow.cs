namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;

    [RequiresSkill(typeof(BowEfficiencySkill), 0)]   
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
                new CraftingElement<BoardItem>(typeof(BowEfficiencySkill), 4, BowEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<StringItem>(typeof(BowEfficiencySkill), 3, BowEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(5);
            this.Initialize("Bow", typeof(BowRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


}