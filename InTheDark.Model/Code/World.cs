using InTheDark.Model.Components;
using InTheDark.Model.Systems;
using Leopotam.Ecs;

namespace InTheDark.Model
{
    public class World
    {
        private EcsWorld _ecsWorld = new EcsWorld();
        private EcsSystems _systems;

        public World()
        {
            _systems = new EcsSystems(_ecsWorld);
            AddAllSystems();
            RegistersAllOneFrameComponents();
        }

        public void Send<T>(in T eventComponent) where T : struct
        {
            var entity = _ecsWorld.NewEntity();
            ref var newEventComponent = ref entity.Get<T>();
            newEventComponent = eventComponent;
        }

        public void Inject<T>(T instance) => _systems.Inject(instance);

        public void Init() => _systems.Init();

        public void Update() => _systems.Run();

        public void Destroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _ecsWorld.Destroy();
                _systems = null;
                _ecsWorld = null;
            }
        }

        private EcsSystems AddAllSystems() => _systems
            .Add(new MapGenerationSystem())
            .Add(new PlayerInitSystem())
            .Add(new InputProcessingSystem())
            .Add(new PlayerMovementSystem())
            .Add(new MapPresentationSystem())
            .Add(new PlayerMovementPresentationSystem());

        private EcsSystems RegistersAllOneFrameComponents() => _systems
            .OneFrame<MapRegionCreationEvent>()
            .OneFrame<PlayerMoveInputEvent>()
            .OneFrame<MovementEvent>()
            .OneFrame<PresentationEvent>()
            .OneFrame<MovementPresentationEvent>();
    }
}
