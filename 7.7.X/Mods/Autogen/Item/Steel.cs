namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    //[RequiresSkill(typeof(AlloySmeltingSkill), 2)]   
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
    //            new CraftingElement<CoalItem>(typeof(AlloySmeltingEfficiencySkill), 2, AlloySmeltingEfficiencySkill.MultiplicativeStrategy),
    //            new CraftingElement<IronIngotItem>(typeof(AlloySmeltingEfficiencySkill), 5, AlloySmeltingEfficiencySkill.MultiplicativeStrategy), 
    //        };
    //        this.CraftMinutes = CreateCraftTimeValue(typeof(SteelRecipe), Item.Get<SteelItem>().UILink(), 3, typeof(AlloySmeltingSpeedSkill));    
    //        this.Initialize(Localizer.Do("Steel"), typeof(SteelRecipe));

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