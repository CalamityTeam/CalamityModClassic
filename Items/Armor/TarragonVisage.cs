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
    public class TarragonVisage : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tarragon Visage");
            /* Tooltip.SetDefault("Temporary immunity to lava and immunity to cursed inferno, fire, cursed, and chilled debuffs\n" +
                "Can move freely through liquids\n" +
                "10% increased ranged damage and critical strike chance\n" +
                "5% increased damage reduction"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 50, 0, 0);
			Item.defense = 21; //98
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("TarragonBreastplate").Type && legs.type == Mod.Find<ModItem>("TarragonLeggings").Type;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.tarraSet = true;
            modPlayer.tarraRanged = true;
            player.setBonus = "Reduces enemy spawn rates\n" +
                "Increased heart pickup range\n" +
                "Enemies have a chance to drop extra hearts on death\n" +
                "Ranged critical strikes will cause an explosion of leaves\n" +
                "Ranged projectiles have a chance to split into life energy on death";
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.1f;
            player.GetCritChance(DamageClass.Ranged) += 10;
            player.endurance += 0.05f;
			player.lavaMax += 240;
			player.ignoreWater = true;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Chilled] = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "UeliaceBar", 7);
            recipe.AddIngredient(null, "DivineGeode", 6);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}