using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
    public class AlchemicalFlask : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Alchemical Flask");
            /* Tooltip.SetDefault("All attacks inflict the plague\n" +
                "Makes you immune to the plague\n" +
                "Projectiles spawn plague seekers on enemy hits"); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 24, 0, 0);
            Item.rare = 8;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.alchFlask = true;
            player.buffImmune[Mod.Find<ModBuff>("Plague").Type] = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(ItemID.Bezoar);
            recipe.AddIngredient(null, "PlagueCellCluster", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}