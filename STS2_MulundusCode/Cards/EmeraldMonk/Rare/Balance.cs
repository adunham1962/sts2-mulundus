using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Rare;

[Pool(typeof(EmeraldMonkCardPool))]
public class Balance : EmeraldMonkCard
{

    public Balance() : base(1, CardType.Status, CardRarity.Rare, TargetType.Self)
    {
        WithKeyword(CardKeyword.Ethereal);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Ethereal);
    }
}