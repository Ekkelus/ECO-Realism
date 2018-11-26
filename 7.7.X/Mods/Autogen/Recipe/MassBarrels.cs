namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(IndustrialEngineeringSkill), 3)] 
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
                new CraftingElement<SteelItem>(typeof(IndustrialEngineeringEfficiencySkill), 4, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(IndustrialEngineeringEfficiencySkill), 8, IndustrialEngineeringEfficiencySkill.MultiplicativeStrategy),
            };
            this.Initialize(Localizer.DoStr("Mass Barrels"), typeof(MassBarrelsRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(MassBarrelsRecipe), this.UILink(), 5, typeof(IndustrialEngineeringSpeedSkill));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }
}