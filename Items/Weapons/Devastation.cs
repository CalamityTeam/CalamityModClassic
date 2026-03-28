using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class Devastation : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Devastation");
			// Tooltip.SetDefault("Remnant of the big bang");
		}

		public override void SetDefaults()
		{
			Item.width = 72;
			Item.damage = 114;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 24;
			Item.useTime = 24;
			Item.useTurn = true;
			Item.useStyle = 1;
			Item.knockBack = 4.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 72;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("GalaxyBlast").Type;
			Item.shootSpeed = 16f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			switch (Main.rand.Next(6))
			{
	    		case 1: type = Mod.Find<ModProjectile>("GalaxyBlast").Type; break;
	    		case 2: type = Mod.Find<ModProjectile>("GalaxyBlastType2").Type; break;
	    		case 3: type = Mod.Find<ModProjectile>("GalaxyBlastType3").Type; break;
	    		default: break;
			}
	       	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, Main.myPlayer);
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
			int num107 = 4;
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
				float speedX4 = num78 + (float)Main.rand.Next(-40, 41) * 0.02f;
				float speedY5 = num79 + (float)Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, speedY5, Mod.Find<ModProjectile>("GalaxyBlast").Type, damage, knockback, player.whoAmI, 0f, (float)Main.rand.Next(5));
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, speedY5, Mod.Find<ModProjectile>("GalaxyBlastType2").Type, damage, knockback, player.whoAmI, 0f, (float)Main.rand.Next(3));
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, speedY5, Mod.Find<ModProjectile>("GalaxyBlastType3").Type, damage, knockback, player.whoAmI, 0f, (float)Main.rand.Next(1));
			}
	    	return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "CatastropheClaymore");
			recipe.AddIngredient(ItemID.LunarBar, 5);
			recipe.AddIngredient(null, "AstralBar", 10);
			recipe.AddIngredient(ItemID.MeteoriteBar, 10);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(3) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 73);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
	    	target.AddBuff(BuffID.CursedInferno, 200);
	    	target.AddBuff(BuffID.OnFire, 200);
	    	target.AddBuff(BuffID.Frostburn, 200);
		}
	}
}
