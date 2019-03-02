namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [RequiresSkill(typeof(SmeltingSkill), 4)]   
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
                new CraftingElement<CoalItem>(typeof(SmeltingSkill), 2, SmeltingSkill.MultiplicativeStrategy),
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 5, SmeltingSkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(RebarRecipe), Item.Get<RebarItem>().UILink(), 0.5f, typeof(SmeltingSkill));    
            this.Initialize(Localizer.DoStr("Rebar"), typeof(RebarRecipe));

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