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
    public class AtaxiaHood : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ataxia Hood");
            /* Tooltip.SetDefault("12% increased rogue damage and 10% increased rogue critical strike chance\n" +
                "50% chance to not consume rogue items\n" +
                "Temporary immunity to lava and immunity to fire damage"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 30, 0, 0);
			Item.rare = 8;
            Item.defense = 12; //49
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("AtaxiaArmor").Type && legs.type == Mod.Find<ModItem>("AtaxiaSubligar").Type;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "5% increased rogue damage\n" +
                "Inferno effect when below 50% life\n" +
                "Rogue weapons have a 10% chance to unleash a volley of chaos flames around the player that chase enemies when used\n" +
                "You have a 20% chance to emit a blazing explosion when you are hit\n" +
				"Rogue stealth builds while not attacking and not moving, up to a max of 120\n" +
				"Rogue stealth only reduces when you attack, it does not reduce while moving\n" +
				"The higher your rogue stealth the higher your rogue damage, crit, and movement speed";
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.ataxiaBlaze = true;
            modPlayer.ataxiaVolley = true;
			modPlayer.rogueStealthMax = 1.2f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.05f;
        }

        public override void UpdateEquip(Player player)
        {
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingAmmoCost50 = true;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.12f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 10;
			player.lavaMax += 240;
			player.buffImmune[BuffID.OnFire] = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "CruptixBar", 7);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}