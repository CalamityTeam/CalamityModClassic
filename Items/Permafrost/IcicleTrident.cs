using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Permafrost
{
	public class IcicleTrident : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Icicle Trident");
			// Tooltip.SetDefault("Shoots piercing icicles");
            Item.staff[Item.type] = true;
		}
		public override void SetDefaults()
		{
			Item.damage = 69;
			Item.DamageType = DamageClass.Magic;
            Item.mana = 21;
			Item.width = 64;
			Item.height = 64;
			Item.useTime = 25;
            Item.useAnimation = 25;
			Item.useStyle = 5;
			Item.useTurn = false;
			Item.noMelee = true;
			Item.knockBack = 7f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
			Item.UseSound = SoundID.Item8;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("TridentIcicle").Type;
            Item.shootSpeed = 12f;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 speed = new Vector2(velocity.X, velocity.Y);
            Projectile.NewProjectile(Entity.GetSource_FromThis(null),position, speed, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(Entity.GetSource_FromThis(null),position, speed.RotatedBy(MathHelper.ToRadians(5)), type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position, speed.RotatedBy(MathHelper.ToRadians(-5)), type, damage, knockback, player.whoAmI);
            return false;
        }
    }
}
