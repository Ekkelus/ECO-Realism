namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;

    [RequiresSkill(typeof(CementSkill), 1)]   
    public partial class ConcreteRecipe : Recipe
    {
        public ConcreteRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(CementSkill), 10, CementSkill.MultiplicativeStrategy),
                new CraftingElement<SandItem>(typeof(CementSkill), 10, CementSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(ConcreteRecipe), Item.Get<ConcreteItem>().UILink(), 2, typeof(CementSkill), typeof(CementFocusedSpeedTalent));    
            Initialize(Localizer.DoStr("Concrete"), typeof(ConcreteRecipe));

            CraftingComponent.AddRecipe(typeof(CementKilnObject), this);
        }
    }


    [Serialized]
    [Weight(10000)]      
    [Currency]              
    public partial class ConcreteItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Concrete"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Concrete"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A very strong building material made from cement and an aggregate like crushed stone."); } }

    }

}