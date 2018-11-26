namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [RequiresSkill(typeof(MetalworkingSkill), 4)]   
    public partial class RebarRecipe : Recipe
    {
        public RebarRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RebarItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CoalItem>(typeof(MetalworkingEfficiencySkill), 2, MetalworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(MetalworkingEfficiencySkill), 5, MetalworkingEfficiencySkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(RebarRecipe), Item.Get<RebarItem>().UILink(), 0.5f, typeof(MetalworkingSpeedSkill));    
            this.Initialize(Localizer.Do("Rebar"), typeof(RebarRecipe));

            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }


    [Serialized]
    [Weight(3000)]      
    [Currency]                                              
    public partial class RebarItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Rebar"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Ribbed steel bars for reinforcing stuctures."); } }

    }

}