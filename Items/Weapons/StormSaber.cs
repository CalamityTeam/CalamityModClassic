using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class StormSaber : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Storm Saber");
        }

        public override void SetDefaults()
        {
            Item.width = 58;
            Item.damage = 42;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 23;
            Item.useTime = 23;
            Item.useTurn = true;
            Item.useStyle = 1;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 68;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
            Item.shoot = Mod.Find<ModProjectile>("StormBeam").Type;
            Item.shootSpeed = 12f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int num250 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 187, (float)(player.direction * 2), 0f, 150, default(Color), 1.3f);
                Main.dust[num250].velocity *= 0.2f;
            }
        }
    }
}
