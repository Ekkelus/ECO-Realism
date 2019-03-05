namespace Eco.Mods.TechTree
{
    using Gameplay.Items;
    using Shared.Localization;
    using Shared.Serialization;

    //[RequiresSkill(typeof(AdvancedSmeltingSkill), 2)]   
    //public partial class SteelRecipe : Recipe
    //{
    //    public SteelRecipe()
    //    {
    //        this.Products = new CraftingElement[]
    //        {
    //            new CraftingElement<SteelItem>(),          
    //        };
    //        this.Ingredients = new CraftingElement[]
    //        {
    //            new CraftingElement<CoalItem>(typeof(AdvancedSmeltingSkill), 2, AdvancedSmeltingSkill.MultiplicativeStrategy),
    //            new CraftingElement<IronIngotItem>(typeof(AdvancedSmeltingSkill), 5, AdvancedSmeltingSkill.MultiplicativeStrategy), 
    //        };
    //        this.CraftMinutes = CreateCraftTimeValue(typeof(SteelRecipe), Item.Get<SteelItem>().UILink(), 3, typeof(AdvancedSmeltingSkill));    
    //        this.Initialize(Localizer.DoStr("Steel"), typeof(SteelRecipe));

    //        CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
    //    }
    //}


    [Serialized]
    [Weight(2500)]      
    [Currency]              
    public partial class SteelItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Steel"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A strong alloy of iron and other elements."); } }

    }

}