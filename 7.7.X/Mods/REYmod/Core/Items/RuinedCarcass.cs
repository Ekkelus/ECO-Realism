namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Items;
    using Shared.Serialization;



    [Serialized]
    [Weight(1000)]
    public partial class RuinedCarcassItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Ruined Carcass"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("This has probably no use anymore."); } }
    }

}