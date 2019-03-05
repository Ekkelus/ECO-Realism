namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Blocks;
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;
    using World;
    using World.Blocks;

    [RequiresSkill(typeof(BasicEngineeringSkill), 3)]   
    public partial class AsphaltRoadRecipe : Recipe
    {
        public AsphaltRoadRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<AsphaltRoadItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(typeof(BasicEngineeringSkill), 1, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<StoneRoadItem>(typeof(BasicEngineeringSkill), 1, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<PetroleumItem>(typeof(BasicEngineeringSkill), 1, BasicEngineeringSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(AsphaltRoadRecipe), Item.Get<AsphaltRoadItem>().UILink(), 1, typeof(BasicEngineeringSkill));    
            Initialize(Localizer.DoStr("Asphalt Road"), typeof(AsphaltRoadRecipe));

            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }
    }

    [Serialized]
    [Solid, Wall, Constructed]
    [Road(1)]                                          
    [UsesRamp(typeof(AsphaltRoadWorldObjectBlock))]              
    [RequiresSkill(typeof(BasicEngineeringSkill), 3)]   
    public partial class AsphaltRoadBlock :
        Block           
    { }

    [Serialized]
    [MaxStackSize(10)]                                      
    [Weight(10000)]      
    [MakesRoads]                                            
    public partial class AsphaltRoadItem :
    RoadItem<AsphaltRoadBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Asphalt Road"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A paved surface constructed with asphalt and concrete. It's durable and extremely efficient for any wheeled vehicle."); } }

    }

}