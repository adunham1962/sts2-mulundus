using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Uncommon;

[Pool(typeof(HeartwoodRangerCardPool))]
public class ManifestDread : HeartWoodRangerCard
{
    public ManifestDread() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        WithPower<VulnerablePower>(1);
        WithPower<WeakPower>(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var drawPile = CardPile.Get(PileType.Draw, Owner);
        if (drawPile is not null && !drawPile.IsEmpty)
        {
            var topCard = drawPile.Cards[0];
            await CardCmd.Exhaust(choiceContext, topCard);
        }

        if (CombatState is null) return;
        await PowerCmd.Apply<VulnerablePower>(CombatState.HittableEnemies, DynamicVars["VulnerablePower"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<WeakPower>(CombatState.HittableEnemies, DynamicVars["WeakPower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["VulnerablePower"].UpgradeValueBy(1);
        DynamicVars["WeakPower"].UpgradeValueBy(1);
    }
}