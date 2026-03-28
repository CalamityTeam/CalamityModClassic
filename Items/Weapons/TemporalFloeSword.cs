using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class TemporalFloeSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Temporal Floe Sword");
            // Tooltip.SetDefault("The iceman cometh...");
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.damage = 85;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 16;
            Item.useStyle = 1;
            Item.useTime = 16;
            Item.useTurn = true;
            Item.knockBack = 6;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 58;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
            Item.shoot = Mod.Find<ModProjectile>("TemporalFloeSwordProjectile").Type;
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "CryoBar", 15);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 34);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next(3) == 0)
            {
                target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 120);
            }
            target.AddBuff(BuffID.Frostburn, 600);
        }
    }
}
