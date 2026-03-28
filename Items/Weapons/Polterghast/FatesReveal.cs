using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Polterghast
{
	public class FatesReveal : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fate's Reveal");
			// Tooltip.SetDefault("Spawns ghostly fire that follows the player");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 60;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 20;
	        Item.width = 68;
	        Item.height = 72;
	        Item.useTime = 16;
	        Item.useAnimation = 16;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5.5f;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item20;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("FatesReveal").Type;
	        Item.shootSpeed = 1f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	    {
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
            float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
            if (player.gravDir == -1f)
            {
                num79 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector.Y;
            }
            float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
            float num81 = num80;
            if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
            {
                num78 = (float)player.direction;
                num79 = 0f;
                num80 = Item.shootSpeed;
            }
            else
            {
                num80 = Item.shootSpeed / num80;
            }
            vector += new Vector2(num78, num79);
            int num107 = 5;
			for (int num108 = 0; num108 < num107; num108++)
			{
				vector.X = vector.X + (float)Main.rand.Next(-100, 101);
				vector.Y += (float)(Main.rand.Next(-25, 26) * num108);
                float spawnX = vector.X;
                float spawnY = vector.Y;
				Projectile.NewProjectile(Entity.GetSource_FromThis(null),spawnX, spawnY, 0f, 0f, type, damage, knockback, player.whoAmI, 0f, (float)Main.rand.Next(3));
			}
	    	return false;
		}
	}
}