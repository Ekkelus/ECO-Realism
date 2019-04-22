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

    [RequiresSkill(typeof(BasicEngineeringSkill), 3)]   
    public partial class AsphaltRampRecipe : Recipe
    {
        public AsphaltRampRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<AsphaltRampItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<AsphaltRoadItem>(4),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(AsphaltRampRecipe), Item.Get<AsphaltRampItem>().UILink(), 5, typeof(BasicEngineeringSkill), typeof(BasicEngineeringFocusedSpeedTalent));    
            Initialize(Localizer.DoStr("Asphalt Ramp"), typeof(AsphaltRampRecipe));

            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }
    }

    [Serialized]
    [Constructed]
    [Road(1)]                                          
    [RequiresSkill(typeof(BasicEngineeringSkill), 3)]   
    public partial class AsphaltRampBlock :
        Block           
    { }

}