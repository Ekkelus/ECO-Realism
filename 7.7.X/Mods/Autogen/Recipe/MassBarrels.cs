namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(IndustrySkill), 3)] 
    public class MassBarrelsRecipe : Recipe
    {
        public MassBarrelsRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<BarrelItem>(5),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 4, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(IndustrySkill), 8, IndustrySkill.MultiplicativeStrategy),
            };
            this.Initialize(Localizer.DoStr("Mass Barrels"), typeof(MassBarrelsRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(MassBarrelsRecipe), this.UILink(), 5, typeof(IndustrySkill));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }
}