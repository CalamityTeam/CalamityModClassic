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
    public class DaedalusBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Daedalus Breastplate");
            // Tooltip.SetDefault("3% increased damage and critical strike chance");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 20, 0, 0);
			Item.rare = 5;
            Item.defense = 17; //41
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Melee) += 3;
            player.GetDamage(DamageClass.Melee) += 0.03f;
            player.GetCritChance(DamageClass.Magic) += 3;
            player.GetDamage(DamageClass.Magic) += 0.03f;
            player.GetCritChance(DamageClass.Ranged) += 3;
            player.GetDamage(DamageClass.Ranged) += 0.03f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 3;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.03f;
            player.GetDamage(DamageClass.Summon) += 0.03f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "VerstaltiteBar", 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}