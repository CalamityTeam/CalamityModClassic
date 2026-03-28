using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.RareVariants
{
	public class EvilSmasher : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Evil Smasher");
			// Tooltip.SetDefault("EViL! sMaSH eVIl! SmAsh...ER!");
		}

		public override void SetDefaults()
		{
			Item.width = 62;
			Item.damage = 55;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 30;
			Item.useStyle = 1;
			Item.useTime = 30;
			Item.useTurn = true;
			Item.knockBack = 8f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 62;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}
	
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("FossilSpike").Type, (int)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative), Item.knockBack, Main.myPlayer);
			if (Main.rand.Next(3) == 0)
			{
				Item.damage = 82;
				Item.useAnimation = 15;
				Item.useTime = 15;
				Item.knockBack = 14f;
			}
			else
			{
				Item.damage = 55;
				Item.useAnimation = 30;
				Item.useTime = 30;
				Item.knockBack = 8f;
			}
		}
	}
}
