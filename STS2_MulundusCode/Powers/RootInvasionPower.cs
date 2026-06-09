using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Common;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class RootInvasionPower : TemporaryStrengthPower, ICustomModel
{
    public override AbstractModel OriginModel => ModelDb.Card<RootInvasion>();
}