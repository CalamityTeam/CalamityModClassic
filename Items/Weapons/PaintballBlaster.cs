using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class PaintballBlaster : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/PaintballBlaster");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Speed Blaster");
        Item.damage = 37;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 68;
        Item.height = 42;
        ////Tooltip.SetDefault("Paint 'em like it's Mario Paint");
        Item.useAnimation = 24;
        Item.reuseDelay = 9;
        Item.useTime = 4;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 2.25f;
        Item.value = 300000;
        Item.rare = 5;
        Item.UseSound = null;
        Item.autoReuse = true;
        Item.shootSpeed = 20f;
        Item.shoot = 587;
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
    	float num208 = num78;
		float num209 = num79;
		num208 += (float)Main.rand.Next(-1, 2) * 0.5f;
		num209 += (float)Main.rand.Next(-1, 2) * 0.5f;
		if (Collision.CanHitLine(player.Center, 0, 0, vector2 + new Vector2(num208, num209) * 2f, 0, 0))
		{
			vector2 += new Vector2(num208, num209);
		}
		Projectile.NewProjectile(source, position.X, position.Y - player.gravDir * 4f, num208, num209, 587, num73, num74, i, 0f, (float)Main.rand.Next(12) / 6f);
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.PainterPaintballGun);
        recipe.AddIngredient(ItemID.SoulofSight, 5);
        recipe.AddIngredient(ItemID.HallowedBar, 9);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}