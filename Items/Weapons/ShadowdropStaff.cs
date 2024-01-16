using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ShadowdropStaff : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/2.HiveMind/ShadowdropStaff");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Shadowdrop Staff");
        Item.damage = 19;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 13;
        Item.width = 48;
        Item.height = 48;
        Item.useTime = 9;
        Item.useAnimation = 18;
        Item.useStyle = 5;
        Item.staff[Item.type] = true;
        ////Tooltip.SetDefault("Summons dark aura rain from the sky");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 2.25f;
        Item.value = 50000;
        Item.rare = 2;
        Item.UseSound = SoundID.Item66;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("AuraRain").Type;
        Item.shootSpeed = 8f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.RottenChunk, 5);
        recipe.AddIngredient(ItemID.DemoniteBar, 5);
        recipe.AddIngredient(null, "TrueShadowScale", 15);
        recipe.AddTile(TileID.DemonAltar);
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
		int num107 = 10;
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
			float speedX4 = num78 + (float)Main.rand.Next(-120, 121) * 0.02f;
			float speedY5 = num79 + (float)Main.rand.Next(-120, 121) * 0.02f;
			Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, speedY5, Mod.Find<ModProjectile>("AuraRain").Type, num73, num74, i, 0f, (float)Main.rand.Next(10));
		}
		return false;
	}
}}