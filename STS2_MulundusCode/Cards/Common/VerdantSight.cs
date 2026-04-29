using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Common;
[Pool(typeof(HeartwoodRangerCardPool))]
public class VerdantSight() : HeartWoodRangerCard(1, CardType.Skill, CardRarity.Common, TargetType.None)
{

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.Draw(this, choiceContext);
        if (PileType.Exhaust.GetPile(this.Owner).Cards.Count > 2)
        {
            await CommonActions.Draw(this, choiceContext);
            await CommonActions.Draw(this, choiceContext);
        }
    }

    protected override void OnUpgrade()
    {
        this.AddKeyword(CardKeyword.Retain);
    }
}