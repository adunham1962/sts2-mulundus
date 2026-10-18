using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using STS2_Mulundus.STS2_MulundusCode.Enchantments;
using STS2_Mulundus.STS2_MulundusCode.Powers;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Token;

[Pool(typeof(TokenCardPool))]
public class CrispAle : ConstructedCardModel
{

    public CrispAle() : base(0, CardType.Skill, CardRarity.Token, TargetType.Self)
    {
        WithPower<CrispAleStrengthPower>(1);
        WithPower<HangoverPower>(1);
        WithPower<ConstitutionPower>(1);
        WithCards(1);
        WithKeyword(CardKeyword.Exhaust);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.ApplySelf<CrispAleStrengthPower>(choiceContext, this);
        await CommonActions.ApplySelf<HangoverPower>(choiceContext, this);
        await CommonActions.ApplySelf<ConstitutionPower>(choiceContext, this);
        var card = (await CommonActions.Draw(this, choiceContext)).LastOrDefault();
        if (card != null) CardCmd.Enchant<Tipsy>(card, 1);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["CrispAleStrengthPower"].UpgradeValueBy(1);
        DynamicVars["ConstitutionPower"].UpgradeValueBy(1);
    }
}