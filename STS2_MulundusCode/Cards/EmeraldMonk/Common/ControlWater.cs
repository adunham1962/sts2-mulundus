using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Common;
[Pool(typeof(EmeraldMonkCardPool))]
public class ControlWater : EmeraldMonkCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/control_water.png";
    public ControlWater() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithKeyword(MulundusKeywords.Absorb);
        WithKeyword(EmeraldMonkKeywords.Flow);
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        if (CombatState != null) await Absorb<SlipperyPower>(choiceContext, CombatState.HittableEnemies);
        var prefs = new CardSelectorPrefs(SelectionScreenPrompt, 1);
        var card = (await CardSelectCmd.FromHand(choiceContext, Owner, prefs, c => !c.Keywords.Contains(EmeraldMonkKeywords.Ebb) && c is EmeraldMonkCard, this)).FirstOrDefault();
        if (card == null)
            return;
        CardCmd.ApplyKeyword(card, EmeraldMonkKeywords.Ebb);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}