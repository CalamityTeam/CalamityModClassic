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
    public class AuricTeslaCuisses : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Auric Tesla Cuisses");
            /* Tooltip.SetDefault("50% increased movement speed\n" +
                "12% increased damage and 5% increased critical strike chance\n" +
                "Magic carpet effect"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(1, 8, 0, 0);
			Item.defense = 44;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 20;
		}

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.5f;
            player.carpet = true;
            player.GetDamage(DamageClass.Melee) += 0.12f;
            player.GetCritChance(DamageClass.Melee) += 5;
            player.GetDamage(DamageClass.Ranged) += 0.12f;
            player.GetCritChance(DamageClass.Ranged) += 5;
            player.GetDamage(DamageClass.Magic) += 0.12f;
            player.GetCritChance(DamageClass.Magic) += 5;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.12f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 5;
            player.GetDamage(DamageClass.Summon) += 0.12f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SilvaLeggings");
            recipe.AddIngredient(null, "GodSlayerLeggings");
            recipe.AddIngredient(null, "BloodflareCuisses");
            recipe.AddIngredient(null, "TarragonLeggings");
            recipe.AddIngredient(null, "AuricOre", 80);
            recipe.AddIngredient(null, "EndothermicEnergy", 20);
            recipe.AddIngredient(null, "NightmareFuel", 20);
            recipe.AddIngredient(null, "Phantoplasm", 15);
            recipe.AddIngredient(null, "DarksunFragment", 10);
            recipe.AddIngredient(null, "BarofLife", 8);
            recipe.AddIngredient(null, "HellcasterFragment", 6);
            recipe.AddIngredient(null, "CoreofCalamity", 3);
            recipe.AddIngredient(null, "GalacticaSingularity", 2);
            recipe.AddIngredient(ItemID.FlyingCarpet);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}