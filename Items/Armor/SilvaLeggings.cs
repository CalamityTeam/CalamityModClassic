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
    [AutoloadEquip(EquipType.Legs)]
    public class SilvaLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Silva Leggings");
            /* Tooltip.SetDefault("45% increased movement speed\n" +
                "12% increased damage and 7% increased critical strike chance"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 54, 0, 0);
			Item.defense = 39;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 15;
		}

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.45f;
            player.GetDamage(DamageClass.Melee) += 0.12f;
            player.GetCritChance(DamageClass.Melee) += 7;
            player.GetDamage(DamageClass.Ranged) += 0.12f;
            player.GetCritChance(DamageClass.Ranged) += 7;
            player.GetDamage(DamageClass.Magic) += 0.12f;
            player.GetCritChance(DamageClass.Magic) += 7;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.12f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 7;
            player.GetDamage(DamageClass.Summon) += 0.12f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DarksunFragment", 7);
            recipe.AddIngredient(null, "EffulgentFeather", 7);
            recipe.AddIngredient(null, "CosmiliteBar", 7);
			recipe.AddIngredient(null, "Tenebris", 9);
			recipe.AddIngredient(null, "NightmareFuel", 15);
            recipe.AddIngredient(null, "EndothermicEnergy", 15);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}