using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Cards.Status;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Rare;
[Pool(typeof(HeartwoodRangerCardPool))]
public class Hatred : HeartWoodRangerCard
{
    public Hatred() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithTips(_ => [HoverTipFactory.FromCard<BlindedByHatred>()]);
        WithPower<StrengthPower>(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        if (CombatState is not null)
        {
            var statuses = BlindedByHatred.Create(Owner, 3, CombatState).ToList();
            List<CardPileAddResult> added = [
                await CardPileCmd.AddGeneratedCardToCombat(statuses[0], PileType.Draw, true),
                await CardPileCmd.AddGeneratedCardToCombat(statuses[1], PileType.Draw, true),
                await CardPileCmd.AddGeneratedCardToCombat(statuses[2], PileType.Draw, true)
            ];
            CardCmd.PreviewCardPileAdd(added);
        }
        
        var debuffs = Owner.Creature.Powers.ToList().FindAll(p => p.Type == PowerType.Debuff);
        var gainAndDraw = DynamicVars["StrengthPower"].BaseValue * debuffs.Count;
        await PowerCmd.Apply<StrengthPower>(Owner.Creature, gainAndDraw, Owner.Creature, this);
        await CardPileCmd.Draw(choiceContext, gainAndDraw, Owner);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}