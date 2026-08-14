using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;

[Pool(typeof(EmeraldMonkCardPool))]
public class UnarmoredDefense : EmeraldMonkCard
{
    public UnarmoredDefense() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<UnarmoredDefensePower>(4);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<UnarmoredDefensePower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}