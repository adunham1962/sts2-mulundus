using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Common;

[Pool(typeof(EmeraldMonkCardPool))]
public class FlowingFlurry : EmeraldMonkCard
{
    public FlowingFlurry() : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithDamage(4);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        var card = cardPlay.Card;
        if (card.Owner == Owner && card != this && card is EmeraldMonkCard && card.HasFlow() && CardPile.GetCards(Owner, PileType.Discard).Contains(this))
        {
            await CardPileCmd.Add(this, PileType.Hand);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
    }
}