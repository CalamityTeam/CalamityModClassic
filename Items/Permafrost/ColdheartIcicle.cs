using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Permafrost
{
	public class ColdheartIcicle : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Coldheart Icicle");
			// Tooltip.SetDefault("Drains a percentage of enemy health on hit\nCannot inflict critical hits");
		}
		public override void SetDefaults()
		{
			Item.damage = 1;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 26;
			Item.height = 26;
            Item.useTime = 27;
            Item.useAnimation = 27;
            Item.autoReuse = true;
            Item.useStyle = 3;
            Item.UseSound = SoundID.Item1;
            Item.useTurn = true;
			Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
		}
        public override void ModifyHitPvp(Player player, Player target, ref Player.HurtModifiers modifiers)
        {
            Item.damage = target.statLifeMax2 * 2 / 100;
            target.statDefense -= target.statDefense;
            target.endurance = 0f;

        }
        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
	        Item.damage = 1;
	        modifiers.DisableCrit();
            if (target.type != NPCID.TargetDummy)
                target.life -= target.lifeMax * 2 / 100;
            target.checkDead();
        }
    }
}
