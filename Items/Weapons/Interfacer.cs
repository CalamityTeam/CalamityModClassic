using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class Interfacer : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Disseminator");
			// Tooltip.SetDefault("50% chance to not consume ammo");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 55;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 66;
	        Item.height = 24;
	        Item.useTime = 21;
	        Item.useAnimation = 21;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 4.5f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item38;
	        Item.autoReuse = true;
	        Item.shootSpeed = 13f;
	        Item.shoot = 10;
	        Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) < 50)
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
	            int bullet = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                Main.projectile[bullet].extraUpdates += 1;
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
			int num107 = Main.rand.Next(4, 6);
			for (int num108 = 0; num108 < num107; num108++)
			{
				vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(51) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
				vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-50, 51);
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
				float speedY5 = num79 + (float)Main.rand.Next(-30, 31) * 0.02f;
				int bullet = Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX4, speedY5, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                Main.projectile[bullet].extraUpdates += 1;
                Main.projectile[bullet].tileCollide = false;

                vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(51) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - /* - */ player.position.X), player.MountedCenter.Y + 600f); //-
                vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-50, 51); //200
                vector2.Y += (float)(100 * num108); //-=
                num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X; //+ -
                num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y; //+ -
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
                float speedX6 = num78 + (float)Main.rand.Next(-30, 31) * 0.02f;
                float speedY7 = num79 + (float)Main.rand.Next(-30, 31) * 0.02f;
                int bullet2 = Projectile.NewProjectile(source, vector2.X, vector2.Y, speedX6, -speedY7, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                Main.projectile[bullet2].extraUpdates += 1;
                Main.projectile[bullet2].tileCollide = false;
            }
	        return false;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "ConferenceCall");
	        recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}