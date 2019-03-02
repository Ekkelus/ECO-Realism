namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.World;
    using Eco.World.Blocks;

    [RequiresSkill(typeof(BasicEngineeringSkill), 1)]   
    public partial class StoneRampRecipe : Recipe
    {
        public StoneRampRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StoneRampItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneRoadItem>(4),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(StoneRampRecipe), Item.Get<StoneRampItem>().UILink(), 1, typeof(BasicEngineeringSkill));    
            this.Initialize(Localizer.DoStr("Stone Ramp"), typeof(StoneRampRecipe));

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