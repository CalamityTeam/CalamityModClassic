using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ArkoftheElements : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/ArkoftheElements");
        return true;
    }


	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Ark of the Elements");
		Item.width = 70;  //The width of the .png file in pixels divided by 2.
		Item.damage = 235;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 20;
		Item.useTime = 20;  //Ranges from 1 to 55. 
		Item.useTurn = true;
		Item.useStyle = 1;
		Item.crit += 10;
		Item.knockBack = 8.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item60;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 70;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		////Tooltip.SetDefault("A heavenly blade infused with the essence of Terraria");
		Item.value = 10000000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("EonBeam").Type;
		Item.shootSpeed = 24f;
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
        switch (Main.rand.Next(4))
		{
	    	case 0: type = Mod.Find<ModProjectile>("EonBeam").Type; break;
	    	case 1: type = Mod.Find<ModProjectile>("EonBeamV2").Type; break;
	    	case 2: type = Mod.Find<ModProjectile>("EonBeamV3").Type; break;
	    	case 3: type = Mod.Find<ModProjectile>("EonBeamV4").Type; break;
		}
        int projectile = Projectile.NewProjectile(source, position, velocity,type, damage, knockback, Main.myPlayer);
        Main.projectile[projectile].timeLeft = 160;
        Main.projectile[projectile].tileCollide = false;
       	int i = Main.myPlayer;
		float num72 = Main.rand.Next(22, 30);
		int num73 = Main.rand.Next(300, 500);
		float num74 = Item.knockBack;
    	num74 = player.GetWeaponKnockback(Item, num74);
    	player.itemTime = Item.useTime;
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
		Vector2 value = Vector2.UnitX.RotatedBy((double)player.fullRotation, default(Vector2));
		Vector2 vector3 = Main.MouseWorld + vector2;
    	float num78 = (float)Main.mouseX + Main.screenPosition.X + vector2.X;
		float num79 = (float)Main.mouseY + Main.screenPosition.Y + vector2.Y;
		if (player.gravDir == -1f)
		{
			num79 = Main.screenPosition.Y + (float)Main.screenHeight + (float)Main.mouseY + vector2.Y;
		}
		float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
		float num81 = num80;
		if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
		{
			num78 = (float)player.direction;
			num79 = 0f;
			num80 = num72;
		}
		else
		{
			num80 = num72 / num80;
		}
    	num78 *= num80;
		num79 *= num80;
		int num107 = 4;
		for (int num108 = 0; num108 < num107; num108++)
		{
			vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y);
			vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
			vector2.Y -= (float)(100 * num108);
			num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
			num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
			if (num79 < 0f)
			{
				num79 *= -1f;
			}
			if (num79 < 20f)
			{
				num79 = 20f;
			}
			num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
			num80 = num72 / num80;
			num78 *= num80;
			num79 *= num80;
			float speedX4 = num78 + (float)Main.rand.Next(-360, 361) * 0.02f;
			float speedY5 = num79 + (float)Main.rand.Next(-360, 361) * 0.02f;
			Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, speedY5, Mod.Find<ModProjectile>("ElementBall").Type, num73, num74, i, 0f, (float)Main.rand.Next(3));
		}
    	return false;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "GalacticaSingularity", 5);
		recipe.AddIngredient(null, "TrueArkoftheAncients");
		recipe.AddIngredient(null, "Terratomere");
		recipe.AddIngredient(null, "CoreofCalamity", 3);
		recipe.AddIngredient(null, "BarofLife", 5);
		recipe.AddIngredient(ItemID.LunarBar, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.Next(5) == 0)
		{
			int num250 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 66, (float)(player.direction * 2), 0f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.3f);
			Main.dust[num250].velocity *= 0.2f;
			Main.dust[num250].noGravity = true;
		}
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
    	target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 500);
    	target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 500);
    	target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 500);
    	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 500);
	}
}}
