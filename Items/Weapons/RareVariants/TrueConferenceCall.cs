using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.RareVariants
{
	public class TrueConferenceCall : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Conference Call");
			// Tooltip.SetDefault("@everyone");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 30;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 66;
	        Item.height = 26;
	        Item.useTime = 24;
	        Item.useAnimation = 24;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 4.5f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
	        Item.UseSound = SoundID.Item38;
	        Item.autoReuse = true;
	        Item.shootSpeed = 18f;
	        Item.shoot = 10;
	        Item.useAmmo = 97;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
			int bulletAmt = 6;
	        for (int index = 0; index < bulletAmt; ++index)
	        {
				float SpeedX = velocity.X + (float)Main.rand.Next(-15, 16) * 0.05f;
				float SpeedY = velocity.Y + (float)Main.rand.Next(-15, 16) * 0.05f;
	            int proj = Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0f, 0f);
				Main.projectile[proj].extraUpdates += 1;
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
			for (int num108 = 0; num108 < bulletAmt; num108++)
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
				float speedX4 = num78 + (float)Main.rand.Next(-15, 16) * 0.01f;
				float speedY5 = num79 + (float)Main.rand.Next(-15, 16) * 0.01f;
				int proj = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, speedY5, type, damage, knockback, player.whoAmI, 0f, 0f);
				Main.projectile[proj].extraUpdates += 1;
			}
	        return false;
	    }
	}
}