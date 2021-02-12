using InTheDark.Model.Components;
using InTheDark.Model.Player;
using Leopotam.Ecs;
using Leopotam.Ecs.Types;

namespace InTheDark.Model.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly IPlayerPresenter _playerPresenter = null;

        //To Config...
        private readonly Int2 _initPlayerPosition = new Int2(15, 15);

        public void Init()
        {
            var entity = _world.NewEntity();
            ref var player = ref entity.Get<PlayerComponent>();
            player = new PlayerComponent { Position = _initPlayerPosition };
            _playerPresenter.Present(player);
        }
    }
}
