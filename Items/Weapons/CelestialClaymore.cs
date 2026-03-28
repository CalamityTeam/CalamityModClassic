using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class CelestialClaymore : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Celestial Claymore");
			// Tooltip.SetDefault("Spawns cosmic energy flames near the player that generate large explosions after 2 seconds");
		}

		public override void SetDefaults()
		{
			Item.width = 50;
			Item.damage = 63;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 23;
			Item.useTime = 23;
			Item.useTurn = true;
			Item.useStyle = 1;
			Item.knockBack = 5.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 50;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
			Item.shoot = Mod.Find<ModProjectile>("CosmicSpiritBomb1").Type;
			Item.shootSpeed = 0.1f;
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
			int num107 = 3;
			for (int num108 = 0; num108 < num107; num108++)
			{
				vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y);
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
				switch (Main.rand.Next(3))
				{
		    		case 0: type = Mod.Find<ModProjectile>("CosmicSpiritBomb1").Type; break;
		    		case 1: type = Mod.Find<ModProjectile>("CosmicSpiritBomb2").Type; break;
		    		case 2: type = Mod.Find<ModProjectile>("CosmicSpiritBomb3").Type; break;
		    		default: break;
				}
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, 0f, 0f, type, damage, knockback, player.whoAmI, 0f, (float)Main.rand.Next(3));
			}
	    	return false;
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(4) == 0)
			{
				int num249 = Main.rand.Next(2);
				if (num249 == 0)
				{
					num249 = 15;
				}
				else if (num249 == 1)
				{
					num249 = 73;
				}
				else
				{
					num249 = 244;
				}
				int num250 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, num249, (float)(player.direction * 2), 0f, 150, default(Color), 1.3f);
				Main.dust[num250].velocity *= 0.2f;
			}
	    }
	}
}
