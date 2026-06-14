using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Common;
[Pool(typeof(EmeraldMonkCardPool))]
public class ControlWater : EmeraldMonkCard
{
    public ControlWater() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithCalculatedVar("CalculatedStrength", 0, 1, (card, _) => PileType.Hand.GetPile(card.Owner).Cards.Count(c => c.Type == CardType.Attack));
        WithCalculatedVar("CalculatedDexterity", 0, 1, (card, _) => PileType.Hand.GetPile(card.Owner).Cards.Count(c => c.Type == CardType.Skill));
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var strength = (DynamicVars["CalculatedStrength"] as CalculatedVar)!.Calculate(play.Target);
        var dexterity = (DynamicVars["CalculatedDexterity"] as CalculatedVar)!.Calculate(play.Target); 

        await CommonActions.ApplySelf<ControlWaterStrengthPower>(choiceContext, this, strength);
        await CommonActions.ApplySelf<ControlWaterDexterityPower>(choiceContext, this, dexterity);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(EmeraldMonkKeywords.Ebb);
    }
}