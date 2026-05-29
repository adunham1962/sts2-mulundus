using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Cards.Token;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Uncommon;

[Pool(typeof(HeartwoodRangerCardPool))]
public class CarboLoad : HeartWoodRangerCard
{
    
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/carbo_load.png";
    public CarboLoad() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var drawPile = CardPile.Get(PileType.Draw, Owner);
        if (drawPile is null) return;
        var goodberries = drawPile.Cards.ToList().FindAll(c => c is Goodberry);
        foreach (var cardModel in goodberries)
        {
            await CardCmd.AutoPlay(choiceContext, cardModel, Owner.Creature);
        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}