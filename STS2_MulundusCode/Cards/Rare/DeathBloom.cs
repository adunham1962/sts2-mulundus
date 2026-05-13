using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Extensions;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Rare;

[Pool(typeof(HeartwoodRangerCardPool))]
public class DeathBloom : HeartWoodRangerCard
{
    public override string PortraitPath => "Cilef Base.png".CardImagePath();
    public DeathBloom() : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithEnergy(1);
        WithPower<ThornsPower>(1);
        WithPower<PoisonPower>(3);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
        await CommonActions.ApplySelf<ThornsPower>(this);
        await CommonActions.ApplySelf<PoisonPower>(this);
        var prefs = new CardSelectorPrefs(SelectionScreenPrompt, 1);
        if (PileType.Discard.GetPile(Owner).IsEmpty)
        {
            return;
        }
        var card = (await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Discard.GetPile(Owner).Cards.ToList(), Owner, prefs)).FirstOrDefault();
        if (card == null)
            return;
        await CardCmd.Exhaust(choiceContext, card);
    }

    protected override void OnUpgrade()
    {

    }
}