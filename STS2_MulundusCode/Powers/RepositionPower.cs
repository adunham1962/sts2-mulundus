using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Cards.Common;

namespace STS2_Mulundus.STS2_MulundusCode.Powers;

public class RepositionPower : TemporaryDexterityPower, ICustomModel
{
    public override AbstractModel OriginModel => (AbstractModel) ModelDb.Card<Reposition>();
}