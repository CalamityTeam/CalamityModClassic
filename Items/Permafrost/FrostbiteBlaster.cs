using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Permafrost
{
	public class FrostbiteBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Frostbite Blaster");
			// Tooltip.SetDefault("Fires a spread of icicles");
		}
		public override void SetDefaults()
		{
			Item.damage = 31;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 54;
			Item.height = 30;
			Item.useTime = 7;
            Item.useAnimation = 21;
            Item.reuseDelay = 54;
			Item.useStyle = 5;
			Item.useTurn = false;
			Item.noMelee = true;
			Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
			Item.useAmmo = AmmoID.Bullet;
			Item.UseSound = SoundID.Item36;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.Blizzard;
            Item.shootSpeed = 9f;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item36, position);

            type = ProjectileID.Blizzard;
            for (int i = 0; i < 2; i++)
            {
                float newSpeedX = velocity.X + Main.rand.Next(-40, 41) * 0.06f;
                float newSpeedY = velocity.Y + Main.rand.Next(-40, 41) * 0.06f;
                int p = Projectile.NewProjectile(Entity.GetSource_FromThis(null),position.X, position.Y, newSpeedX, newSpeedY, type, damage, knockback, player.whoAmI);
                Main.projectile[p].DamageType = DamageClass.Ranged;
            }
            return true;
        }
    }
}
