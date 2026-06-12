using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Common;

[Pool(typeof(EmeraldMonkCardPool))]
public class WaterWalk : EmeraldMonkCard
{

    public WaterWalk() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithCalculatedVar("CalculatedDraw", 1, (c, _) => c.Owner.Creature.GetPowerAmount<DexterityPower>());
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var amount = (DynamicVars["CalculatedDraw"] as CalculatedVar)!.Calculate(Owner.Creature);
        await CardPileCmd.Draw(choiceContext, amount, Owner);
    }

    protected override void OnUpgrade()
    {
        AddKeyword(EmeraldMonkKeywords.Ebb);
    }
}