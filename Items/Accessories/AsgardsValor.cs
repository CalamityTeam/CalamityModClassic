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
    [AutoloadEquip(EquipType.Shield)]
    public class AsgardsValor : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Asgard's Valor");
            /* Tooltip.SetDefault("Grants immunity to fire blocks and knockback\n" +
                "Immune to most debuffs including Brimstone Flames, Holy Flames, and Glacial State\n" +
                "10% damage reduction while submerged in liquid\n" +
                "+20 max life\n" +
                "Grants an improved holy dash\n" +
                "Can be used to ram enemies\n" +
                "Toggle visibility of this accessory to enable/disable the dash"); */
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 44;
            Item.value = Item.buyPrice(0, 45, 0, 0);
            Item.rare = 9;
            Item.defense = 8;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            if (!hideVisual) { modPlayer.dashMod = 2; }
            player.buffImmune[46] = true;
            player.buffImmune[44] = true;
            player.noKnockback = true;
            player.fireWalk = true;
            player.buffImmune[33] = true;
            player.buffImmune[36] = true;
            player.buffImmune[30] = true;
            player.buffImmune[20] = true;
            player.buffImmune[32] = true;
            player.buffImmune[31] = true;
            player.buffImmune[35] = true;
            player.buffImmune[23] = true;
            player.buffImmune[22] = true;
            player.buffImmune[Mod.Find<ModBuff>("BrimstoneFlames").Type] = true;
            player.buffImmune[Mod.Find<ModBuff>("HolyLight").Type] = true;
            player.buffImmune[Mod.Find<ModBuff>("GlacialState").Type] = true;
            player.statLifeMax2 += 20;
            if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir)) { player.endurance += 0.1f; }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AnkhShield);
            recipe.AddIngredient(null, "OrnateShield");
            recipe.AddIngredient(null, "ShieldoftheOcean");
            recipe.AddIngredient(null, "Abaddon");
            recipe.AddIngredient(null, "CoreofEleum", 3);
            recipe.AddIngredient(null, "CoreofCinder", 3);
            recipe.AddIngredient(null, "CoreofChaos", 3);
            recipe.AddIngredient(ItemID.LifeFruit, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}