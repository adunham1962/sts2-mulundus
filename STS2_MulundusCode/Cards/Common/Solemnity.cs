using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Common;
[Pool(typeof(HeartwoodRangerCardPool))]
public class Solemnity : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/solemnity.png";
    public Solemnity() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithEnergy(2);
        WithPower<StrengthPower>(2);
        WithEnergyTip();
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PowerCmd.Apply<EnergyNextTurnPower>(Owner.Creature, DynamicVars.Energy.BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<SolemnityPower>(Owner.Creature, DynamicVars.Strength.BaseValue * -1, Owner.Creature, this);

        if (play.Card.CombatState != null)
        {
            var enemies = play.Card.CombatState.Enemies.ToArray();
            foreach (var enemy in enemies)
            {
                await PowerCmd.Apply<SolemnityPower>(enemy, DynamicVars.Strength.BaseValue * -1, Owner.Creature, this);
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Energy.UpgradeValueBy(1);
    }
}