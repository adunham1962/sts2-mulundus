using BaseLib.Patches.Content;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2_Mulundus.STS2_MulundusCode.Character;
using STS2_Mulundus.STS2_MulundusCode.Extensions;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Uncommon;
[Pool(typeof(HeartwoodRangerCardPool))]
public class InsectPlague : HeartWoodRangerCard
{
    public override string PortraitPath => "Cilef Base.png".CardImagePath();
    public InsectPlague() : base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        WithDamage(4);
        WithKeyword(HeartwoodRangerKeywords.Grim);
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var numOfExhaust = CardPile.GetCards(Owner, PileType.Exhaust).Count();
        await CommonActions.CardAttack(this, play, numOfExhaust).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2);
    }
}