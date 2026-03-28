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
    public class AstralHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Astral Helm");
            // Tooltip.SetDefault("Danger detection");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 30, 0, 0);
			Item.rare = 7;
            Item.defense = 13; //53
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("AstralBreastplate").Type && legs.type == Mod.Find<ModItem>("AstralLeggings").Type;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "25% increased movement speed\n" +
                "22% increased damage and 17% increased critical strike chance\n" +
                "Whenever you crit an enemy fallen, hallowed, and astral stars will rain down\n" +
                "This effect has a 1 second cooldown before it can trigger again";
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.astralStarRain = true;
            player.moveSpeed += 0.25f;
            player.GetDamage(DamageClass.Melee) += 0.22f;
            player.GetCritChance(DamageClass.Melee) += 17;
            player.GetDamage(DamageClass.Ranged) += 0.22f;
            player.GetCritChance(DamageClass.Ranged) += 17;
            player.GetDamage(DamageClass.Magic) += 0.22f;
            player.GetCritChance(DamageClass.Magic) += 17;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.22f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 17;
            player.GetDamage(DamageClass.Summon) += 0.22f;
        }

        public override void UpdateEquip(Player player)
        {
            player.dangerSense = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AstralBar", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}