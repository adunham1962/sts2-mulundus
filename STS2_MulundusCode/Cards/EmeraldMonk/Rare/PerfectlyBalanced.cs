using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Rare;
[Pool(typeof(EmeraldMonkCardPool))]
public class PerfectlyBalanced : EmeraldMonkCard
{
    public PerfectlyBalanced() : base(2, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithPower<PerfectlyBalancedPower>(1);
        WithCards(2);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<PerfectlyBalancedPower>(choiceContext, this);
        await CommonActions.Draw(this, choiceContext);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}