using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class GreaterRestoration : HeartWoodRangerCard 
{

    public GreaterRestoration() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithHeal(3);
        WithKeyword(CardKeyword.Exhaust);
        WithCalculatedVar("CalculatedDraw", 0, (card, _) => card.Owner.Creature.Powers.Count((p) => p.Type == PowerType.Debuff));
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CardPileCmd.Draw(choiceContext, (DynamicVars["CalculatedDraw"] as CalculatedVar)!.Calculate(null), Owner);
        var debuffs = Owner.Creature.Powers.Where(p => p.Type == PowerType.Debuff).ToList();
        foreach (var powerModel in debuffs)
        {
            await PowerCmd.Remove(powerModel);
        }

        await CreatureCmd.Heal(Owner.Creature, DynamicVars["Heal"].BaseValue);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}