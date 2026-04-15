using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Extensions;
using Godot;

namespace STS2_Mulundus.STS2_MulundusCode.Relics;

[Pool(typeof(HeartwoodRangerRelicPool))]
public abstract class STS2_MulundusRelic : CustomRelicModel
{
    public override string PackedIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".RelicImagePath();
            return ResourceLoader.Exists(path) ? path : "relic.png".RelicImagePath();
        }
    }

    protected override string PackedIconOutlinePath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}_outline.png".RelicImagePath();
            return ResourceLoader.Exists(path) ? path : "relic_outline.png".RelicImagePath();
        }
    }

    protected override string BigIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigRelicImagePath();
            return ResourceLoader.Exists(path) ? path : "relic.png".BigRelicImagePath();
        }
    }
}