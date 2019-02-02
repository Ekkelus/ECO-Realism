namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;



    [Serialized]
    [Weight(10)]
    [Fuel(50)]
    [Currency]              
    public partial class WoodPulpItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wood Pulp"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Wood Pulp"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A byproduct of processing lumber, wood pulp can be burned for pitch, pressed into paper or used as fuel."); } }

    }

}