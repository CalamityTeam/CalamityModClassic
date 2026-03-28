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
    [AutoloadEquip(EquipType.Body)]
    public class GodSlayerChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("God Slayer Chestplate");
            /* Tooltip.SetDefault("+60 max life\n" +
                       "15% increased movement speed\n" +
                       "Enemies take damage when they hit you\n" +
                       "Attacks have a 2% chance to do no damage to you\n" +
                       "11% increased damage and 6% increased critical strike chance"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 60, 0, 0);
			Item.defense = 41;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}

        public override void UpdateEquip(Player player)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.godSlayerReflect = true;
            player.thorns += 0.5f;
            player.statLifeMax2 += 60;
            player.moveSpeed += 0.15f;
            player.GetDamage(DamageClass.Melee) += 0.11f;
            player.GetCritChance(DamageClass.Melee) += 6;
            player.GetDamage(DamageClass.Magic) += 0.11f;
            player.GetCritChance(DamageClass.Magic) += 6;
            player.GetDamage(DamageClass.Ranged) += 0.11f;
            player.GetCritChance(DamageClass.Ranged) += 6;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.11f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 6;
            player.GetDamage(DamageClass.Summon) += 0.11f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "CosmiliteBar", 23);
            recipe.AddIngredient(null, "NightmareFuel", 11);
            recipe.AddIngredient(null, "EndothermicEnergy", 11);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}