using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2_Mulundus.STS2_MulundusCode.Cards.Status;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class Despair : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/despair.png";
    public Despair() : base(3, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithTips(_ => [HoverTipFactory.FromCard<LostInDespair>()]);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var drawPile = CardPile.GetCards(Owner, PileType.Draw).ToList();
        foreach (var cardModel in drawPile)
        {
            await CardCmd.Exhaust(choiceContext, cardModel);
        }

        if (CombatState is null) return;
        var statuses = LostInDespair.Create(Owner, 2, CombatState).ToList();
        List<CardPileAddResult> added =
        [
            await CardPileCmd.AddGeneratedCardToCombat(statuses[0], PileType.Discard, true),
            await CardPileCmd.AddGeneratedCardToCombat(statuses[1], PileType.Discard, true)
        ];
        CardCmd.PreviewCardPileAdd(added);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}