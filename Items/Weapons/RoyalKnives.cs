using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class RoyalKnives : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/RoyalKnives");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Illustrious Knives");
		Item.width = 18;  //The width of the .png file in pixels divided by 2.
		Item.damage = 115;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 12;
		Item.useStyle = 1;
		Item.useTime = 12;
		////Tooltip.SetDefault("Great for impersonating devs!\nRapidly throw life-stealing holy daggers");
		Item.knockBack = 3f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item39;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 20;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 8000000;  //Value is calculated in copper coins.
		Item.expert = true;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("RoyalKnife").Type;
		Item.shootSpeed = 9f;
	}
	
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
    	int i = Main.myPlayer;
		float num72 = Item.shootSpeed;
		int num73 = Item.damage;
		float num74 = Item.knockBack;
    	num74 = player.GetWeaponKnockback(Item, num74);
    	player.itemTime = Item.useTime;
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
		Vector2 value = Vector2.UnitX.RotatedBy((double)player.fullRotation, default(Vector2));
		Vector2 vector3 = Main.MouseWorld - vector2;
    	float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
		float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
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
		int num146 = 4;
		if (Main.rand.Next(2) == 0)
		{
			num146++;
		}
		if (Main.rand.Next(4) == 0)
		{
			num146++;
		}
		if (Main.rand.Next(8) == 0)
		{
			num146++;
		}
		if (Main.rand.Next(16) == 0)
		{
			num146++;
		}
		for (int num147 = 0; num147 < num146; num147++)
		{
			float num148 = num78;
			float num149 = num79;
			float num150 = 0.05f * (float)num147;
			num148 += (float)Main.rand.Next(-35, 36) * num150;
			num149 += (float)Main.rand.Next(-35, 36) * num150;
			num80 = (float)Math.Sqrt((double)(num148 * num148 + num149 * num149));
			num80 = num72 / num80;
			num148 *= num80;
			num149 *= num80;
			float x4 = vector2.X;
			float y4 = vector2.Y;
			Projectile.NewProjectile(source, x4, y4, num148, num149, Mod.Find<ModProjectile>("RoyalKnife").Type, num73, num74, i, 0f, 0f);
		}
		return false;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "ShadowspecBar", 5);
        recipe.AddIngredient(null, "CoreofCalamity", 3);
        recipe.AddIngredient(ItemID.VampireKnives);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
