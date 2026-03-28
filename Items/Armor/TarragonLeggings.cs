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
    [AutoloadEquip(EquipType.Legs)]
    public class TarragonLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tarragon Leggings");
            /* Tooltip.SetDefault("20% increased movement speed; greater speed boost if health is lower\n" +
                "6% increased damage and critical strike chance\n" +
                "Leggings of a fabled explorer"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 30, 0, 0);
			Item.defense = 32;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.20f;
            if (player.statLife <= (int)((double)player.statLifeMax2 * 0.5))
            {
                player.moveSpeed += 0.15f;
            }
            player.GetDamage(DamageClass.Melee) += 0.06f;
            player.GetCritChance(DamageClass.Melee) += 6;
            player.GetDamage(DamageClass.Magic) += 0.06f;
            player.GetCritChance(DamageClass.Magic) += 6;
            player.GetDamage(DamageClass.Ranged) += 0.06f;
            player.GetCritChance(DamageClass.Ranged) += 6;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.06f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 6;
            player.GetDamage(DamageClass.Summon) += 0.06f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "UeliaceBar", 11);
            recipe.AddIngredient(null, "DivineGeode", 12);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}