using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Shared.Localization;

/* CURRENTLY DISABLED
namespace Eco.Mods.TechTree
{
    [RequiresSkill(typeof(LumberSkill), 10)]
    class HewnLumberRecipe : Recipe
    {
        public HewnLumberRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<LumberItem>(),
               new CraftingElement<WoodPulpItem>(3),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(LumberSkill), 10f, LumberSkill.MultiplicativeStrategy),
            };
            this.Initialize(Localizer.DoStr("Upgrade Hewn Logs"), typeof(HewnLumberRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(HewnLumberSkill));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}
*/