using InTheDark.Model.Components;

namespace InTheDark.Model.Player
{
    public interface IPlayerMovementPresenter
    {
        public void Present(in MovementPresentationEvent movementPresentation);
    }
}
