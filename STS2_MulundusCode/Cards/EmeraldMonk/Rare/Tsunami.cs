using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Rare;
[Pool(typeof(EmeraldMonkCardPool))]
public class Tsunami : EmeraldMonkCard
{
    public Tsunami() : base(3, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
    {
        WithCalculatedDamage(15, 3, (card, _) => card.CombatState!.Creatures.Sum(c => c.GetPowerAmount<DrenchedPower>()));
        WithPower<DrenchedPower>(3);
        WithKeyword(MulundusKeywords.Consume);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var bonus = 0;
        if (CombatState != null) bonus = await Consume<DrenchedPower>(CombatState.Creatures);
        await CommonActions.CardAttack(this, play.Target, DynamicVars.CalculationBase.BaseValue + bonus).Execute(choiceContext);
        if (CombatState != null) await CommonActions.Apply<DrenchedPower>(choiceContext, CombatState.Creatures, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.ExtraDamage.UpgradeValueBy(2);
        DynamicVars["DrenchedPower"].UpgradeValueBy(3);
    }
}