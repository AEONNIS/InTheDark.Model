using InTheDark.Model.Components;

namespace InTheDark.Model.Map
{
    public interface IMapPresenter
    {
        public void Present(in BackgroundTileComponent backgroundTile);

        public void Present(in ForegroundTileComponent foregroundTile);
    }
}
