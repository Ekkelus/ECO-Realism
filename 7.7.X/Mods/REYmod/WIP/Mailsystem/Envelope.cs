namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Items;
    using Shared.Serialization;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Systems.Tooltip;

    [Serialized]
    [Weight(0)]
    public partial class EnvelopeItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Envelope"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("An envelope to put your notes in"); } }

    }

}