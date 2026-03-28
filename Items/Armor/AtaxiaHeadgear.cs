using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class AtaxiaHeadgear : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ataxia Headgear");
            /* Tooltip.SetDefault("12% increased ranged damage and 10% increased ranged critical strike chance\n" +
                "Reduces ammo cost by 25%\n" +
                "Temporary immunity to lava and immunity to fire damage"); */
            if (Main.netMode != NetmodeID.Server)
            {
                ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
                ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
            }
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 30, 0, 0);
			Item.rare = 8;
            Item.defense = 15; //53
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
            player.setBonus = "5% increased ranged damage\n" +
                "Inferno effect when below 50% life\n" +
                "You have a 50% chance to fire a homing chaos flare when using ranged weapons\n" +
                "You have a 20% chance to emit a blazing explosion when you are hit";
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.ataxiaBlaze = true;
            modPlayer.ataxiaBolt = true;
            player.GetDamage(DamageClass.Ranged) += 0.05f;
        }

        public override void UpdateEquip(Player player)
        {
            player.ammoCost75 = true;
            player.GetDamage(DamageClass.Ranged) += 0.12f;
            player.GetCritChance(DamageClass.Ranged) += 10;
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