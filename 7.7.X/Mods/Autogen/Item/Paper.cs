namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;

    [RequiresSkill(typeof(PaperMillingSkill), 1)]   
    public partial class PaperRecipe : Recipe
    {
        public PaperRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<PaperItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<WoodPulpItem>(typeof(PaperMillingSkill), 10, PaperMillingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(PaperRecipe), Item.Get<PaperItem>().UILink(), 0.25f, typeof(PaperMillingSkill), typeof(PaperMillingFocusedSpeedTalent));    
            Initialize(Localizer.DoStr("Paper"), typeof(PaperRecipe));

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