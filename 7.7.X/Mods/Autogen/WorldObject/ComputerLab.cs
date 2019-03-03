namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;


    [Serialized]
    [Weight(5000)]
    public partial class ComputerLabItem : WorldObjectItem<ComputerLabObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Computer Lab"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A place where you can sit all day and play video games! Or work on expert-level research."); } }

        static ComputerLabItem()
        {
            
        }
        
    }


    [RequiresSkill(typeof(IndustrySkill), 4)]
    public partial class ComputerLabRecipe : Recipe
    {
        public ComputerLabRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ComputerLabItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 50, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(IndustrySkill), 30, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<ConcreteItem>(typeof(IndustrySkill), 40, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<PaperItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(IndustrySkill), 10, IndustrySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ComputerLabRecipe), Item.Get<ComputerLabItem>().UILink(), 260, typeof(ElectronicsSkill));
            this.Initialize(Localizer.DoStr("Computer Lab"), typeof(ComputerLabRecipe));
            CraftingComponent.AddRecipe(typeof(ElectronicsAssemblyObject), this);
        }
    }
}