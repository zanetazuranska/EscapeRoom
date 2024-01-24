
namespace ER
{
    public struct InteractionContext
    {
        public InteractionManager interactionManager;
        public PlayerController playerController;
        public bool canTriggerOnClick;

        public InteractionContext(PlayerController playerController, bool canTriggerOnClick, InteractionManager interactionManager)
        {
            this.playerController = playerController;
            this.canTriggerOnClick = canTriggerOnClick;
            this.interactionManager = interactionManager;
        }
    }
}
