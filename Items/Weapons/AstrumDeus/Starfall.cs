using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.AstrumDeus
{
	public class Starfall : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Starfall");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 39;
            Item.crit += 25;
            Item.DamageType = DamageClass.Magic;
	        Item.mana = 15;
            Item.rare = 7;
	        Item.width = 28;
	        Item.height = 30;
	        Item.useTime = 14;
	        Item.useAnimation = 14;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 3.25f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.UseSound = SoundID.Item105;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("AstralStarMagic").Type;
	        Item.shootSpeed = 12f;
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
            if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
            {
                num78 = (float)player.direction;
                num79 = 0f;
            }
            float num208 = num78;
            float num209 = num79;
            num208 += (float)Main.rand.Next(-1, 2) * 0.5f;
            num209 += (float)Main.rand.Next(-1, 2) * 0.5f;
            vector2 += new Vector2(num208, num209);
            for (int num108 = 0; num108 < 5; num108++)
            {
                float speedX4 = 2f + (float)Main.rand.Next(-8, 5);
                float speedY5 = 15f + (float)Main.rand.Next(1, 6);
                int star = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, speedY5, type, damage, knockback, player.whoAmI, 0f, 0f);
            }
            return false;
	    }
	}
}