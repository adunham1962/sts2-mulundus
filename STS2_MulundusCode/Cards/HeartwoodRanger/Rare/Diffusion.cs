using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;

[Pool(typeof(HeartwoodRangerCardPool))]
public class Diffusion() : HeartWoodRangerCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        var poison = Owner.Creature.GetPowerAmount<PoisonPower>();
        await PowerCmd.Remove<PoisonPower>(Owner.Creature);
        if (CombatState is null) return;
        foreach (var enemy in CombatState.HittableEnemies)
        {
            await PowerCmd.Apply<PoisonPower>(enemy, poison, Owner.Creature, this);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}