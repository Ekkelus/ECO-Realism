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

    [RequiresSkill(typeof(BasicEngineeringSkill), 1)]   
    public partial class StoneRampRecipe : Recipe
    {
        public StoneRampRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<StoneRampItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneRoadItem>(4),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(StoneRampRecipe), Item.Get<StoneRampItem>().UILink(), 1, typeof(BasicEngineeringSkill), typeof(BasicEngineeringFocusedSpeedTalent));    
            Initialize(Localizer.DoStr("Stone Ramp"), typeof(StoneRampRecipe));

            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }

    [Serialized]
    [Constructed]
    [Road(1)]                                          
    [RequiresSkill(typeof(BasicEngineeringSkill), 1)]   
    public partial class StoneRampBlock :
        Block            
    {
    }

}