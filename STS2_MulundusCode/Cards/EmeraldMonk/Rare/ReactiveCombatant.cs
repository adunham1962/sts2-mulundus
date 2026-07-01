using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Rare;
[Pool(typeof(EmeraldMonkCardPool))]
public class ReactiveCombatant : EmeraldMonkCard
{

    public ReactiveCombatant() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithPower<ReactiveCombatantPower>(3);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.ApplySelf<ReactiveCombatantPower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["ReactiveCombatantPower"].UpgradeValueBy(2);
    }
}