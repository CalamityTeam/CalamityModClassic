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
    public class XerocMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Xeroc Mask");
            /* Tooltip.SetDefault("11% increased rogue damage and critical strike chance\n" +
                "Temporary immunity to lava and immunity to cursed, fire, cursed inferno, and chilled\n" +
                "Wrath of the cosmos"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 40, 0, 0);
			Item.rare = 9;
            Item.defense = 20; //71
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("XerocPlateMail").Type && legs.type == Mod.Find<ModItem>("XerocCuisses").Type;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.xerocSet = true;
			modPlayer.rogueStealthMax = 1.25f;
			player.setBonus = "9% increased rogue damage and velocity\n" +
                "All projectile types have special effects on enemy hits\n" +
                "Imbued with cosmic wrath and rage when you are damaged\n" +
				"Rogue stealth builds while not attacking and not moving, up to a max of 125\n" +
				"Rogue stealth only reduces when you attack, it does not reduce while moving\n" +
				"The higher your rogue stealth the higher your rogue damage, crit, and movement speed";
            if (player.statLife <= (int)((double)player.statLifeMax2 * 0.5))
            {
                player.AddBuff(BuffID.Wrath, 2);
                player.AddBuff(BuffID.Rage, 2);
            }
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.09f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingVelocity += 0.09f;
        }

        public override void UpdateEquip(Player player)
        {
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.11f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 11;
			player.lavaMax += 240;
			player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Chilled] = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "MeldiateBar", 12);
            recipe.AddIngredient(ItemID.LunarBar, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}