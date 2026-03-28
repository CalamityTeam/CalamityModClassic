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
    [AutoloadEquip(EquipType.Shoes)]
    public class AngelTreads : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Angel Treads");
            /* Tooltip.SetDefault("Extreme speed!\n" +
                               "Greater mobility on ice\n" +
                               "Water and lava walking\n" +
                               "Temporary immunity to lava"); */
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 8;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.harpyRing = true;
            player.accRunSpeed = 8f;
            player.rocketBoots = 3;
            player.moveSpeed += 0.16f;
            player.iceSkate = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaMax += 240;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FrostsparkBoots);
            recipe.AddIngredient(ItemID.LavaWaders);
            recipe.AddIngredient(null, "HarpyRing");
            recipe.AddIngredient(null, "EssenceofCinder", 5);
            recipe.AddIngredient(null, "AerialiteBar", 20);
            recipe.AddIngredient(ItemID.SoulofMight);
            recipe.AddIngredient(ItemID.SoulofSight);
            recipe.AddIngredient(ItemID.SoulofFright);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}