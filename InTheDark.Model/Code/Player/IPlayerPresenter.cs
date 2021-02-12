using InTheDark.Model.Components;

namespace InTheDark.Model.Player
{
    public interface IPlayerPresenter
    {
        public void Present(in PlayerComponent player);
    }
}
