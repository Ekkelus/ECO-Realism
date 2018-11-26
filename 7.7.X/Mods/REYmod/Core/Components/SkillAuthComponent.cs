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
