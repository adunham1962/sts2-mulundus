using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Extensions;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class DanceInTheDark : HeartWoodRangerCard
{
    public override string PortraitPath => "Cilef Base.png".CardImagePath();
    public DanceInTheDark() : base(3, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        WithCards(2);
        WithBlock(12);
        WithKeyword(HeartwoodRangerKeywords.Grim);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        await CommonActions.Draw(this, choiceContext);
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (Pile is { Type: PileType.Exhaust } && player == Owner)
        {
            await CardCmd.AutoPlay(choiceContext, this, Owner.Creature);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(4);
    }
}