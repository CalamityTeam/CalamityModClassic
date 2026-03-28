using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class Murasama : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Murasama");
			/* Tooltip.SetDefault("There will be blood!\n" +
                "ID and power-level locked.  Prove your strength or have the correct user ID to wield this sword."); */
		}

		public override void SetDefaults()
		{
			Item.width = 72;
			Item.damage = 999;
            Item.crit += 30;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.useAnimation = 25;
			Item.useStyle = 5;
			Item.useTime = 5;
			Item.knockBack = 6.5f;
			Item.autoReuse = false;
			Item.height = 78;
            Item.value = Item.buyPrice(2, 50, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("Murasama").Type;
			Item.shootSpeed = 15f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 15;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            bool yharon = CalamityWorldPreTrailer.downedYharon;
            if (player.name == "Sam" || player.name == "Samuel Rodrigues" || yharon)
            {
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
                return false;
            }
            else
            {
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, 0f, 0f, 504, 6, 0f, player.whoAmI, 0.0f, 0.0f);
                return false;
            }
        }
    }
}
