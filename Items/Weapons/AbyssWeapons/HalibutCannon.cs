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
	public class HalibutCannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Halibut Cannon");
            /* Tooltip.SetDefault("This weapon is overpowered, use at the risk of ruining your playthrough\n" +
				"Revengeance drop"); */
        }

	    public override void SetDefaults()
	    {
			Item.damage = 6;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 108;
			Item.height = 54;
			Item.useTime = 10;
			Item.useAnimation = 20;
			Item.useStyle = 5;
            Item.rare = 10;
			Item.noMelee = true;
			Item.knockBack = 1f;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.UseSound = SoundID.Item38;
			Item.autoReuse = true;
			Item.shoot = 10;
			Item.shootSpeed = 12f;
			Item.useAmmo = 97;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 0);
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.hardMode) { Item.damage = 12; }
            if (NPC.downedMoonlord) { Item.damage = 24; }
            int num6 = Main.rand.Next(25, 36);
            for (int index = 0; index < num6; ++index)
            {
                float num7 = velocity.X;
                float num8 = velocity.Y;
                float SpeedX = velocity.X + (float)Main.rand.Next(-10, 11) * 0.05f;
                float SpeedY = velocity.Y + (float)Main.rand.Next(-10, 11) * 0.05f;
                int shot = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                Main.projectile[shot].timeLeft = 180;
            }
            return false;
        }
	}
}