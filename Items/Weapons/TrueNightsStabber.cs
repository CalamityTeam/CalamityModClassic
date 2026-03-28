using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class TrueNightsStabber : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Night's Stabber");
            // Tooltip.SetDefault("Don't underestimate the power of stabby knives");
        }

        public override void SetDefaults()
        {
            Item.useStyle = 3;
            Item.useTurn = false;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.width = 40;
            Item.height = 40;
            Item.damage = 75;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.knockBack = 6.25f;
            Item.UseSound = SoundID.Item1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("ShortNightBeam").Type;
            Item.shootSpeed = 8f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "NightsStabber");
            recipe.AddIngredient(ItemID.BrokenHeroSword);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 21);
            }
        }
    }
}
