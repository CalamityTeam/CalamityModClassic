using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class PhoenixBlade : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/PhoenixBlade");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Phoenix Blade");
		Item.width = 106;  //The width of the .png file in pixels divided by 2.
		Item.damage = 43;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 27;
		Item.useStyle = 1;
		Item.useTime = 27;
		Item.useTurn = true;
		////Tooltip.SetDefault("Striking enemies causes explosions and has a chance to spawn healing orbs");
		Item.knockBack = 7.75f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 106;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 300000;  //Value is calculated in copper coins.
		Item.rare = 5;  //Ranges from 1 to 11.
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
		if (Main.rand.Next(4) == 0)
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
        if (Main.rand.Next(4) == 0)
        {
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 244);
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
}}
