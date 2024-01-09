using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons {
public class Terracotta : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Terra-cotta");
			//Tooltip.SetDefault("Causes enemies to erupt into healing projectiles on death\nEnemies explode on death");
		}

	public override void SetDefaults()
	{
		Item.width = 42;  //The width of the .png file in pixels divided by 2.
		Item.damage = 117;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 17;
		Item.useTime = 17;  //Ranges from 1 to 55.
		Item.useTurn = true;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.knockBack = 5.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 50;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 800000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Yellow;  //Ranges from 1 to 11.
		Item.shootSpeed = 5f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "BurntSienna");
		recipe.AddIngredient(null, "UnholyCore", 3);
		recipe.AddIngredient(null, "CoreofCinder", 3);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
	
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		float spread = 180f * 0.0174f;
		double startAngle = Math.Atan2(Item.shootSpeed, Item.shootSpeed)- spread/2;
		double deltaAngle = spread/8f;
		double offsetAngle;
		int i;
		if (target.life <= 0)
		{
    		for (i = 0; i < 1; i++ )
			{
				float randomSpeedX = (float)Main.rand.Next(3);
				float randomSpeedY = (float)Main.rand.Next(3, 5);
			   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
			   	int projectile1 = Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("Terracotta").Type, hit.Damage, hit.Knockback, Main.myPlayer);
			    int projectile2 = Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("Terracotta").Type, hit.Damage, hit.Knockback, Main.myPlayer);
				int projectile3 = Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("Terracotta").Type, hit.Damage, hit.Knockback, Main.myPlayer);
			    Main.projectile[projectile1].velocity.X = -randomSpeedX;
			    Main.projectile[projectile1].velocity.Y = -randomSpeedY;
			    Main.projectile[projectile2].velocity.X = randomSpeedX;
			    Main.projectile[projectile2].velocity.Y = -randomSpeedY;
			    Main.projectile[projectile3].velocity.X = 0f;
			    Main.projectile[projectile3].velocity.Y = -randomSpeedY;
			}
    		Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("TerracottaExplosion").Type, hit.Damage, hit.Knockback, Main.myPlayer);
		}
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(5))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.GoldCoin);
        }
    }
}}
