using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Ancient;
[Pool(typeof(EventCardPool))]
public class ReforgedSoul : ConstructedCardModel
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/reforged_soul.png";
    
    public ReforgedSoul() : base(0, CardType.Skill, CardRarity.Ancient, TargetType.Self)
    {
        WithCards(2);
    }
    private int _timesPlayedThisCombat;
    private int TimesPlayedThisCombat
    {
        get => _timesPlayedThisCombat;
        set
        {
            AssertMutable();
            _timesPlayedThisCombat = value;
        }
    }
    
    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CommonActions.Draw(this, choiceContext);
        TimesPlayedThisCombat++;
        EnergyCost.AddThisCombat(1);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}