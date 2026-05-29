using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class SoulShatter : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/soul_shatter.png";
    public SoulShatter() : base(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
    {
        WithDamage(10);
        WithTips(_ => [HoverTipFactory.FromCard<Soul>()]);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        if (CombatState is not null)
        {
            var souls = Soul.Create(Owner, 3, CombatState).ToList();
            var discardResult1 = await CardPileCmd.AddGeneratedCardToCombat(souls[0], PileType.Discard, true);
            var discardResult2 = await CardPileCmd.AddGeneratedCardToCombat(souls[1], PileType.Discard, true);
            var discardResult3 = await CardPileCmd.AddGeneratedCardToCombat(souls[2], PileType.Discard, true);
            CardCmd.PreviewCardPileAdd([discardResult1, discardResult2, discardResult3]);
        }
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}