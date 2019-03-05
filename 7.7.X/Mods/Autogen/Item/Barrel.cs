namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;
    using World.Blocks;

    [RequiresSkill(typeof(OilDrillingSkill), 0)]   
    public partial class BarrelRecipe : Recipe
    {
        public BarrelRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<BarrelItem>(),          
            };
            Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(OilDrillingSkill), 4, OilDrillingSkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(OilDrillingSkill), 4, OilDrillingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(BarrelRecipe), Item.Get<BarrelItem>().UILink(), 1, typeof(OilDrillingSkill));    
            Initialize(Localizer.DoStr("Barrel"), typeof(BarrelRecipe));

            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }

    [Serialized]
    [Solid]
    [RequiresSkill(typeof(OilDrillingSkill), 0)]   
    public partial class BarrelBlock :
        PickupableBlock     
    { }

    [Serialized]
    [MaxStackSize(10)]                                       
    [Weight(2000)]      
    [Currency]              
    public partial class BarrelItem :
    BlockItem<BarrelBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Barrel"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Expertly crafted from smoothed boards and metal bands, this can carry a variety of substances."); } }

    }

}