using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Common;
[Pool(typeof(EmeraldMonkCardPool))]
public class ShapeWater : EmeraldMonkCard
{

    public ShapeWater() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithBlock(7);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        var prefs = new CardSelectorPrefs(SelectionScreenPrompt, 1);
        var card = (await CardSelectCmd.FromHand(choiceContext, Owner, prefs, c => !c.Keywords.Contains(EmeraldMonkKeywords.Flow) && c is EmeraldMonkCard, this)).FirstOrDefault();
        if (card == null)
            return;
        CardCmd.ApplyKeyword(card, EmeraldMonkKeywords.Flow);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(2);
    }
}