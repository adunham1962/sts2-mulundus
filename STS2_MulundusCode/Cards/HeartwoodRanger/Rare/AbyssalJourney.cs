using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Extensions;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class AbyssalJourney : HeartWoodRangerCard
{
    public override string PortraitPath => "Cilef Base.png".CardImagePath();
    public AbyssalJourney() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        WithPower<WisdomPower>(2);
        WithKeyword(CardKeyword.Ethereal);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        var hand = PileType.Hand.GetPile(Owner).Cards.ToList();
        var count = hand.Count;
        foreach (var card in hand)
            await CardCmd.Exhaust(choiceContext, card);

        await CommonActions.ApplySelf<WisdomPower>(choiceContext, this, DynamicVars["WisdomPower"].BaseValue + count);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["WisdomPower"].UpgradeValueBy(2);
    }
}