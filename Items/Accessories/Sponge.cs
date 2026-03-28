using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
    public class Sponge : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Sponge");
            /* Tooltip.SetDefault("50% increased mining speed and you emit light\n" +
                "10% increased damage reduction and increased life regen\n" +
                "Poison, Freeze, Chill, Frostburn, and Venom immunity\n" +
                "Honey-like life regen with no speed penalty, +20 max life and mana\n" +
                "Most bee/hornet enemies and projectiles do 75% damage to you\n" +
                "120% increased jump speed and 12% increased movement speed\n" +
                "Standing still boosts life and mana regen\n" +
                "Increased defense and damage reduction when submerged in liquid\n" +
                "Increased movement speed when submerged in liquid\n" +
                "Enemies take damage when they hit you\n" +
                "Taking a hit will make you move very fast for a short time\n" +
                "You emit a mushroom spore and spark explosion when you are hit\n" +
                "Enemy attacks will have part of their damage absorbed and used to heal you"); */
        }

        public override void SetDefaults()
        {
            Item.defense = 10;
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 90, 0, 0);
            Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.beeResist = true;
            modPlayer.aSpark = true;
            modPlayer.gShell = true;
            modPlayer.fCarapace = true;
            modPlayer.absorber = true;
            modPlayer.aAmpoule = true;
            player.statManaMax2 += 20;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "TheAbsorber");
            recipe.AddIngredient(null, "AmbrosialAmpoule");
            recipe.AddIngredient(null, "CosmiliteBar", 15);
            recipe.AddIngredient(null, "Phantoplasm", 15);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}