using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using Terraria.Audio;

namespace CalamityModClassicPreTrailer.Items.Weapons.Providence
{
	public class PurgeGuzzler : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Purge Guzzler");
			// Tooltip.SetDefault("Fires 3 beams of holy energy");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 80;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 12;
			Item.width = 58;
			Item.height = 44;
			Item.useTime = 12;
			Item.useAnimation = 12;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 4.5f;
			Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/LaserCannon");
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("HolyLaser").Type;
			Item.shootSpeed = 6f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numProj = 2;
            float rotation = MathHelper.ToRadians(4);
            for (int i = 0; i < numProj + 1; i++)
            {
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProj - 1)));
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI, 0f, 0f);
            }
            return false;
        }
	}
}