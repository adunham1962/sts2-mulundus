using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class InsectPlague : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/insect_plague.png";
    public InsectPlague() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        WithDamage(1);
        WithVar("Hits", 3);
        WithKeyword(CardKeyword.Exhaust);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.CardAttack(this, play, DynamicVars["Hits"].IntValue).Execute(choiceContext);
        var clone1 = CreateClone();
        clone1.EnergyCost.SetThisCombat(0);
        var cloneAdd1 = await CardPileCmd.AddGeneratedCardToCombat(clone1, PileType.Draw, true);
        var clone2 = CreateClone();
        clone2.EnergyCost.SetThisCombat(0);
        var cloneAdd2 = await CardPileCmd.AddGeneratedCardToCombat(clone2, PileType.Draw, true);
        var added = new List<CardPileAddResult>(
        [
            cloneAdd1,
            cloneAdd2
        ]);
        CardCmd.PreviewCardPileAdd(added, 2.2f);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Hits"].UpgradeValueBy(1);
    }
}