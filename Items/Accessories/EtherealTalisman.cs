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
    public class EtherealTalisman : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ethereal Talisman");
            /* Tooltip.SetDefault("15% increased magic damage, 5% increased magic critical strike chance, and 10% decreased mana usage\n" +
                "+150 max mana\n" +
                "Reveals treasure locations\n" +
                "Reduces the cooldown of healing potions\n" +
                "You automatically use mana potions when needed\n" +
                "Magic attacks have a chance to instantly kill normal enemies"); */
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 90, 0, 0);
            Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 20;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.eTalisman = true;
            player.findTreasure = true;
            player.pStone = true;
            player.manaFlower = true;
            player.statManaMax2 += 150;
            player.GetDamage(DamageClass.Magic) += 0.15f;
            player.manaCost *= 0.9f;
            player.GetCritChance(DamageClass.Magic) += 5;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SigilofCalamitas");
            recipe.AddIngredient(ItemID.ManaFlower);
            recipe.AddIngredient(null, "Phantoplasm", 20);
            recipe.AddIngredient(null, "NightmareFuel", 20);
            recipe.AddIngredient(null, "EndothermicEnergy", 20);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}