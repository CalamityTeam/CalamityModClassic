using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class BloodflareHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bloodflare Helm");
            /* Tooltip.SetDefault("You can move freely through liquids and have temporary immunity to lava\n" +
                "10% increased rogue damage and critical strike chance"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 60, 0, 0);
			Item.defense = 28; //85
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("BloodflareBodyArmor").Type && legs.type == Mod.Find<ModItem>("BloodflareCuisses").Type;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.bloodflareSet = true;
            modPlayer.bloodflareThrowing = true;
			modPlayer.rogueStealthMax = 1.35f;
			player.setBonus = "Greatly increases life regen\n" +
                "Enemies below 50% life have a chance to drop hearts when struck\n" +
                "Enemies above 50% life have a chance to drop mana stars when struck\n" +
                "Enemies killed during a Blood Moon have a much higher chance to drop Blood Orbs\n" +
                "Being over 80% life boosts your defense by 30 and rogue crit by 5%\n" +
                "Being below 80% life boosts your rogue damage by 10%\n" +
                "Rogue critical strikes have a 50% chance to heal you\n" +
				"Rogue stealth builds while not attacking and not moving, up to a max of 135\n" +
				"Rogue stealth only reduces when you attack, it does not reduce while moving\n" +
				"The higher your rogue stealth the higher your rogue damage, crit, and movement speed";
            player.crimsonRegen = true;
        }

        public override void UpdateEquip(Player player)
        {
			player.lavaMax += 240;
			player.ignoreWater = true;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.1f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 10;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "BloodstoneCore", 11);
            recipe.AddIngredient(null, "RuinousSoul", 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}