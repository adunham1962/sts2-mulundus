using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class CloakOfWilting : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/cloak_of_wilting.png";
    public CloakOfWilting() : base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<StrengthPower>(1);
        WithPower<CloakOfWiltingPower>(1);
        WithEnergy(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<CloakOfWiltingPower>(choiceContext, this);
        await PowerCmd.Apply<StrengthPower>(Owner.Creature, DynamicVars["StrengthPower"].BaseValue * -1, Owner.Creature, this);
        if (CombatState is not null)
        {
            foreach (var combatStateEnemy in CombatState.Enemies)
            {
                await PowerCmd.Apply<StrengthPower>(combatStateEnemy, DynamicVars["StrengthPower"].BaseValue * -1, Owner.Creature, this);
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["StrengthPower"].UpgradeValueBy(1);
    }
}