using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Permafrost
{
	public class EternalBlizzard : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eternal Blizzard");
			// Tooltip.SetDefault("Wooden arrows turn into icicle arrows that shatter on impact");
		}
		public override void SetDefaults()
		{
			Item.damage = 48;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 42;
			Item.height = 62;
			Item.useTime = 19;
            Item.useAnimation = 19;
			Item.useStyle = 5;
			Item.useTurn = false;
			Item.noMelee = true;
			Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
			Item.useAmmo = AmmoID.Arrow;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("IcicleArrow").Type;
            Item.shootSpeed = 11f;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
                type = Mod.Find<ModProjectile>("IcicleArrow").Type;

            return true;
        }
    }
}
