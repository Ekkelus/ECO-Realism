namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;

    [RequiresSkill(typeof(OilDrillingSkill), 3)]   
    public partial class PlasticRecipe : Recipe
    {
        public PlasticRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<PlasticItem>(),
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<PetroleumItem>(typeof(OilDrillingSkill), 5, OilDrillingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(PlasticRecipe), Item.Get<PlasticItem>().UILink(), 2, typeof(OilDrillingSkill), typeof(OilDrillingFocusedSpeedTalent));    
            Initialize(Localizer.DoStr("Plastic"), typeof(PlasticRecipe));

            CraftingComponent.AddRecipe(typeof(OilRefineryObject), this);
        }
    }


    [Serialized]
    [Weight(1000)]      
    [Currency]              
    public partial class PlasticItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Plastic"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Plastic"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("An extremely useful synthetic material derived from petrochemicals"); } }

    }

}