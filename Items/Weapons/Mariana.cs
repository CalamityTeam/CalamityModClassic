using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class Mariana : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mariana");
			/* Tooltip.SetDefault("Tropical and deadly\n" +
				"Enemies explode into water orbs on death"); */
		}

		public override void SetDefaults()
		{
			Item.damage = 90;
			Item.width = 54;
			Item.height = 62;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 24;
			Item.useStyle = 1;
			Item.useTime = 24;
			Item.useTurn = true;
			Item.knockBack = 6.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.value = Item.buyPrice(0, 60, 0, 0);
			Item.rare = 7;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ChlorophyteClaymore);
			recipe.AddIngredient(ItemID.Coral, 3);
			recipe.AddIngredient(ItemID.Starfish, 3);
			recipe.AddIngredient(ItemID.Seashell, 3);
			recipe.AddIngredient(null, "DepthCells", 10);
			recipe.AddIngredient(null, "Lumenite", 10);
			recipe.AddIngredient(null, "Tenebris", 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (target.life <= 0)
			{
				int num251 = Main.rand.Next(4, 6);
				for (int num252 = 0; num252 < num251; num252++)
				{
					Vector2 value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
					while (value15.X == 0f && value15.Y == 0f)
					{
						value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
					}
					value15.Normalize();
					value15 *= (float)Main.rand.Next(70, 101) * 0.1f;
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, value15.X, value15.Y, Mod.Find<ModProjectile>("MarianaProjectile").Type, (int)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative), Item.knockBack, player.whoAmI, 0f, 0f);
				}
				for (int num621 = 0; num621 < 30; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 59, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 50; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 59, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 59, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 59);
			}
		}
	}
}
