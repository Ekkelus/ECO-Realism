using System;
using System.Collections.Generic;
using System.ComponentModel;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Minimap;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Gameplay.Pipes.LiquidComponents;
using Eco.Gameplay.Pipes.Gases;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
	using Eco.Shared.Localization;
using Eco.Shared.Utils;
using Eco.Shared.View;
using Eco.Shared.Items;
using Eco.Gameplay.Pipes;
using Eco.World.Blocks;
using REYmod.Utils;

namespace Eco.Gameplay.Components.Auth
{
    // New Authentication Component to also check for certain Skills besides the normal Rightsmanagement
    // Has to be Initialized with Initialize(AuthModeType defaultMode, Type RequiredSkillType, int RequiredSkillLevel)
    // Otherwise its just a normal PropertyAuthComponent

    // currently not working as an update changed how Authcomponents work, and i didn't update it here as its currently not used anyways
    // therefor commentetd out

    /*
    [Serialized]
    public class SkillAuthComponent : PropertyAuthComponent
    {
        private Type reqskilltype;
        private int reqskilllevel;


        public void Initialize(AuthModeType defaultMode, Type RequiredSkillType, int RequiredSkillLevel)
        {
            base.Initialize(defaultMode);
            reqskilltype = RequiredSkillType;
            reqskilllevel = RequiredSkillLevel;
        }

        public override bool IsAuthorized(User user)
        {
            if ((user == null) || (reqskilltype == null)) return base.IsAuthorized(user);
            if (base.IsAuthorized(user) && SkillUtils.UserHasSkill(user, reqskilltype, reqskilllevel))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    */
}
