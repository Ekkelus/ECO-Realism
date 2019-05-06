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
	using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    [RequiresSkill(typeof(LumberSkill), 1)]   
    public partial class CharcoalRecipe : Recipe
    {
        public CharcoalRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CharcoalItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LumberItem>(typeof(LumberProcessingEfficiencySkill), 2, LumberProcessingEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CharcoalRecipe), Item.Get<CharcoalItem>().UILink(), 1, typeof(LumberProcessingSpeedSkill));    
            this.Initialize("Charcoal", typeof(CharcoalRecipe));

            CraftingComponent.AddRecipe(typeof(BloomeryObject), this);
        }
    }


    [Serialized]
    [Weight(1000)]      
    [Fuel(25000)]          
    [Currency]              
    public partial class CharcoalItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Charcoal"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A black residue, consisting of carbon and any remaining ash."); } }

    }

}