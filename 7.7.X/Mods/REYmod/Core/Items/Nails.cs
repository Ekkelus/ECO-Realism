namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;

    [RequiresSkill(typeof(MetalworkingSkill), 1)]   
    public partial class NailsRecipe : Recipe
    {
        public NailsRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<NailsItem>(5),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(MetalworkingEfficiencySkill), 2, MetalworkingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(NailsRecipe), Item.Get<NailsItem>().UILink(), 4, typeof(MetalworkingSpeedSkill));    
            this.Initialize(Localizer.DoStr("Nails"), typeof(NailsRecipe));

            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }


    [Serialized]
    [Weight(20)]      
    [Currency]              
    public partial class NailsItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Nails"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Hold wooden constructions together."); } }

    }

}