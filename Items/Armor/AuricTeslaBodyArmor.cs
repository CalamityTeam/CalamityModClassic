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
    public class AuricTeslaBodyArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Auric Tesla Body Armor");
            /* Tooltip.SetDefault("+100 max life\n" +
                       "25% increased movement speed\n" +
                       "Attacks have a 2% chance to do no damage to you\n" +
                       "8% increased damage and 5% increased critical strike chance\n" +
                       "You will freeze enemies near you when you are struck"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(1, 44, 0, 0);
			Item.defense = 48;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 20;
		}
        
        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
                EquipLoader.AddEquipTexture(Mod, Texture + "_Back", EquipType.Back, this);
        }
        
        public override void EquipFrameEffects(Player player, EquipType type)
        {
            if (player.body == Item.bodySlot)
                player.back = (sbyte)EquipLoader.GetEquipSlot(Mod, Name, EquipType.Back);
        }

        public override void UpdateEquip(Player player)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.fBarrier = true;
            modPlayer.godSlayerReflect = true;
            player.statLifeMax2 += 100;
            player.moveSpeed += 0.25f;
            player.GetDamage(DamageClass.Melee) += 0.08f;
            player.GetCritChance(DamageClass.Melee) += 5;
            player.GetDamage(DamageClass.Ranged) += 0.08f;
            player.GetCritChance(DamageClass.Ranged) += 5;
            player.GetDamage(DamageClass.Magic) += 0.08f;
            player.GetCritChance(DamageClass.Magic) += 5;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.08f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 5;
            player.GetDamage(DamageClass.Summon) += 0.08f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SilvaArmor");
            recipe.AddIngredient(null, "GodSlayerChestplate");
            recipe.AddIngredient(null, "BloodflareBodyArmor");
            recipe.AddIngredient(null, "TarragonBreastplate");
            recipe.AddIngredient(null, "AuricOre", 100);
            recipe.AddIngredient(null, "EndothermicEnergy", 30);
            recipe.AddIngredient(null, "NightmareFuel", 30);
            recipe.AddIngredient(null, "Phantoplasm", 20);
            recipe.AddIngredient(null, "DarksunFragment", 15);
            recipe.AddIngredient(null, "BarofLife", 10);
            recipe.AddIngredient(null, "HellcasterFragment", 7);
            recipe.AddIngredient(null, "CoreofCalamity", 5);
            recipe.AddIngredient(null, "GalacticaSingularity", 3);
            recipe.AddIngredient(null, "FrostBarrier");
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}