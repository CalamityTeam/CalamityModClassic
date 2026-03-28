using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
    public class StatisBeltOfCurses : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Statis' Belt of Curses");
            /* Tooltip.SetDefault("Increases jump speed and allows constant jumping\n" +
                "Can climb walls, dash, and dodge attacks\n" +
                "10% increased rogue damage and velocity\n" +
                "5% increased rogue crit chance\n" +
                "Increased max minions by 3 and 10% increased minion damage\n" +
                "Increased minion knockback\n" +
                "Grants shadowflame powers to all minions\n" +
                "Minions make enemies cry on hit\n" +
                "Minion attacks have a chance to instantly kill normal enemies"); */
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 90, 0, 0);
            Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.statisBeltOfCurses = true;
            modPlayer.shadowMinions = true;
            modPlayer.tearMinions = true;
            player.GetKnockback(DamageClass.Summon).Base += 2.5f;
            player.GetDamage(DamageClass.Summon) += 0.1f;
            player.maxMinions += 3;
            player.autoJump = true;
            player.jumpSpeedBoost += 1.2f;
            player.extraFall += 50;
            player.blackBelt = true;
            player.dash = 1;
            player.spikedBoots = 2;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.1f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 5;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingVelocity += 0.1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "StatisNinjaBelt");
            recipe.AddIngredient(null, "StatisCurse");
            recipe.AddIngredient(null, "Phantoplasm", 20);
            recipe.AddIngredient(null, "NightmareFuel", 20);
            recipe.AddIngredient(null, "EndothermicEnergy", 20);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}