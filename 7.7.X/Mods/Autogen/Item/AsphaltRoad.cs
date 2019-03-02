namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;
    using Eco.World;
    using Eco.World.Blocks;

    [RequiresSkill(typeof(BasicEngineeringSkill), 3)]   
    public partial class AsphaltRoadRecipe : Recipe
    {
        public AsphaltRoadRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<AsphaltRoadItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(typeof(BasicEngineeringSkill), 1, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<StoneRoadItem>(typeof(BasicEngineeringSkill), 1, BasicEngineeringSkill.MultiplicativeStrategy),
                new CraftingElement<PetroleumItem>(typeof(BasicEngineeringSkill), 1, BasicEngineeringSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(AsphaltRoadRecipe), Item.Get<AsphaltRoadItem>().UILink(), 1, typeof(BasicEngineeringSkill));    
            this.Initialize(Localizer.DoStr("Asphalt Road"), typeof(AsphaltRoadRecipe));

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