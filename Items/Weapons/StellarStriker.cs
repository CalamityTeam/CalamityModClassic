using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class StellarStriker : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Stellar Striker");
			// Tooltip.SetDefault("Summons a swarm of lunar flares from the sky on enemy hits");
		}

		public override void SetDefaults()
		{
			Item.width = 86;
			Item.damage = 640;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 25;
			Item.useStyle = 1;
			Item.useTime = 25;
			Item.useTurn = true;
			Item.knockBack = 7.75f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 86;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			if (target.type == NPCID.TargetDummy)
			{
				return;
			}
			SoundEngine.PlaySound(SoundID.Item88, player.position);
			int i = Main.myPlayer;
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
			int num112 = 2;
			for (int num113 = 0; num113 < num112; num113++) 
			{
				vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(Main.rand.Next(201) * -(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
				vector2.X = (vector2.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
				vector2.Y -= (float)(100 * num113);
				num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X + (float)Main.rand.Next(-40, 41) * 0.03f;
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
				float num114 = num78;
				float num115 = num79 + (float)Main.rand.Next(-80, 81) * 0.02f;
				int proj = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, num114, num115, 645, (int)((double)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative) * 0.5), Item.knockBack, i, 0f, (float)Main.rand.Next(3));
				Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
			}
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(3) == 0)
	        {
	            Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 229);
	        }
	    }
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "CometQuasher");
			recipe.AddIngredient(ItemID.LunarBar, 10);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	}
}
