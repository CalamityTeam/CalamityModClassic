using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class Vortexpopper : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Vortexpopper");
		}

    public override void SetDefaults()
    {
        Item.damage = 28;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 46;
        Item.height = 22;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 3.25f;
        Item.value = 1000000;
        Item.rare = ItemRarityID.Cyan;
        Item.UseSound = SoundID.Item95;
        Item.autoReuse = true;
        Item.shootSpeed = 50f;
        Item.shoot = ProjectileID.Xenopopper;
        Item.useAmmo = 97;
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(-5, 0);
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float num72 = Item.shootSpeed;
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
    	Vector2 value6 = Vector2.Normalize(new Vector2(num78, num79)) * 40f * Item.scale;
		if (Collision.CanHit(vector2, 0, 0, vector2 + value6, 0, 0)) 
		{
			vector2 += value6;
		}
		float ai = new Vector2(num78, num79).ToRotation();
		float num96 = 2.09439516f;
		int num97 = Main.rand.Next(6, 10);
		if (Main.rand.NextBool(4))
		{
			num97++;
		}
		for (int num98 = 0; num98 < num97; num98++) 
		{
			float scaleFactor2 = (float)Main.rand.NextDouble() * 0.2f + 0.05f;
			Vector2 vector6 = new Vector2(num78, num79).RotatedBy((double)(num96 * (float)Main.rand.NextDouble() - num96 / 2f), default(Vector2)) * scaleFactor2;
			int num99 = Projectile.NewProjectile(source, position.X, position.Y, vector6.X, vector6.Y, 444, damage, knockback, player.whoAmI, ai, 0f);
			Main.projectile[num99].localAI[0] = (float)type;
			Main.projectile[num99].localAI[1] = 12f;
		}
		int num107 = Main.rand.Next(5, 10);
		for (int num108 = 0; num108 < num107; num108++)
		{
			vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
			vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-800, 801);
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
			float speedX4 = num78 + (float)Main.rand.Next(-1000, 1001) * 0.02f;
			float speedY4 = num79 + (float)Main.rand.Next(-1000, 1001) * 0.02f;
			int projectile = Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, speedY4, 444, damage, knockback, player.whoAmI, ai, 0f);
			Main.projectile[projectile].localAI[0] = (float)type;
			Main.projectile[projectile].localAI[1] = 12f;
		}
		return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Xenopopper);
        recipe.AddIngredient(ItemID.FragmentVortex, 20);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}}