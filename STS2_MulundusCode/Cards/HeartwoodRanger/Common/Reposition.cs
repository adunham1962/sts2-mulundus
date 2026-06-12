using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Common;
[Pool(typeof(HeartwoodRangerCardPool))]
public class Reposition : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/reposition.png";
    public Reposition() : base(0, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithPower<DexterityPower>(2);
        WithPower<VulnerablePower>(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<RepositionPower>(Owner.Creature, DynamicVars.Dexterity.BaseValue, Owner.Creature, this);
        if (play.Target != null) await CommonActions.Apply<VulnerablePower>(choiceContext, play.Target, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["VulnerablePower"].UpgradeValueBy(1);
        DynamicVars.Dexterity.UpgradeValueBy(1);
    }
}