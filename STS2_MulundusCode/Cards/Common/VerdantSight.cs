using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Extensions;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Common;
[Pool(typeof(HeartwoodRangerCardPool))]
public class VerdantSight : HeartWoodRangerCard
{
    public override string PortraitPath => "Cilef Base.png".CardImagePath();
    public VerdantSight() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        WithCards(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.Draw(this, choiceContext);
        await CommonActions.Draw(this, choiceContext);
        if (play.Card.Owner.PlayerCombatState is { ExhaustPile.IsEmpty: false })
        {
            await CommonActions.Draw(this, choiceContext);
        }
    }

    protected override void OnUpgrade()
    {
        this.AddKeyword(CardKeyword.Retain);
    }
}