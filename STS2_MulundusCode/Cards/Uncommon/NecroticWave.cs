using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2_Mulundus.STS2_MulundusCode.Cards.Status;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.Uncommon;

[Pool(typeof(HeartwoodRangerCardPool))]
public class NecroticWave : HeartWoodRangerCard
{
    
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/necrotic_wave.png";
    public NecroticWave() : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        WithTips(_ => [HoverTipFactory.FromCard<Necrosis>()]);
        WithKeyword(CardKeyword.Exhaust);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var ePile = CardPile.Get(PileType.Exhaust, Owner);
        if (ePile is null || CombatState is null) return;
        var damage = ePile.Cards.Count;
        await DamageCmd.Attack(damage).FromCard(this).TargetingAllOpponents(CombatState).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Exhaust);
    }
}