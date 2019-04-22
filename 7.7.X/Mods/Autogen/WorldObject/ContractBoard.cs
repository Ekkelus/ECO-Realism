namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Components;
    using Gameplay.Components.Auth;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Localization;
    using Shared.Serialization;

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
            GetComponent<MinimapComponent>().Initialize("Economy");                                 


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
    }


    [RequiresSkill(typeof(HewingSkill), 1)]
    public partial class ContractBoardRecipe : Recipe
    {
        public ContractBoardRecipe()
        {
            Products = new CraftingElement[]
            {
                new CraftingElement<ContractBoardItem>(),
            };

            Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                new CraftingElement<PaperItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy),
                //new CraftingElement<NailsItem>(typeof(HewingSkill), 20, HewingSkill.MultiplicativeStrategy),
            };
            CraftMinutes = CreateCraftTimeValue(typeof(ContractBoardRecipe), Item.Get<ContractBoardItem>().UILink(), 15, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent));
            Initialize(Localizer.DoStr("Contract Board"), typeof(ContractBoardRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}