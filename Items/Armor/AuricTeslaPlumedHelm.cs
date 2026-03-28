using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class AuricTeslaPlumedHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Auric Tesla Plumed Helm");
            /* Tooltip.SetDefault("20% increased rogue damage and critical strike chance\n" +
                               "Not moving boosts all damage and critical strike chance"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(1, 80, 0, 0);
			Item.defense = 34; //132
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 20;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("AuricTeslaBodyArmor").Type && legs.type == Mod.Find<ModItem>("AuricTeslaCuisses").Type;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Rogue Tarragon, Bloodflare, God Slayer, and Silva armor effects\n" +
                "All projectiles spawn healing auric orbs on enemy hits\n" +
                "Max run speed and acceleration boosted by 10%\n" +
                "Rogue weapon critical strikes will do 1.25 times damage while you are above 50% HP\n" +
				"Rogue stealth builds while not attacking and not moving, up to a max of 160\n" +
				"Rogue stealth only reduces when you attack, it does not reduce while moving\n" +
				"The higher your rogue stealth the higher your rogue damage, crit, and movement speed";
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.tarraSet = true;
            modPlayer.tarraThrowing = true;
            modPlayer.bloodflareSet = true;
            modPlayer.bloodflareThrowing = true;
            modPlayer.godSlayer = true;
            modPlayer.godSlayerThrowing = true;
            modPlayer.silvaSet = true;
            modPlayer.silvaThrowing = true;
            modPlayer.auricSet = true;
			modPlayer.rogueStealthMax = 1.6f;
			player.thorns += 3f;
			player.lavaMax += 240;
			player.ignoreWater = true;
            player.crimsonRegen = true;
            if (player.lavaWet)
            {
                player.statDefense += 30;
                player.lifeRegen += 10;
            }
        }

        public override void UpdateEquip(Player player)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.auricBoost = true;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.2f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 20;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SilvaMask");
            recipe.AddIngredient(null, "GodSlayerMask");
            recipe.AddIngredient(null, "BloodflareHelm");
            recipe.AddIngredient(null, "TarragonHelmet");
            recipe.AddIngredient(null, "AuricOre", 60);
            recipe.AddIngredient(null, "EndothermicEnergy", 10);
            recipe.AddIngredient(null, "NightmareFuel", 10);
            recipe.AddIngredient(null, "Phantoplasm", 8);
            recipe.AddIngredient(null, "DarksunFragment", 6);
            recipe.AddIngredient(null, "BarofLife", 5);
            recipe.AddIngredient(null, "HellcasterFragment", 5);
            recipe.AddIngredient(null, "CoreofCalamity", 2);
            recipe.AddIngredient(null, "GalacticaSingularity");
            recipe.AddIngredient(null, "PsychoticAmulet");
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}