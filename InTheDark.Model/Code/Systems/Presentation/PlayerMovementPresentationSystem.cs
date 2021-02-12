using InTheDark.Model.Components;
using InTheDark.Model.Player;
using Leopotam.Ecs;

namespace InTheDark.Model.Systems
{
    public class PlayerMovementPresentationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, MovementPresentationEvent> _playerMovementPresentationFilter = null;
        private readonly IPlayerMovementPresenter _playerMovementPresenter = null;

        public void Run()
        {
            foreach (var i in _playerMovementPresentationFilter)
            {
                ref var movementPresentation = ref _playerMovementPresentationFilter.Get2(i);
                _playerMovementPresenter.Present(movementPresentation);
            }
        }
    }
}
