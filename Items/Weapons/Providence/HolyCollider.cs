using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Providence
{
    public class HolyCollider : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Holy Collider");
            // Tooltip.SetDefault("Striking enemies will cause them to explode into holy fire");
        }

        public override void SetDefaults()
        {
            Item.width = 94;
            Item.damage = 400;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 22;
            Item.useStyle = 1;
            Item.useTime = 22;
            Item.useTurn = true;
            Item.knockBack = 7.75f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 80;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.shootSpeed = 10f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(Item.shootSpeed, Item.shootSpeed) - spread / 2;
            double deltaAngle = spread / 8f;
            double offsetAngle;
            int i;
            for (i = 0; i < 4; i++)
            {
                offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("HolyColliderHolyFire").Type, (int)((double)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative) * 0.3), Item.knockBack, Main.myPlayer);
                Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), Mod.Find<ModProjectile>("HolyColliderHolyFire").Type, (int)((double)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative) * 0.3), Item.knockBack, Main.myPlayer);
            }
        }
    }
}
