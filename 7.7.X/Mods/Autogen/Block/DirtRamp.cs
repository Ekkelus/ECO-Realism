namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;
    using World;
    using World.Blocks;

    [RequiresSkill(typeof(BasicEngineeringSkill), 0)]   
    public partial class DirtRampRecipe : Recipe
    {
        public DirtRampRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<DirtRampItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<DirtItem>(4), 
            };
            CraftMinutes = CreateCraftTimeValue(typeof(DirtRampRecipe), Item.Get<DirtRampItem>().UILink(), 0.5f, typeof(BasicEngineeringSkill), typeof(BasicEngineeringFocusedSpeedTalent));    
            Initialize(Localizer.DoStr("Dirt Ramp"), typeof(DirtRampRecipe));

            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }

    [Serialized]
    [Constructed]
    [Road(1)]                                          
    [RequiresSkill(typeof(BasicEngineeringSkill), 0)]   
    public partial class DirtRampBlock :
        Block            
    {
    }

}