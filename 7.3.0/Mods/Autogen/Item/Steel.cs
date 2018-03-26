namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Shared.Localization;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    [RequiresSkill(typeof(AlloySmeltingSkill), 2)]   
    public partial class SteelRecipe : Recipe
    {
        public SteelRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(AlloySmeltingEfficiencySkill), 3, AlloySmeltingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<CharcoalItem>(typeof(AlloySmeltingEfficiencySkill), 2, AlloySmeltingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<TailingsItem>(typeof(AlloySmeltingEfficiencySkill), 1, AlloySmeltingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(SteelRecipe), Item.Get<SteelItem>().UILink(), 5, typeof(AlloySmeltingSpeedSkill));    
            this.Initialize("Steel", typeof(SteelRecipe));

            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }


    [Serialized]
    [Weight(4000)]      
    [Currency]              
    public partial class SteelItem :
    Item                                     
    {
        public override string FriendlyName { get { return "Steel"; } }
        public override string Description { get { return "A strong alloy of iron and other elements."; } }

    }

}