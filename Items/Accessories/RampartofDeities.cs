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
    public class RampartofDeities : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rampart of Deities");
            /* Tooltip.SetDefault("Taking damage makes you move very fast for a short time\n" +
                "Increases armor penetration by 50 and immune time after being struck\n" +
                "Provides light underwater and causes stars to fall when damaged\n" +
                "Increases pickup range for mana stars and you restore mana when damaged\n" +
                "Absorbs 25% of damage done to players on your team\n" +
                "Only active above 25% life\n" +
                "Grants immunity to knockback\n" +
                "Puts a shell around the owner when below 50% life that reduces damage\n" +
                "The shell becomes more powerful when below 15% life and reduces damage even further"); */
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 44;
            Item.value = Item.buyPrice(0, 90, 0, 0);
            Item.defense = 12;
            Item.accessory = true;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.dAmulet = true;
			modPlayer.rampartOfDeities = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "FrigidBulwark");
            recipe.AddIngredient(null, "DeificAmulet");
            recipe.AddIngredient(null, "GalacticaSingularity", 5);
            recipe.AddIngredient(null, "DivineGeode", 10);
            recipe.AddIngredient(null, "CosmiliteBar", 20);
            recipe.AddTile(null, "DraedonsForge");
            recipe.Register();
        }
    }
}