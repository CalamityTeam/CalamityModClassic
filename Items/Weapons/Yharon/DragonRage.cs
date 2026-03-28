using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons.Yharon
{
    public class DragonRage : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Rage");
        }

        public override void SetDefaults()
        {
            Item.width = 68;
            Item.damage = 800;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.useTurn = true;
            Item.useStyle = 1;
            Item.knockBack = 7.5f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 80;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("DragonRage").Type;
            Item.shootSpeed = 14f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float SpeedX = velocity.X + 10f * 0.05f;
            float SpeedY = velocity.Y + 10f * 0.05f;
            float SpeedX2 = velocity.X - 10f * 0.05f;
            float SpeedY2 = velocity.Y - 10f * 0.05f;
            float SpeedX3 = velocity.X + 0f * 0.05f;
            float SpeedY3 = velocity.Y + 0f * 0.05f;
            float SpeedX4 = velocity.X - 20f * 0.05f;
            float SpeedY4 = velocity.Y - 20f * 0.05f;
            float SpeedX5 = velocity.X + 20f * 0.05f;
            float SpeedY5 = velocity.Y + 20f * 0.05f;
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage * 0.75f), knockback, player.whoAmI, 0.0f, 0.0f);
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX2, SpeedY2, type, (int)((double)damage * 0.75f), knockback, player.whoAmI, 0.0f, 0.0f);
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX3, SpeedY3, type, (int)((double)damage * 0.75f), knockback, player.whoAmI, 0.0f, 0.0f);
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX4, SpeedY4, type, (int)((double)damage * 0.75f), knockback, player.whoAmI, 0.0f, 0.0f);
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX5, SpeedY5, type, (int)((double)damage * 0.75f), knockback, player.whoAmI, 0.0f, 0.0f);
            return false;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 244);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Daybreak, 360);
        }
    }
}
