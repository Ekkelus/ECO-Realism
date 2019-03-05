namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(IndustrySkill), 3)] 
    public class MassBarrelsRecipe : Recipe
    {
        public MassBarrelsRecipe()
        {
            Products = new CraftingElement[]
            {
               new CraftingElement<BarrelItem>(5),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 4, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(IndustrySkill), 8, IndustrySkill.MultiplicativeStrategy),
            };
            Initialize(Localizer.DoStr("Mass Barrels"), typeof(MassBarrelsRecipe));
            CraftMinutes = CreateCraftTimeValue(typeof(MassBarrelsRecipe), this.UILink(), 5, typeof(IndustrySkill));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }
}