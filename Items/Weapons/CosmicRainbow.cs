using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
//using TerrariaOverhaul;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class CosmicRainbow : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Rainbow");
			// Tooltip.SetDefault("Launch a barrage of rainbows!");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 105;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 30;
	        Item.width = 38;
	        Item.height = 66;
	        Item.useTime = 35;
	        Item.useAnimation = 35;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 0f;
            Item.value = Item.buyPrice(0, 95, 0, 0);
            Item.rare = 9;
	        Item.UseSound = SoundID.Item67;
	        Item.autoReuse = true;
	        Item.shoot = 250;
	        Item.shootSpeed = 18f;
	    }

        /*public void OverhaulInit()
        {
            this.SetTag("bow");
        }*/

        public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.RainbowGun);
	        recipe.AddIngredient(null, "MeldiateBar", 5);
	        recipe.AddIngredient(null, "CoreofCalamity");
	        recipe.AddIngredient(null, "BarofLife", 5);
	        recipe.AddTile(TileID.LunarCraftingStation);
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
				float speedX4 = num78 + (float)Main.rand.Next(-360, 361) * 0.02f;
				float speedY5 = num79 + (float)Main.rand.Next(-360, 361) * 0.02f;
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, speedY5, type, damage, knockback, player.whoAmI, 0f, 0f);
			}
			return false;
		}
	}
}