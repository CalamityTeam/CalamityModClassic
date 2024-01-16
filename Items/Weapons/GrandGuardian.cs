using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class GrandGuardian : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/GrandGuardian");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Grand Guardian");
		Item.width = 120;  //The width of the .png file in pixels divided by 2.
		Item.damage = 360;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 22;
		Item.useStyle = 1;
		Item.useTime = 22;
		Item.useTurn = true;
		////Tooltip.SetDefault("Has a chance to lower enemy defense by 30 when striking them\nIf enemy defense is 0 or below your attacks will heal you\nStriking enemies causes a large explosion\nStriking enemies that have under half life will make you release rainbow bolts\nEnemies spawn healing orbs on death");
		Item.knockBack = 8.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 120;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 5000000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shootSpeed = 12f;
	}
	
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		if (Main.rand.Next(2) == 0)
		{
			target.defense -= 30;
		}
		if (target.defense <= 0)
		{
	    	player.statLife += 12;
	    	player.HealEffect(12);
		}
		Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("RainbowBoom").Type, hit.Damage, hit.Knockback, Main.myPlayer);
		float spread = 180f * 0.0174f;
		double startAngle = Math.Atan2(Item.shootSpeed, Item.shootSpeed)- spread/2;
		double deltaAngle = spread/8f;
		double offsetAngle;
		int i;
		if (target.life <= (target.lifeMax * 0.5f))
		{
			for (i = 0; i < 1; i++ )
			{
				float randomSpeedX = (float)Main.rand.Next(9);
				float randomSpeedY = (float)Main.rand.Next(6, 15);
			   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
			   	int projectile1 = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainBolt").Type, hit.Damage, hit.Knockback, Main.myPlayer);
			    int projectile2 = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainBolt").Type, hit.Damage, hit.Knockback, Main.myPlayer);
				int projectile3 = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainBolt").Type, hit.Damage, hit.Knockback, Main.myPlayer);
			    Main.projectile[projectile1].velocity.X = -randomSpeedX;
			    Main.projectile[projectile1].velocity.Y = -randomSpeedY;
			    Main.projectile[projectile2].velocity.X = randomSpeedX;
			    Main.projectile[projectile2].velocity.Y = -randomSpeedY;
			    Main.projectile[projectile3].velocity.X = 0f;
			    Main.projectile[projectile3].velocity.Y = -randomSpeedY;
			}
		}
		if (target.life <= 0)
		{
    		for (i = 0; i < 1; i++ )
			{
				float randomSpeedX = (float)Main.rand.Next(9);
				float randomSpeedY = (float)Main.rand.Next(6, 15);
			   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
			   	int projectile1 = Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainHeal").Type, hit.Damage, hit.Knockback, Main.myPlayer);
			    int projectile2 = Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainHeal").Type, hit.Damage, hit.Knockback, Main.myPlayer);
				int projectile3 = Projectile.NewProjectile(player.GetSource_FromThis(), target.Center.X, target.Center.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), Mod.Find<ModProjectile>("RainHeal").Type, hit.Damage, hit.Knockback, Main.myPlayer);
			    Main.projectile[projectile1].velocity.X = -randomSpeedX;
			    Main.projectile[projectile1].velocity.Y = -randomSpeedY;
			    Main.projectile[projectile2].velocity.X = randomSpeedX;
			    Main.projectile[projectile2].velocity.Y = -randomSpeedY;
			    Main.projectile[projectile3].velocity.X = 0f;
			    Main.projectile[projectile3].velocity.Y = -randomSpeedY;
			}
		}
	}
	
	public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(3) == 0)
        {
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 66, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f);
            Main.dust[dust].noGravity = true;
        }
    }

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "MajesticGuard");
		recipe.AddIngredient(null, "BarofLife", 10);
		recipe.AddIngredient(null, "GalacticaSingularity", 3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
