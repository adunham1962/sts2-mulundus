using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Special;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.EmeraldMonk.Rare;
[Pool(typeof(EmeraldMonkCardPool))]
public class ShadowedWebStance : EmeraldMonkCard
{
    public ShadowedWebStance() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithKeyword(EmeraldMonkKeywords.EnterStance);
        WithTips(_ => [HoverTipFactory.FromCard<SlipIntoShadow>()]);
    }

    public override async Task AfterCardPlayedLate(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (CombatState is null || cardPlay.Card != this) return;
        var card = SlipIntoShadow.Create(Owner, 1, CombatState).ToList()[0];
        if (IsUpgraded)
        {
            CardCmd.Upgrade(card);
        }

        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, Owner);
    }
    
    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}