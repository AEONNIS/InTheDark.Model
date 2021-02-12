using InTheDark.Model.Components;
using Leopotam.Ecs;

namespace InTheDark.Model.Systems
{
    public class InputProcessingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerMoveInputEvent> _playerMoveInputFilter = null;
        private readonly EcsFilter<PlayerComponent> _playerFilter = null;

        public void Run()
        {
            foreach (var pmi in _playerMoveInputFilter)
            {
                foreach (var p in _playerFilter)
                {
                    ref var playerMoveInput = ref _playerMoveInputFilter.Get1(pmi);
                    ref var entity = ref _playerFilter.GetEntity(p);
                    ref var movement = ref entity.Get<MovementEvent>();
                    movement = new MovementEvent { Value = playerMoveInput.Direction };
                }
            }
        }
    }
}
