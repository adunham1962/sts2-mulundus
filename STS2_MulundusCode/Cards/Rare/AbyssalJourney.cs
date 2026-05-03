using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class AbyssalJourney : HeartWoodRangerCard
{

    public AbyssalJourney() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithCards(4);
        WithPower<AbyssalJourneyPower>(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        AbyssalJourney aj = this;
        await CreatureCmd.TriggerAnim(aj.Owner.Creature, "Cast", aj.Owner.Character.CastAnimDelay);
        List<CardModel> hand = PileType.Hand.GetPile(aj.Owner).Cards.ToList();
        foreach (CardModel card in hand)
            await CardCmd.Exhaust(choiceContext, card);
        await CommonActions.Draw(this, choiceContext);
        await PowerCmd.Apply<AbyssalJourneyPower>(aj.Owner.Creature, aj.DynamicVars["AbyssalJourneyPower"].IntValue,
            aj.Owner.Creature, aj);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Cards.UpgradeValueBy(2);
    }
}