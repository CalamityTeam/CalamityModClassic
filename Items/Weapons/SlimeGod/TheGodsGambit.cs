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
    public class TheGodsGambit : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The God's Gambit");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(3291);
            Item.damage = 28;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = 5;
            Item.channel = true;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.knockBack = 3.5f;
            Item.value = Item.buyPrice(0, 12, 0, 0);
            Item.rare = 4;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("TheGodsGambitProjectile").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "PurifiedGel", 30);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}