namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Items;
    using Eco.Shared.Serialization;



    [Serialized]
    [Weight(1500)]
    [Currency]
    public partial class SkinnedElkItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinned Elk"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A skinned elk."); } }

    }

}