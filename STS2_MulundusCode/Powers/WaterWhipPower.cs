using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Basic;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class WaterWhipPower : TemporaryDexterityPower, ICustomPower
{
    public override AbstractModel OriginModel => ModelDb.Card<WaterWhip>();
}