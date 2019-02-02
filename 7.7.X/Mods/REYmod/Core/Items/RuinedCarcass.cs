namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Items;
    using Eco.Shared.Serialization;



    [Serialized]
    [Weight(1000)]
    public partial class RuinedCarcassItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Ruined Carcass"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("This has probably no use anymore."); } }
    }

}