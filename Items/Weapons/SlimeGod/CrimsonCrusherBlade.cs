using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.SlimeGod
{
    public class CrimsonCrusherBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crimson Crusher Blade");
        }

        public override void SetDefaults()
        {
            Item.damage = 41;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 68;
            Item.height = 76;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useTurn = true;
            Item.useStyle = 1;
            Item.knockBack = 7f;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(7) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 5);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "EbonianGel", 15);
            recipe.AddIngredient(ItemID.CrimstoneBlock, 50);
            recipe.AddIngredient(ItemID.TissueSample, 5);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 4);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}