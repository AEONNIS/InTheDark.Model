using InTheDark.Model.Components;
using InTheDark.Model.Map;
using Leopotam.Ecs;
using Leopotam.Ecs.Types;

namespace InTheDark.Model.Systems
{
    public class MapGenerationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<MapRegionCreationEvent> _mapRegionCreationFilter = null;

        // To Config...
        private readonly Int2 _initOffsetOnMap = new Int2(0, 0);
        private readonly Int2 _mapRegionSize = new Int2(32, 32);
        private readonly int _splitsNumber = 15;
        private readonly float _limitingWallSplitting = 0.2f;

        public void Init()
        {
            var initMapRegionCreation = new MapRegionCreationEvent { Size = _mapRegionSize, OffsetOnMap = _initOffsetOnMap };
            CreateInitialMapRegion(initMapRegionCreation);
        }

        public void Run()
        {
            foreach (int i in _mapRegionCreationFilter)
            {
                ref var entity = ref _mapRegionCreationFilter.GetEntity(i);
                ref var mapRegionCreation = ref _mapRegionCreationFilter.Get1(i);
                CreateFloor(mapRegionCreation);
                CreateExternalWalls(mapRegionCreation);
            }
        }

        public void CreateInitialMapRegion(in MapRegionCreationEvent initMapRegionCreation)
        {
            var entity = _world.NewEntity();
            ref var mapRegionCreation = ref entity.Get<MapRegionCreationEvent>();
            mapRegionCreation = initMapRegionCreation;
        }

        private void CreateFloor(in MapRegionCreationEvent mapRegionCreation)
        {
            for (int x = 0; x < mapRegionCreation.Size.X; x++)
            {
                for (int y = 0; y < mapRegionCreation.Size.Y; y++)
                {
                    var position = new Int2(x, y) + mapRegionCreation.OffsetOnMap;
                    CreateFloorTile(position);
                }
            }
        }

        private void CreateFloorTile(in Int2 position)
        {
            var entity = _world.NewEntity();
            ref var backgroundTile = ref entity.Get<BackgroundTileComponent>();
            backgroundTile = new BackgroundTileComponent { Position = position, Id = BackgroundTileId.Floor };
            entity.Get<PresentationEvent>();
        }

        private void CreateExternalWalls(in MapRegionCreationEvent mapRegionCreation)
        {
            CreateSouthAndNorthWalls(mapRegionCreation);
            CreateWestAndEastWalls(mapRegionCreation);
        }

        private void CreateSouthAndNorthWalls(in MapRegionCreationEvent mapRegionCreation)
        {
            for (int x = 0; x < mapRegionCreation.Size.X; x++)
            {
                var southPosition = new Int2(x, 0) + mapRegionCreation.OffsetOnMap;
                var northPosition = new Int2(x, mapRegionCreation.Size.Y - 1) + mapRegionCreation.OffsetOnMap;
                CreateWallTile(southPosition);
                CreateWallTile(northPosition);
            }
        }

        private void CreateWestAndEastWalls(in MapRegionCreationEvent mapRegionCreation)
        {
            for (int y = 1; y < mapRegionCreation.Size.Y - 1; y++)
            {
                var westPosition = new Int2(0, y) + mapRegionCreation.OffsetOnMap;
                var eastPosition = new Int2(mapRegionCreation.Size.X - 1, y) + mapRegionCreation.OffsetOnMap;
                CreateWallTile(westPosition);
                CreateWallTile(eastPosition);
            }
        }

        private void CreateWallTile(in Int2 position)
        {
            var entity = _world.NewEntity();
            ref var foregroundTile = ref entity.Get<ForegroundTileComponent>();
            foregroundTile = new ForegroundTileComponent { Position = position, Id = ForegroundTileId.Wall };
            entity.Get<PresentationEvent>();
        }
    }
}
