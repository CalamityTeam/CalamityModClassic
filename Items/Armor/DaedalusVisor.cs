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
    public class DaedalusVisor : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Daedalus Facemask");
            /* Tooltip.SetDefault("10% increased rogue damage and critcal strike chance, increases rogue velocity by 15%\n" +
                "Immune to Cursed and gives control over gravity"); */
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 25, 0, 0);
			Item.rare = 5;
            Item.defense = 7; //37
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("DaedalusBreastplate").Type && legs.type == Mod.Find<ModItem>("DaedalusLeggings").Type;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "5% increased rogue damage\n" +
                "Rogue projectiles throw out crystal shards as they travel\n" +
				"Rogue stealth builds while not attacking and not moving, up to a max of 110\n" +
				"Rogue stealth only reduces when you attack, it does not reduce while moving\n" +
				"The higher your rogue stealth the higher your rogue damage, crit, and movement speed";
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.daedalusSplit = true;
			modPlayer.rogueStealthMax = 1.1f;
			CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.05f;
        }

        public override void UpdateEquip(Player player)
        {
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingVelocity += 0.15f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.1f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 10;
            player.AddBuff(BuffID.Gravitation, 2);
            player.buffImmune[BuffID.Cursed] = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "VerstaltiteBar", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}