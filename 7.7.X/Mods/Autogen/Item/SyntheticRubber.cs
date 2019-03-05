namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;

    [RequiresSkill(typeof(OilDrillingSkill), 3)]   
    public partial class SyntheticRubberRecipe : Recipe
    {
        public SyntheticRubberRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<SyntheticRubberItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<PetroleumItem>(typeof(OilDrillingSkill), 5, OilDrillingSkill.MultiplicativeStrategy), 
            };
            CraftMinutes = CreateCraftTimeValue(typeof(SyntheticRubberRecipe), Item.Get<SyntheticRubberItem>().UILink(), 2, typeof(OilDrillingSkill));    
            Initialize(Localizer.DoStr("Synthetic Rubber"), typeof(SyntheticRubberRecipe));

            CraftingComponent.AddRecipe(typeof(OilRefineryObject), this);
        }
    }


    [Serialized]
    [Weight(1000)]      
    [Currency]              
    public partial class SyntheticRubberItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Synthetic Rubber"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Rubber"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("An extremely useful synthetic material derived from petrochemicals"); } }

    }

}