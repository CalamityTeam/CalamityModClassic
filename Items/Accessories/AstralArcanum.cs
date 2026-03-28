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
    public class AstralArcanum : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Astral Arcanum");
            /* Tooltip.SetDefault("Taking damage drops astral stars from the sky\n" +
                               "Provides immunity to the god slayer inferno debuff\n" +
                               "You have a 5% chance to reflect projectiles when they hit you\n" +
                               "If this effect triggers you get healed for the projectile's damage\n" +
                               "Boosts life regen even while under the effects of a damaging debuff\n" +
                               "While under the effects of a damaging debuff you will gain 20 defense\n" +
                               "Press O to toggle teleportation UI"); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 90, 0, 0);
            Item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(0, 255, 0);
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.astralArcanum = true;
            modPlayer.aBulwark = true;
            modPlayer.projRef = true;
            player.buffImmune[Mod.Find<ModBuff>("GodSlayerInferno").Type] = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "CelestialJewel");
            recipe.AddIngredient(null, "AstralBulwark");
            recipe.AddIngredient(null, "ArcanumoftheVoid");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}