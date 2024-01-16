using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ClockworkBow : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/ClockworkBow");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Clockwork Bow");
        ////Tooltip.SetDefault("Fires a storm of arrows in random directions\nAll arrows fired will go through blocks");
        Item.damage = 135;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 34;
        Item.height = 100;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4.25f;
        Item.value = 2000000;
        Item.rare = 10;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = 10;
        Item.shootSpeed = 30f;
        Item.useAmmo = 40;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Cog, 50);
        recipe.AddIngredient(ItemID.LunarBar, 15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
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
    	int num130 = 15;
		if (Main.rand.Next(3) == 0)
		{
			num130++;
		}
		if (Main.rand.Next(4) == 0)
		{
			num130++;
		}
		if (Main.rand.Next(5) == 0)
		{
			num130++;
		}
		for (int num131 = 0; num131 < num130; num131++)
		{
			vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y);
			vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
			vector2.Y -= (float)(100 * num131);
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
			float speedX4 = num78 + (float)Main.rand.Next(-600, 601) * 0.02f;
			float speedY5 = num79 + (float)Main.rand.Next(-600, 601) * 0.02f;
			int projectile = Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, speedY5, type, num73, num74, i, 0f, 0f);
			Main.projectile[projectile].tileCollide = false;
		}
		return false;
	}
}}