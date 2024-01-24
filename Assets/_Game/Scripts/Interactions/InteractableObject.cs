using Unity.Netcode;

namespace ER
{
    public abstract class InteractableObject : NetworkBehaviour
    {
        public abstract void OnClick(InteractionContext context);
        public abstract void OnHover();
        public abstract void OnUnHover();
    }
}

