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
    public class ReaverCap : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reaver Headgear");
            /* Tooltip.SetDefault("10% increased rogue damage, 5% increased rogue velocity and critical strike chance\n" +
                "10% increased movement speed and can move freely through liquids"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 30, 0, 0);
			Item.rare = 7;
            Item.defense = 10; //43
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("ReaverScaleMail").Type && legs.type == Mod.Find<ModItem>("ReaverCuisses").Type;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.reaverSpore = true;
			modPlayer.rogueStealthMax = 1.15f;
			player.setBonus = "5% increased rogue damage\n" +
                "You emit a cloud of spores when you are hit\n" +
                "Rage activates when you are damaged";
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.05f;
        }

        public override void UpdateEquip(Player player)
        {
            player.ignoreWater = true;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.1f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 5;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingVelocity += 0.05f;
            player.moveSpeed += 0.1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DraedonBar", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}