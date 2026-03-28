using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.Armor;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class StatigelHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Statigel Helm");
            /* Tooltip.SetDefault("10% increased melee damage and melee speed\n" +
                "7% increased melee critical strike chance"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 5, 0, 0);
			Item.rare = 4;
            Item.defense = 9; //27
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("StatigelArmor").Type && legs.type == Mod.Find<ModItem>("StatigelGreaves").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "When you take over 100 damage in one hit you become immune to damage for an extended period of time\n" +
                "Grants an extra jump and increased jump height";
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.statigelSet = true;
            player.GetJumpState(ExtraJump.TsunamiInABottle).Enable();
            player.jumpBoost = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.1f;
            player.GetCritChance(DamageClass.Melee) += 7;
            player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "PurifiedGel", 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 9);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}