namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;

    [RequiresSkill(typeof(PaperSkill), 1)]   
    public partial class PaperRecipe : Recipe
    {
        public PaperRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PaperItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WoodPulpItem>(typeof(PaperEfficiencySkill), 10, PaperEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(PaperRecipe), Item.Get<PaperItem>().UILink(), 0.25f, typeof(PaperSpeedSkill));    
            this.Initialize(Localizer.DoStr("Paper"), typeof(PaperRecipe));

            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }


    [Serialized]
    [Weight(100)]      
    [Currency]              
    public partial class PaperItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Paper"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("It's paper."); } }

    }

}