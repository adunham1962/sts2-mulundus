using BaseLib.Abstracts;
using BaseLib.Extensions;
using STS2_Mulundus.STS2_MulundusCode.Extensions;
using Godot;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public abstract class STS2_MulundusPower : CustomPowerModel
{
    //Loads from STS2_Mulundus/images/powers/your_power.png
    public override string CustomPackedIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".PowerImagePath();
        }
    }

    public override string CustomBigIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".BigPowerImagePath();
        }
    }
}