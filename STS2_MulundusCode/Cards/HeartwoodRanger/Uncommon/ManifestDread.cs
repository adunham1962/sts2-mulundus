using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2_Mulundus.STS2_MulundusCode.Character;

namespace STS2_Mulundus.STS2_MulundusCode.Cards.HeartwoodRanger.Uncommon;

[Pool(typeof(HeartwoodRangerCardPool))]
public class ManifestDread : HeartWoodRangerCard
{
    public override string PortraitPath => "res://STS2_Mulundus/images/card_portraits/manifest_dread.png";
    public ManifestDread() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        WithKeyword(CardKeyword.Ethereal);
        WithPower<VulnerablePower>(1);
        WithPower<WeakPower>(1);
    }

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var pile = CardPile.GetCards(Owner, PileType.Draw).ToList();
        var count = pile.Count > 10 ? 10 : pile.Count;
        var damage = pile.GetRange(0, count).Sum(card => card.EnergyCost.Canonical);

        if (CombatState is null) return;
        
        await DamageCmd.Attack(damage).FromCard(this).TargetingAllOpponents(CombatState).Execute(choiceContext);
        await CommonActions.Apply<VulnerablePower>(CombatState.HittableEnemies, this);
        await CommonActions.Apply<WeakPower>(CombatState.HittableEnemies, this);
    }

    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Ethereal);
    }
}