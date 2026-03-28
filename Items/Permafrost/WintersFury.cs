using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Permafrost
{
	public class WintersFury : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Winter's Fury");
			// Tooltip.SetDefault("The pages are freezing to the touch");
		}
		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Magic;
            Item.mana = 9;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 9;
            Item.useAnimation = 9;
			Item.useStyle = 5;
			Item.useTurn = false;
			Item.noMelee = true;
			Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
			Item.UseSound = SoundID.Item9;
            Item.scale = 0.9f;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("Icicle").Type;
            Item.shootSpeed = 12f;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.Next(3) != 0)
            {
                Vector2 speed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-15, 16)));
                speed.Normalize();
                speed *= 15f;
                speed.Y -= Math.Abs(speed.X) * 0.2f;
                int p = Projectile.NewProjectile(Entity.GetSource_FromThis(null),position, speed, Mod.Find<ModProjectile>("FrostShardFriendly").Type, damage, knockback, player.whoAmI);
                Main.projectile[p].minion = false;
                Main.projectile[p].DamageType = DamageClass.Magic;
            }
            if (Main.rand.Next(3) == 0)
            {
                SoundEngine.PlaySound(SoundID.Item1, position);
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X * 1.2f, velocity.Y * 1.2f, Mod.Find<ModProjectile>("Snowball").Type, damage, knockback * 2f, player.whoAmI);
            }
            velocity.X += Main.rand.Next(-40, 41) * 0.05f;
            velocity.Y += Main.rand.Next(-40, 41) * 0.05f;
            return true;
        }
    }
}
