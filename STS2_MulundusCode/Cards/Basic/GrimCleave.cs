using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Basic;

[Pool(typeof(HeartwoodRangerCardPool))]
public class GrimCleave() : HeartWoodRangerCard(1, CardType.Attack, CardRarity.Basic, TargetType.AllEnemies)
{
    protected new IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return [
                new CalculationBaseVar(4M),
                new ExtraDamageVar(2M),
                new CalculatedDamageVar(ValueProp.Move).WithMultiplier(((card, _) => PileType.Exhaust.GetPile(card.Owner).Cards.Count))
            ];
        }
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await DamageCmd.Attack(this.DynamicVars.CalculatedDamage).FromCard(this).WithHitFx("vfx/vfx_attack_slash", tmpSfx: "blunt_attack.mp3").Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {

    }
}