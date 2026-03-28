using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class AuricTeslaHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Auric Tesla Royal Helm");
            /* Tooltip.SetDefault("20% increased melee damage and critical strike chance\n" +
                               "Not moving boosts all damage and critical strike chance"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(1, 80, 0, 0);
			Item.defense = 54; //132
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
            player.setBonus = "Melee Tarragon, Bloodflare, God Slayer, and Silva armor effects\n" +
                "All projectiles spawn healing auric orbs on enemy hits\n" +
                "Max run speed and acceleration boosted by 10%\n" +
                "Your melee damage is multiplied based on how high your HP is; at full HP this effect is at max";
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.tarraSet = true;
            modPlayer.tarraMelee = true;
            modPlayer.bloodflareSet = true;
            modPlayer.bloodflareMelee = true;
            modPlayer.godSlayer = true;
            modPlayer.godSlayerDamage = true;
            modPlayer.silvaSet = true;
            modPlayer.silvaMelee = true;
            modPlayer.auricSet = true;
            player.thorns += 3f;
			player.lavaMax += 240;
			player.ignoreWater = true;
            player.crimsonRegen = true;
            player.aggro += 1200;
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
            player.GetDamage(DamageClass.Melee) += 0.2f;
            player.GetCritChance(DamageClass.Melee) += 20;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SilvaHelm");
            recipe.AddIngredient(null, "GodSlayerHelm");
            recipe.AddIngredient(null, "BloodflareMask");
            recipe.AddIngredient(null, "TarragonHelm");
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