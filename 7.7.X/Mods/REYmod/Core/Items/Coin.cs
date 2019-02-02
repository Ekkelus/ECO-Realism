namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Items;
    using Eco.Shared.Serialization;



    [Serialized]
    [Weight(0)]      
    [Currency]              
    public partial class CoinItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Coin"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Print some money at the local Mint."); } }

    }

}