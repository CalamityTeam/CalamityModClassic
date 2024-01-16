using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Earth : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/Earth");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Earth");
		Item.width = 134;  //The width of the .png file in pixels divided by 2.
		Item.damage = 420;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 16;
		Item.useStyle = 1;
		Item.useTime = 16;
		Item.useTurn = true;
		////Tooltip.SetDefault("Has a chance to lower enemy defense by 50 when striking them\nYour attacks will heal you a lot\nRains RGB meteors that explode into more meteors after a short time on enemy hits\nIce meteors freeze enemies\nFlame meteors explode\nGreen meteors spawn healing orbs");
		Item.knockBack = 9.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 134;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 69696969;  //Value is calculated in copper coins.
		Item.expert = true;  //Ranges from 1 to 11.
	}
	
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		if (Main.rand.Next(3) == 0)
		{
			target.defense -= 50;
		}
		int heal = Main.rand.Next(35, 69);
	    player.statLife += heal;
	   	player.HealEffect(heal);
		int i = Main.myPlayer;
		float num72 = 25f;
		int num73 = hit.Damage;
		float num74 = Item.knockBack;
	   	num74 = player.GetWeaponKnockback(Item, num74);
	   	player.itemTime = Item.useTime;
	   	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
	   	float num78 = (float)Main.mouseX - Main.screenPosition.X - vector2.X;
		float num79 = (float)Main.mouseY - Main.screenPosition.Y - vector2.Y;
		if (player.gravDir == -1f)
		{
			num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
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
		int num107 = 3;
		for (int num108 = 0; num108 < num107; num108++)
		{
			vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
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
			float speedX4 = num78;
			float speedY5 = num79 + (float)Main.rand.Next(-180, 181) * 0.02f;
			Projectile.NewProjectile(player.GetSource_FromThis(), vector2.X, vector2.Y, speedX4, speedY5, Mod.Find<ModProjectile>("Earth").Type, num73, num74, i, 0f, (float)Main.rand.Next(10));
		}
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "GrandGuardian");
		recipe.AddIngredient(null, "GalacticaBlade");
		recipe.AddIngredient(null, "ShadowspecBar", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
