using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class PhoenixBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Phoenix Blade");
		}

		public override void SetDefaults()
		{
			Item.width = 106;
			Item.damage = 35;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 30;
			Item.useTurn = true;
			Item.knockBack = 7f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 106;
			Item.value = 300000;
			Item.rare = ItemRarityID.Pink;
			Item.shootSpeed = 12f;
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, 612, hit.Damage, hit.Knockback, Main.myPlayer);
			float spread = 180f * 0.0174f;
			double startAngle = Math.Atan2(Item.shootSpeed, Item.shootSpeed)- spread/2;
			double deltaAngle = spread/8f;
			double offsetAngle;
			int i;
			if (Main.rand.NextBool(4))
			{
				for (i = 0; i < 1; i++ )
				{
					float randomSpeedX = (float)Main.rand.Next(5);
					float randomSpeedY = (float)Main.rand.Next(3, 7);
				   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
				   	int projectile1 = Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("PhoenixHeal").Type, hit.Damage, hit.Knockback, Main.myPlayer);
				    int projectile2 = Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("PhoenixHeal").Type, hit.Damage, hit.Knockback, Main.myPlayer);
				    Main.projectile[projectile1].velocity.X = -randomSpeedX;
				    Main.projectile[projectile1].velocity.Y = -randomSpeedY;
				    Main.projectile[projectile2].velocity.X = randomSpeedX;
				    Main.projectile[projectile2].velocity.Y = -randomSpeedY;
				}
			}
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(4))
	        {
	            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.CopperCoin);
	        }
	    }
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BreakerBlade);
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddIngredient(null, "EssenceofCinder", 2);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
