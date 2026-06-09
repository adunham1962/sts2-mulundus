using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class FindTraps : HeartWoodRangerCard
{
    public FindTraps() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithKeyword(CardKeyword.Exhaust);
        WithCards(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var drawPile = PileType.Draw.GetPile(Owner);
        if (drawPile.IsEmpty)
        {
            return;
        }
        var statuses = drawPile.Cards.ToList().FindAll(c => c.Type == CardType.Status);
        foreach (var cardModel in statuses)
        {
            await CardCmd.Exhaust(choiceContext, cardModel);
        }

        foreach (var unused in statuses)
        {
            await CommonActions.Draw(this, choiceContext);
        }
        
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}