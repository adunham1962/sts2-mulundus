using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;

[Pool(typeof(EmeraldMonkCardPool))]
public class Evasion : EmeraldMonkCard
{

    public Evasion() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithEnergy(1);
        WithPower<SlipperyPower>(1);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
        await CommonActions.ApplySelf<SlipperyPower>(choiceContext, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Energy.UpgradeValueBy(1);
        DynamicVars["SlipperyPower"].UpgradeValueBy(1);
    }
}