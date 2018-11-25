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
            this.Initialize("Nails", typeof(NailsRecipe));

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