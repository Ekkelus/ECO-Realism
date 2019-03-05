namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Components;
    using Gameplay.DynamicValues;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Shared.Serialization;

    [RequiresSkill(typeof(BasicEngineeringSkill), 1)]   
    public partial class RoadToolRecipe : Recipe
    {
        public RoadToolRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<RoadToolItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(BasicEngineeringSkill), 6, BasicEngineeringSkill.MultiplicativeStrategy),
            };
            CraftMinutes = new ConstantValue(5);
            Initialize(Localizer.DoStr("Road Tool"), typeof(RoadToolRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }


    [Serialized]
    [Weight(1000)]      
    [Currency]              
    public partial class RoadToolItem :
    ToolItem                        
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Road Tool"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Used to press roads into dirt and stone rubble."); } }

    }

}