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
    public class MeleeLevelMeter : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Melee Level Meter");
            // Tooltip.SetDefault("Tells you how high your melee proficiency is");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(0, 6, 0, 0);
            Item.rare = 1;
            Item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            int level = Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>().meleeLevel;
            int exactLevel = Main.player[Main.myPlayer].GetModPlayer<CalamityPlayerPreTrailer>().exactMeleeLevel;
            int damageGain = 0;
            int meleeSpeed = 0;
            int critGain = 0;
            switch (exactLevel)
            {
                case 0:
                    break;
                case 1:
                    damageGain = 1;
                    break;
                case 2:
                    damageGain = 2;
                    break;
                case 3:
                    damageGain = 2;
                    meleeSpeed = 1;
                    break;
                case 4:
                    damageGain = 3;
                    meleeSpeed = 1;
                    critGain = 1;
                    break;
                case 5:
                    damageGain = 3;
                    meleeSpeed = 2;
                    critGain = 1;
                    break;
                case 6:
                    damageGain = 4;
                    meleeSpeed = 3;
                    critGain = 1;
                    break;
                case 7:
                    damageGain = 4;
                    meleeSpeed = 4;
                    critGain = 2;
                    break;
                case 8:
                    damageGain = 5;
                    meleeSpeed = 5;
                    critGain = 2;
                    break;
                case 9:
                    damageGain = 5;
                    meleeSpeed = 5;
                    critGain = 3;
                    break;
                case 10:
                    damageGain = 6;
                    meleeSpeed = 6;
                    critGain = 3;
                    break;
                case 11:
                    damageGain = 7;
                    meleeSpeed = 7;
                    critGain = 4;
                    break;
                case 12:
                    damageGain = 8;
                    meleeSpeed = 8;
                    critGain = 4;
                    break;
                case 13:
                    damageGain = 9;
                    meleeSpeed = 9;
                    critGain = 5;
                    break;
                case 14:
                    damageGain = 10;
                    meleeSpeed = 10;
                    critGain = 5;
                    break;
                case 15:
                    damageGain = 12;
                    meleeSpeed = 12;
                    critGain = 6;
                    break;
            }
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "Tooltip0")
                {
                    line2.Text = "Tells you how high your melee proficiency is\n" +
                "While equipped you will gain melee proficiency faster\n" +
                "The higher your melee level the higher your melee damage, speed, and critical strike chance\n" +
                "Melee proficiency (max of 12500): " + (level - (level > 12500 ? 1 : 0)) + "\n" +
                "Melee level (max of 15): " + exactLevel + "\n" +
                "Melee damage increase: " + damageGain + "%\n" +
                "Melee speed increase: " + meleeSpeed + "%\n" +
                "Melee crit increase: " + critGain + "%";
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.fasterMeleeLevel = true;
        }
    }
}