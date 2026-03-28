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
    public class ReaverScaleMail : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reaver Scale Mail");
            /* Tooltip.SetDefault("9% increased damage and 4% increased critical strike chance\n" +
                "+20 max life"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 24, 0, 0);
			Item.rare = 7;
            Item.defense = 19;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 20;
            player.GetCritChance(DamageClass.Melee) += 4;
            player.GetDamage(DamageClass.Melee) += 0.09f;
            player.GetCritChance(DamageClass.Magic) += 4;
            player.GetDamage(DamageClass.Magic) += 0.09f;
            player.GetCritChance(DamageClass.Ranged) += 4;
            player.GetDamage(DamageClass.Ranged) += 0.09f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 4;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.09f;
            player.GetDamage(DamageClass.Summon) += 0.09f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DraedonBar", 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}