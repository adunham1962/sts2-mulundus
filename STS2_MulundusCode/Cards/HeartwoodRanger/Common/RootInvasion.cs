using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;
using ConstrictPower = STS2_Mulundus.STS2_MulundusCode.Powers.ConstrictPower;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Common;

[Pool(typeof(HeartwoodRangerCardPool))]
public class RootInvasion : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/root_invasion.png";
    public RootInvasion() : base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(4);
        WithPower<ConstrictPower>(2);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        if (play.Target != null) await CommonActions.Apply<ConstrictPower>(choiceContext, play.Target, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["ConstrictPower"].UpgradeValueBy(2);
    }
}