using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.AbyssWeapons
{
	public class UndinesRetribution : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Undine's Retribution");
            Item.staff[Item.type] = true;
        }

	    public override void SetDefaults()
	    {
	        Item.damage = 30;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 15;
	        Item.width = 64;
	        Item.height = 64;
	        Item.useTime = 18;
	        Item.useAnimation = 18;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 3.5f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
	        Item.UseSound = SoundID.Item66;
	        Item.autoReuse = true;
	        Item.shootSpeed = 12f;
	        Item.shoot = Mod.Find<ModProjectile>("UndinesRetribution").Type;
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
            for (int num108 = 0; num108 < 3; num108++)
            {
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
                float speedX6 = num78 + (float)Main.rand.Next(-60, 61) * 0.02f;
                float speedY7 = num79 + (float)Main.rand.Next(-60, 61) * 0.02f;
                float ai1 = (Main.rand.NextFloat() + 0.5f);
                int bullet2 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX6, -speedY7, type, damage, knockback, player.whoAmI, 0.0f, ai1);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DepthCells", 30);
            recipe.AddIngredient(null, "Lumenite", 10);
            recipe.AddIngredient(null, "Tenebris", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}