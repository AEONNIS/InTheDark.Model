using InTheDark.Model.Components;
using InTheDark.Model.Map;
using Leopotam.Ecs;
using Leopotam.Ecs.Types;

namespace InTheDark.Model.Systems
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, MovementEvent> _playerMovementFilter = null;
        private readonly EcsFilter<ForegroundTileComponent> _foregroundTileFilter = null;

        public void Run()
        {
            foreach (var i in _playerMovementFilter)
            {
                ref var entity = ref _playerMovementFilter.GetEntity(i);
                ref var player = ref _playerMovementFilter.Get1(i);
                ref var movement = ref _playerMovementFilter.Get2(i);
                var endPosition = player.Position + movement.Value;

                if (IsMovementPossibleTo(endPosition))
                {
                    ref var movementPresentation = ref entity.Get<MovementPresentationEvent>();
                    movementPresentation = new MovementPresentationEvent { Movement = movement.Value };
                    player.Position = endPosition;
                }
            }
        }

        private bool IsMovementPossibleTo(in Int2 endPosition)
        {
            foreach (var i in _foregroundTileFilter)
            {
                ref var foregroundTile = ref _foregroundTileFilter.Get1(i);

                if (foregroundTile.Position == endPosition && TileIsNotPassable(foregroundTile))
                    return false;
            }

            return true;
        }

        private bool TileIsNotPassable(in ForegroundTileComponent foregroundTile)
            => foregroundTile.Id == ForegroundTileId.Wall || foregroundTile.Id == ForegroundTileId.Door;
    }
}
