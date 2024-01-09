using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Leviathan
{
    public class Greentide : ModItem
    {
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Greentide");
			//Tooltip.SetDefault("Summons green blasts of water from the sky on enemy hits");
		}

        public override void SetDefaults()
        {
            Item.damage = 84;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 60;
            Item.height = 60;
            Item.useTime = 24;
            Item.useAnimation = 24;
			Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7;
            Item.value = 750000;
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shootSpeed = 18f;
        }
        
        public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "IOU");
	        recipe.AddIngredient(null, "LivingShard");
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
        
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            int i = Main.myPlayer;
			float num72 = Item.shootSpeed;
			int num73 = hit.Damage;
			float num74 = hit.Knockback;
	    	num74 = player.GetWeaponKnockback(Item, num74);
	    	player.itemTime = Item.useTime;
	    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
	    	float num78 = (float)Main.mouseX - Main.screenPosition.X - vector2.X;
			float num79 = (float)Main.mouseY - Main.screenPosition.Y - vector2.Y;
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
				float speedX4 = num78;
				float speedY4 = num79 + (float)Main.rand.Next(-180, 181) * 0.02f;
				Projectile.NewProjectile(player.GetSource_FromThis(), vector2.X, vector2.Y, speedX4, speedY4, Mod.Find<ModProjectile>("GreenWater").Type, num73, num74, i, 0f, (float)Main.rand.Next(10));
			}
        }
        
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
        	int randomDust = Main.rand.Next(2);
			if (randomDust == 0)
			{
				randomDust = 33;
			}
			else
			{
				randomDust = 89;
			}
            if (Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, randomDust);
            }
        }
    }
}