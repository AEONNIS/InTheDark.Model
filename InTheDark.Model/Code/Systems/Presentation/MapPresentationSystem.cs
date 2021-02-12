using InTheDark.Model.Components;
using InTheDark.Model.Map;
using Leopotam.Ecs;

namespace InTheDark.Model.Systems
{
    public class MapPresentationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BackgroundTileComponent, PresentationEvent> _backgroundTilePresentationFilter = null;
        private readonly EcsFilter<ForegroundTileComponent, PresentationEvent> _foregroundTilePresentationFilter = null;
        private readonly IMapPresenter _mapPresenter = null;

        public void Run()
        {
            PresentBackground();
            PresentForeground();
        }

        private void PresentBackground()
        {
            foreach (var i in _backgroundTilePresentationFilter)
            {
                ref var backgroundTile = ref _backgroundTilePresentationFilter.Get1(i);
                _mapPresenter.Present(backgroundTile);
            }
        }

        private void PresentForeground()
        {
            foreach (var i in _foregroundTilePresentationFilter)
            {
                ref var foregroundTile = ref _foregroundTilePresentationFilter.Get1(i);
                _mapPresenter.Present(foregroundTile);
            }
        }
    }
}
