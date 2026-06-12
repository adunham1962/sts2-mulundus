using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Uncommon;
[Pool(typeof(EmeraldMonkCardPool))]
public class Feint : EmeraldMonkCard
{

    public Feint() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithBlock(4);
        WithPower<FeintPower>(1);
        WithKeyword(EmeraldMonkKeywords.Ebb);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        await CommonActions.ApplySelf<FeintPower>(choiceContext, this);
        await PowerCmd.Apply<DoubleDamagePower>(Owner.Creature, 1, Owner.Creature, null);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}