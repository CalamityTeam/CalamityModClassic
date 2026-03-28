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
    public class SilvaArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Silva Armor");
            /* Tooltip.SetDefault("+80 max life\n" +
                       "20% increased movement speed\n" +
                       "12% increased damage and 8% increased critical strike chance"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 72, 0, 0);
			Item.defense = 44;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 15;
		}

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 80;
            player.moveSpeed += 0.2f;
            player.GetDamage(DamageClass.Melee) += 0.12f;
            player.GetCritChance(DamageClass.Melee) += 8;
            player.GetDamage(DamageClass.Ranged) += 0.12f;
            player.GetCritChance(DamageClass.Ranged) += 8;
            player.GetDamage(DamageClass.Magic) += 0.12f;
            player.GetCritChance(DamageClass.Magic) += 8;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.12f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 8;
            player.GetDamage(DamageClass.Summon) += 0.12f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DarksunFragment", 10);
            recipe.AddIngredient(null, "EffulgentFeather", 10);
            recipe.AddIngredient(null, "CosmiliteBar", 10);
			recipe.AddIngredient(null, "Tenebris", 12);
			recipe.AddIngredient(null, "NightmareFuel", 16);
            recipe.AddIngredient(null, "EndothermicEnergy", 16);
			recipe.AddIngredient(null, "LeadCore");
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}