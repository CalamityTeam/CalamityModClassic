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
	public class HellwingStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Hellwing Staff");
			//Tooltip.SetDefault("Unleashes pillars of flame");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 18;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 18;
	        Item.width = 54;
	        Item.height = 52;
	        Item.useTime = 33;
	        Item.useAnimation = 33;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 5;
	        Item.value = 55000;
	        Item.rare = ItemRarityID.LightRed;
	        Item.UseSound = SoundID.Item43;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("FlamePillar").Type;
	        Item.shootSpeed = 9f;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.LavaBucket, 2);
	        recipe.AddIngredient(ItemID.HellstoneBar, 7);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
			float num72 = Item.shootSpeed;
	    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
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
	    	int num130 = 7;
			if (Main.rand.NextBool(6))
			{
				num130++;
			}
			if (Main.rand.NextBool(7))
			{
				num130++;
			}
			if (Main.rand.NextBool(8))
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
				float speedX4 = num78 + (float)Main.rand.Next(-40, 41) * 0.02f;
				float speedY4 = num79 + (float)Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, speedY4, Mod.Find<ModProjectile>("FlamePillar").Type, damage, knockback, player.whoAmI, 0f, 0f);
			}
			return false;
		}
	}
}