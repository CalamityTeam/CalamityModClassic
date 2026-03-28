using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Permafrost
{
	public class AbsoluteZero : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Absolute Zero");
			// Tooltip.SetDefault("Ancient blade imbued with the Archmage of Ice's magic\nShoots dark ice crystals\nThe blade creates frost explosions on direct hits");
		}
		public override void SetDefaults()
		{
			Item.damage = 42;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 58;
			Item.height = 58;
			Item.useTime = 20;
            Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.useTurn = false;
			Item.knockBack = 4f;
			Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("DarkIceZero").Type;
            Item.shootSpeed = 3f;
		}
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 600);
            target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 300);

            int p = Projectile.NewProjectile(Entity.GetSource_FromThis(null),target.Center, Vector2.Zero, Mod.Find<ModProjectile>("DarkIceZero").Type, Item.damage, Item.knockBack * 3f, player.whoAmI);
            Main.projectile[p].Kill();
        }
    }
}
