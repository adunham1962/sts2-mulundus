using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Common;
[Pool(typeof(HeartwoodRangerCardPool))]
public class Lacerate : HeartWoodRangerCard
{
    
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/lacerate.png";
    public Lacerate() : base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(6);
        WithVar("Damage2", 1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var deck = CardPile.Get(PileType.Deck, Owner);
        var extraAmount = 0;
        if (deck is not null)
        {
            extraAmount = deck.Cards.ToList().FindAll(c => c is HeartWoodRangerCard && c.IsGrim()).Count;
        }

        var damage = DynamicVars.Damage.BaseValue + (extraAmount * DynamicVars["Damage2"].BaseValue);
        if (play.Target is null) return;
        await DamageCmd.Attack(damage).FromCard(this).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Damage2"].UpgradeValueBy(1);
    }
}