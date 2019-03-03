namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class ContractBoardObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Contract Board"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ContractBoardItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Economy");                                 


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1500)]
    public partial class ContractBoardItem : WorldObjectItem<ContractBoardObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Contract Board"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A board to post contracts."); } }

        static ContractBoardItem()
        {
            
        }
        
    }


    [RequiresSkill(typeof(HewingSkill), 1)]
    public partial class ContractBoardRecipe : Recipe
    {
        public ContractBoardRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ContractBoardItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<PaperItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                //new CraftingElement<NailsItem>(typeof(HewingSkill), 20, HewingSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ContractBoardRecipe), Item.Get<ContractBoardItem>().UILink(), 15, typeof(HewingSkill));
            this.Initialize(Localizer.DoStr("Contract Board"), typeof(ContractBoardRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}