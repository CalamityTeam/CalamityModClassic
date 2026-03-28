using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class YinYo : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("YinYo");
            // Tooltip.SetDefault("Fires light or dark shards when enemies are near\nLight shards fly up and down while dark shards fly left and right");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Chik);
            Item.damage = 34;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = 5;
            Item.channel = true;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.knockBack = 3.2f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("YinYo").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DarkShard);
            recipe.AddIngredient(ItemID.LightShard);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}