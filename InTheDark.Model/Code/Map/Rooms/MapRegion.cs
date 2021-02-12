using InTheDark.Model.Components;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InTheDark.Model.Map
{
    internal class MapRegion
    {
        private readonly List<BackgroundTileComponent> _background = new List<BackgroundTileComponent>();
        private readonly List<ForegroundTileComponent> _foreground = new List<ForegroundTileComponent>();

        public ReadOnlyCollection<BackgroundTileComponent> Background => _background.AsReadOnly();
        public ReadOnlyCollection<ForegroundTileComponent> Foreground => _foreground.AsReadOnly();

    }
}
