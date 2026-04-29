using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards;
[Pool(typeof(HeartwoodRangerCardPool))]
public class AbyssalJourney() : HeartWoodRangerCard(1, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected new IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return
            [
                new PowerVar<AbyssalJourneyPower>(1M),
                new EnergyVar(1),
                new CardsVar(4)
            ];
        }
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        List<CardModel> hand = PileType.Hand.GetPile(this.Owner).Cards.ToList<CardModel>();
        foreach (CardModel card in hand)
            await CardCmd.Exhaust(choiceContext, card);
        await CardPileCmd.Draw(choiceContext, this.DynamicVars.Cards.BaseValue, this.Owner);
        await PowerCmd.Apply<AbyssalJourneyPower>(this.Owner.Creature, (Decimal) this.DynamicVars["AbyssalJourneyPower"].IntValue,
            this.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Cards.UpgradeValueBy(2);
    }
}