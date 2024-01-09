using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class ConferenceCall : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Conclave Crossfire");
			//Tooltip.SetDefault("50% chance to not consume ammo");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 35;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 66;
	        Item.height = 26;
	        Item.useTime = 26;
	        Item.useAnimation = 26;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 4.5f;
	        Item.value = 300000;
	        Item.rare = ItemRarityID.Yellow;
	        Item.UseSound = SoundID.Item38;
	        Item.autoReuse = true;
	        Item.shootSpeed = 13f;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.useAmmo = 97;
	    }
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 50)
	    		return false;
	    	return true;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
	        int num6 = Main.rand.Next(4, 6);
	        for (int index = 0; index < num6; ++index)
	        {
	            float SpeedX = velocity.X + (float) Main.rand.Next(-30, 31) * 0.05f;
	            float SpeedY = velocity.Y + (float) Main.rand.Next(-30, 31) * 0.05f;
	            Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	        }
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
			int num107 = Main.rand.Next(4, 6);
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
				float speedX4 = num78 + (float)Main.rand.Next(-30, 31) * 0.02f;
				float speedY4 = num79 + (float)Main.rand.Next(-30, 31) * 0.02f;
				Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, speedY4, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			}
	        return false;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.TacticalShotgun);
	        recipe.AddIngredient(null, "CoreofChaos", 7);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}