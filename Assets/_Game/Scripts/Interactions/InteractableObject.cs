using Unity.Netcode;

public abstract class InteractableObject : NetworkBehaviour
{
    public abstract void OnClick();
    public abstract void OnHover();
    public abstract void OnUnHover();
}
