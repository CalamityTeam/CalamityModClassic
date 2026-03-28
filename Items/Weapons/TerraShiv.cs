using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class TerraShiv : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Shiv");
            // Tooltip.SetDefault("Don't underestimate the power of shivs");
        }

        public override void SetDefaults()
        {
            Item.useStyle = 3;
            Item.useTurn = false;
            Item.useAnimation = 13;
            Item.useTime = 13;
            Item.width = 42;
            Item.height = 42;
            Item.damage = 125;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.knockBack = 6.5f;
            Item.UseSound = SoundID.Item1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("ShortTerraBeam").Type;
            Item.shootSpeed = 12f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "TrueNightsStabber");
            recipe.AddIngredient(null, "TrueExcaliburShortsword");
            recipe.AddIngredient(null, "LivingShard", 7);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 107);
            }
        }
    }
}
