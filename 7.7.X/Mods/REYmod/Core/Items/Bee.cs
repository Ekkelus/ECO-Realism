namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Items;
    using Eco.Shared.Serialization;



    [Serialized]
    [Weight(1)]
    [Currency]
    [Fuel(60)]
    public partial class BeeItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Bee"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Bzzz."); } }

    }

}