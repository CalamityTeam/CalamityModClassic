using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class PlanetaryAnnihilation : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/PlanetaryAnnihilation");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Planetary Annihilation");
        Item.damage = 95;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 48;
        Item.height = 102;
        Item.scale = 0.75f;
        Item.useTime = 22;
        Item.useAnimation = 22;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        ////Tooltip.SetDefault("A bow of the gods");
        Item.knockBack = 5.5f;
        Item.value = 2000000;
        Item.rare = 10;
        Item.UseSound = SoundID.Item75;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("TerraBall").Type; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 16f;
        Item.useAmmo = 40;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	int i = Main.myPlayer;
		float num72 = Main.rand.Next(19, 35);
		int num73 = Main.rand.Next(80, 120);
		float num74 = Item.knockBack;
    	num74 = player.GetWeaponKnockback(Item, num74);
    	player.itemTime = Item.useTime;
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
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
		int num107 = 7;
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
			float speedX4 = num78 + (float)Main.rand.Next(-240, 241) * 0.02f;
			float speedY5 = num79 + (float)Main.rand.Next(-240, 241) * 0.02f;
			Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, (speedY5 * 1.3f), Mod.Find<ModProjectile>("TerraBallR").Type, num73, num74, i, 0f, (float)Main.rand.Next(2));
			Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, (speedY5 * 1.2f), Mod.Find<ModProjectile>("TerraBallO").Type, num73, num74, i, 0f, (float)Main.rand.Next(3));
			Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, (speedY5 * 1.1f), Mod.Find<ModProjectile>("TerraBallY").Type, num73, num74, i, 0f, (float)Main.rand.Next(4));
			Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, (speedY5 * 1f), Mod.Find<ModProjectile>("TerraBallG").Type, num73, num74, i, 0f, (float)Main.rand.Next(5));
			Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, (speedY5 * 0.9f), Mod.Find<ModProjectile>("TerraBallB").Type, num73, num74, i, 0f, (float)Main.rand.Next(6));
			Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, (speedY5 * 0.8f), Mod.Find<ModProjectile>("TerraBallI").Type, num73, num74, i, 0f, (float)Main.rand.Next(7));
			Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, (speedY5 * 0.7f), Mod.Find<ModProjectile>("TerraBallV").Type, num73, num74, i, 0f, (float)Main.rand.Next(8));
		}
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "GalacticaSingularity", 5);
        recipe.AddIngredient(null, "LunarianBow");
        recipe.AddIngredient(ItemID.DaedalusStormbow);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}