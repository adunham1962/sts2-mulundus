using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Uncommon;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class NecroticWavePower : TemporaryStrengthPower, ICustomPower
{
    public override AbstractModel OriginModel => ModelDb.Card<NecroticWave>();
}