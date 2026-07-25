using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using STS2_Mulundus.STS2_MulundusCode.Cards.Token;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Uncommon;

[Pool(typeof(HeartwoodRangerCardPool))]
public class CarboLoad : HeartWoodRangerCard
{
    
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/carbo_load.png";
    public CarboLoad() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<ConstitutionPower>(2);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var drawPile = CardPile.Get(PileType.Draw, Owner);
        var discardPile = CardPile.Get(PileType.Discard, Owner);
        List<CardModel> berries;
        berries = [];
        if (drawPile is not null)
        {
            berries = berries.Concat(drawPile.Cards.ToList().FindAll(c => c is Goodberry)).ToList();
        }

        if (discardPile is not null)
        {
            berries = berries.Concat(discardPile.Cards.ToList().FindAll(c => c is Goodberry)).ToList();
        }
        
        foreach (var cardModel in berries)
        {
            await CardCmd.AutoPlay(choiceContext, cardModel, Owner.Creature);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["ConstitutionPower"].UpgradeValueBy(2);
    }
}